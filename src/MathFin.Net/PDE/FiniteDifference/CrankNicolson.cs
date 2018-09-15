using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Single.Solvers;

using MathFin.Net.Options;

namespace MathFin.Net.PDE.FiniteDifference
{
  /// <summary>
  /// A class to solve the Black-Scholes PDE using the Crank-Nicolson method.
  /// </summary>
  public static class CrankNicolson
  {
    /// <summary>
    /// Computes the coefficient multiplying the value of V(i, j - 1).
    /// </summary>
    /// <param name="S">Asset price.</param>
    /// <param name="h">Increment in asset price.</param>
    /// <param name="k">Increment in time.</param>
    /// <param name="R">Annualized continuously compounded risk-free interest rate.</param>
    /// <param name="volatility">Annualized asset's return volatility.</param>
    /// <returns>The coefficient multiplying the value of V(i, j - 1).</returns>
    private static double a(double S, double h, double k, double R, double volatility)
    {
      return (0.25 * k / h) * (Math.Pow(volatility * S, 2) / h - R * S);
    }

    /// <summary>
    /// Computes the coefficient multiplying the value of V(i, j).
    /// </summary>
    /// <param name="S">Asset price.</param>
    /// <param name="h">Increment in asset price.</param>
    /// <param name="k">Increment in time.</param>
    /// <param name="R">Annualized continuously compounded risk-free interest rate.</param>
    /// <param name="volatility">Annualized asset's return volatility.</param>
    /// <returns>The coefficient multiplying the value of V(i, j).</returns>
    private static double b(double S, double h, double k, double R, double volatility)
    {
      return 1 - 0.5 * k * (Math.Pow(volatility * S / h, 2) + R);
    }

    /// <summary>
    /// Computes the coefficient multiplying the value of V(i, j + 1).
    /// </summary>
    /// <param name="S">Asset price.</param>
    /// <param name="h">Increment in asset price.</param>
    /// <param name="k">Increment in time.</param>
    /// <param name="R">Annualized continuously compounded risk-free interest rate.</param>
    /// <param name="volatility">Annualized asset's return volatility.</param>
    /// <returns>The coefficient multiplying the value of V(i, j + 1).</returns>
    private static double c(double S, double h, double k, double R, double volatility)
    {
      return (0.25 * k / h) * (Math.Pow(volatility * S, 2) / h + R * S);
    }

    /// <summary>
    /// Computes the coefficient multiplying the value of V(i - 1, j - 1).
    /// </summary>
    /// <param name="S">Asset price.</param>
    /// <param name="h">Increment in asset price.</param>
    /// <param name="k">Increment in time.</param>
    /// <param name="R">Annualized continuously compounded risk-free interest rate.</param>
    /// <param name="volatility">Annualized asset's return volatility.</param>
    /// <returns>The coefficient multiplying the value of V(i - 1, j - 1).</returns>
    private static double d(double S, double h, double k, double R, double volatility)
    {
      return -1 * a(S, h, k, R, volatility);
    }

    /// <summary>
    /// Computes the coefficient multiplying the value of V(i - 1, j).
    /// </summary>
    /// <param name="S">Asset price.</param>
    /// <param name="h">Increment in asset price.</param>
    /// <param name="k">Increment in time.</param>
    /// <param name="R">Annualized continuously compounded risk-free interest rate.</param>
    /// <param name="volatility">Annualized asset's return volatility.</param>
    /// <returns>The coefficient multiplying the value of V(i - 1, j).</returns>
    private static double e(double S, double h, double k, double R, double volatility)
    {
      return 2 - b(S, h, k, R, volatility);
    }

    /// <summary>
    /// Computes the coefficient multiplying the value of V(i - 1, j + 1).
    /// </summary>
    /// <param name="S">Asset price.</param>
    /// <param name="h">Increment in asset price.</param>
    /// <param name="k">Increment in time.</param>
    /// <param name="R">Annualized continuously compounded risk-free interest rate.</param>
    /// <param name="volatility">Annualized asset's return volatility.</param>
    /// <returns>The coefficient multiplying the value of V(i - 1, j + 1).</returns>
    private static double f(double S, double h, double k, double R, double volatility)
    {
      return -1 * c(S, h, k, R, volatility);
    }

    /// <summary>
    /// Computes the matrices A and B in equation B * V(i - 1, ) = A * V(i, ) + z.
    /// </summary>
    /// <param name="option">Option features.</param>
    /// <param name="model">Black-Scholes model.</param>
    /// <param name="dp">Discretization parameters for the grid.</param>
    /// <returns>The matrices A and B in equation B * V(i - 1, ) = A * V(i, ) + z.</returns>
    private static Tuple<DenseMatrix, DenseMatrix> MatricesAandB(IOption option, BlackScholesModel model, DiscretizationParameters dp)
    {
      var order = dp.jMax - 1;
      var A = new DenseMatrix(order);
      var B = new DenseMatrix(order);

      // Center diagonal
      for (int j = 0; j < order; j++)
      {
        A.At(j, j, b(dp.S(j + 1), dp.h, dp.k(option), model.R, model.Volatility));
        B.At(j, j, e(dp.S(j + 1), dp.h, dp.k(option), model.R, model.Volatility));
      }

      // Lower diagonal
      for (int j = 1; j < order; j++)
      {
        A.At(j, j - 1, a(dp.S(j + 1), dp.h, dp.k(option), model.R, model.Volatility));
        B.At(j, j - 1, d(dp.S(j + 1), dp.h, dp.k(option), model.R, model.Volatility));
      }

      // Upper diagonal
      for (int j = 1; j < order; j++)
      {
        A.At(j - 1, j, c(dp.S(j), dp.h, dp.k(option), model.R, model.Volatility));
        B.At(j - 1, j, f(dp.S(j), dp.h, dp.k(option), model.R, model.Volatility));
      }

      return new Tuple<DenseMatrix, DenseMatrix>(A, B);
    }

    /// <summary>
    /// Solves the Black-Scholes PDE using the Crank-Nicolson method.
    /// </summary>
    /// <param name="V">Option price.</param>
    /// <param name="option">Option features.</param>
    /// <param name="model">Black-Scholes model.</param>
    /// <param name="dp">Discretization parameters for the grid.</param>
    /// <param name="conditions">Boundary and terminal conditions.</param>
    public static void Solve(ref DenseMatrix V, IOption option, BlackScholesModel model, DiscretizationParameters dp, Conditions conditions)
    {
      // Terminal condition
      for (int j = 0; j <= dp.jMax; j++)
      {
        V.At(dp.iMax, j, conditions.TerminalCondition(dp.S(j)));
      }

      var AandB = MatricesAandB(option, model, dp);

      for (int i = dp.iMax; i > 0; i--)
      {
        // Boundary conditions
        var lowerCondition = conditions.LowerBoundaryCondition(dp.k(option) * (i - 1));
        var upperCondition = conditions.UpperBoundaryCondition(dp.k(option) * (i - 1));
        V.At(i - 1, 0, lowerCondition);
        V.At(i - 1, dp.jMax, upperCondition);

        var z = new SparseVector(dp.jMax - 1);
        z[0] = V.At(i, 0) * a(dp.S(1), dp.h, dp.k(option), model.R, model.Volatility) -
               lowerCondition * d(dp.S(1), dp.h, dp.k(option), model.R, model.Volatility);
        z[dp.jMax - 2] = V.At(i, dp.jMax) * c(dp.S(dp.jMax - 1), dp.h, dp.k(option), model.R, model.Volatility) -
                         upperCondition * f(dp.S(dp.jMax - 1), dp.h, dp.k(option), model.R, model.Volatility);
        var y = new DenseVector(V.Row(i).Skip(1).Take((int)dp.jMax - 1).ToArray());
        var Btimesy = AandB.Item1.Multiply(y);

        // Solve tridiagonal system
        var x = AandB.Item2.LU().Solve(Btimesy.Add(z));

        for (int j = 1; j < dp.jMax; j++)
        {
          V.At(i - 1, j, x[j - 1]);
        }
      }
    }
  }
}