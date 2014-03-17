using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFinNet.PDE
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