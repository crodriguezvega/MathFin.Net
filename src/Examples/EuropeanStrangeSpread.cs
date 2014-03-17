using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;

using MathFinNet;
using MathFinNet.ExtensionMethods;
using MathFinNet.Options;
using MathFinNet.PDE;
using MathFinNet.PDE.FiniteDifference;

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
        TerminalCondition = (double S) => { return option.Payoff(S); },
        LowerBoundaryCondition = (double t) => { return option.K[0] * Math.Exp(-model.R * (option.T - t)); },
        UpperBoundaryCondition = (double t) => { return parameters.maxS - option.K[1] * Math.Exp(-model.R * (option.T - t)); }
      };

      var V = new DenseMatrix(parameters.iMax + 1, parameters.jMax + 1);
      ImplicitMethod.Solve(V, option, model, parameters, conditions);

      // Write matrix to text file to plot in Matlab
      File.WriteAllText(@"OptionPrice.txt", V.ToMatrixString(parameters.iMax + 1, parameters.jMax + 1));

      return 0;
    }
  }
}