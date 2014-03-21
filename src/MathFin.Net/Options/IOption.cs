using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFinNet.Options
{
  public interface IOption
  {
    /// <summary>
    /// Time to maturiry in years.
    /// </summary>
    double T { get; set; }

    /// <summary>
    /// Strike prices.
    /// </summary>
    double[] K { get; set; }

    /// <summary>
    /// Computes the payoff for a given asset price.
    /// </summary>
    /// <param name="S">Asset price.</param>
    /// <returns>The payoff for a given asset price.</returns>
    double Payoff(double S);
  }
}