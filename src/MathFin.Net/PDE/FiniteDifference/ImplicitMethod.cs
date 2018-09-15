using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;

using MathFin.Net.Options;

namespace MathFin.Net.PDE.FiniteDifference
{
  /// <summary>
  /// A class to solve the Black-Scholes PDE using the implicit method.
  /// </summary>
  public static class ImplicitMethod
  {
    /// <summary>
    /// Computes the coefficient multiplying the value of V(i - 1, j - 1).
    /// </summary>
    /// <param name="S">Asset price.</param>
    /// <param name="h">Increment in asset price.</param>
    /// <param name="k">Increment in time.</param>
    /// <param name="R">Annualized continuously compounded risk-free interest rate.</param>
    /// <param name="volatility">Annualized asset's return volatility.</param>
    /// <returns>The coefficient multiplying the value of V(i - 1, j - 1).</returns>
    private static double a(double S, double h, double k, double R, double volatility)
    {
      return (0.5 * k / h) * (R * S - Math.Pow(volatility * S, 2) / h);
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
    private static double b(double S, double h, double k, double R, double volatility)
    {
      return 1 + k * (R + Math.Pow(volatility * S / h, 2));
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
    private static double c(double S, double h, double k, double R, double volatility)
    {
      return (-0.5 * k / h) * (R * S + Math.Pow(volatility * S, 2) / h);
    }

    /// <summary>
    /// Computes the matrix A in equation V(i, ) = A * V(i - 1, ) + z.
    /// </summary>
    /// <param name="option">Option features.</param>
    /// <param name="model">Black-Scholes model.</param>
    /// <param name="dp">Discretization parameters for the grid.</param>
    /// <returns>The matrix A in equation V(i, ) = A * V(i - 1, ) + z.</returns>
    private static DenseMatrix MatrixA(IOption option, BlackScholesModel model, DiscretizationParameters dp)
    {
      var order = dp.jMax - 1;
      var A = new DenseMatrix(order);

      // Center diagonal
      for (int j = 0; j < order; j++)
      {
        A.At(j, j, b(dp.S(j + 1), dp.h, dp.k(option), model.R, model.Volatility));
      }

      // Lower diagonal
      for (int j = 1; j < order; j++)
      {
        A.At(j, j - 1, a(dp.S(j + 1), dp.h, dp.k(option), model.R, model.Volatility));
      }

      // Upper diagonal
      for (int j = 1; j < order; j++)
      {
         A.At(j - 1, j, c(dp.S(j), dp.h, dp.k(option), model.R, model.Volatility));
      }

      return A;
    }

    /// <summary>
    /// Solves the Black-Scholes PDE using the implicit method.
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

      var A = MatrixA(option, model, dp);

      for (int i = dp.iMax; i > 0; i--)
      {
        // Boundary conditions
        var lowerCondition = conditions.LowerBoundaryCondition(dp.k(option) * (i - 1));
        var upperCondition = conditions.UpperBoundaryCondition(dp.k(option) * (i - 1));
        V.At(i - 1, 0, lowerCondition);
        V.At(i - 1, dp.jMax, upperCondition);

        var z = new SparseVector(dp.jMax - 1);
        z[0] = lowerCondition * a(dp.S(1), dp.h, dp.k(option), model.R, model.Volatility);
        z[dp.jMax - 2] = upperCondition * c(dp.S(dp.jMax - 1), dp.h, dp.k(option), model.R, model.Volatility);
        var y = new DenseVector(V.Row(i).Skip(1).Take((int)dp.jMax - 1).ToArray());

        // Solve tridiagonal system
        var x = A.LU().Solve(y.Subtract(z));

        for (int j = 1; j < dp.jMax; j++)
        {
          V.At(i - 1, j, x[j - 1]);
        }
      }
    }
  }
}