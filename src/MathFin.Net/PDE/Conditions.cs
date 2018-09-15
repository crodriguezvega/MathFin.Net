using System;

namespace MathFin.Net.PDE
{
  /// <summary>
  /// Terminal and boundary conditions to be applied when solving the Black-Scholes PDE.
  /// </summary>
  public class Conditions
  {
    public Func<double, double> TerminalCondition;
    public Func<double, double> LowerBoundaryCondition;
    public Func<double, double> UpperBoundaryCondition;
  }
}