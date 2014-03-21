using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;

using MathFinNet.Options;

namespace MathFinNet.PDE.FiniteDifference
{
  /// <summary>
  /// A class to solve the Black-Scholes PDE using the implicit method.
  /// </summary>
  public static class ExplicitMethod
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
        return (0.5 * k * S / h) * (Math.Pow(volatility, 2) * S / h - R);
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
        return 1 - k * (Math.Pow(volatility * S / h, 2) + R);
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
    private static double c(double S, double h, double k, double R, double volatility)
    {
        return (0.5 * S * k / h) * (Math.Pow(volatility, 2) * S / h + R);
    }

    /// <summary>
    /// Solves the Black-Scholes PDE using the explicit method.
    /// </summary>
    /// <param name="V">Option price.</param>
    /// <param name="option">Option features.</param>
    /// <param name="model">Black-Scholes model.</param>
    /// <param name="dp">Discretization parameters for the grid.</param>
    /// <param name="conditions">Boundary and terminal conditions.</param>
    public static void Solve(DenseMatrix V, IOption option, BlackScholesModel model, DiscretizationParameters dp, Conditions conditions)
    {
      if (dp.k(option) <= 0 && dp.k(option) >= Math.Pow(dp.h, 2) / 2)
      {
        throw new ArgumentException("The explicit method is not stable for the chosen discretization parameters.");
      }

      // Terminal condition
      for (int j = 0; j <= dp.jMax; j++)
      {
        V.At(dp.iMax, j, conditions.TerminalCondition(dp.S(j)));
      }

      for (int i = dp.iMax; i > 0; i--)
      {
        // Boundary conditions
        V.At(i - 1, 0, conditions.LowerBoundaryCondition(dp.k(option) * (i - 1)));
        V.At(i - 1, dp.jMax, conditions.UpperBoundaryCondition(dp.k(option) * (i - 1)));

        for (int j = 1; j < dp.jMax; j++)
        {
          V.At(i - 1, j, a(dp.S(j), dp.h, dp.k(option), model.R, model.Volatility) * V.At(i, j - 1) +
                         b(dp.S(j), dp.h, dp.k(option), model.R, model.Volatility) * V.At(i, j) +
                         c(dp.S(j), dp.h, dp.k(option), model.R, model.Volatility) * V.At(i, j + 1));
        }
      }
    }
  }
}