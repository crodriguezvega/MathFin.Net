using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;

using MathFin.Net;
using MathFin.Net.Options;
using MathFin.Net.PDE;
using MathFin.Net.PDE.FiniteDifference;

namespace Examples
{
  /// <summary>
  /// Example of option price calculation of an European strangle spread option using the implicit method.
  /// </summary>
  public class Example
  {
    static int Main(string[] args)
    {
      var option = new EuropeanStrangleSpread(1, 80, 120);
      var model = new BlackScholesModel(100, 0.05, 0.2);

      var parameters = new DiscretizationParameters(0, 200, 100, 50);
      var conditions = new Conditions()
      {
        TerminalCondition = (double S) => option.Payoff(S),
        LowerBoundaryCondition = (double t) => option.K[0] * Math.Exp(-model.R * (option.T - t)),
        UpperBoundaryCondition = (double t) => parameters.maxS - option.K[1] * Math.Exp(-model.R * (option.T - t))
      };

      var V = new DenseMatrix(parameters.iMax + 1, parameters.jMax + 1);
      ImplicitMethod.Solve(ref V, option, model, parameters, conditions);

      // Write matrix to text file to plot in Matlab
      File.WriteAllText(@"OptionPrice.txt", V.ToMatrixString(parameters.iMax + 1, parameters.jMax + 1));

      return 0;
    }
  }
}