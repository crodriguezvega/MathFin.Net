using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathFinNet.Options;

namespace MathFinNet.PDE
{
  /// <summary>
  /// Parameters to discretize the time and asset price plane into a discrete grid where the Black-Scholes PDE will be solved.
  /// </summary>
  public class DiscretizationParameters
  {
    public double maxS { get; set; }
    public double minS { get; set; }

    public int iMax { get; set; }
    public int jMax { get; set; }

    /// <summary>
    /// Initializes a new instance of the BlackScholesModel class.
    /// </summary>
    /// <param name="minS">Minimum asset price in the grid.</param>
    /// <param name="maxS">Maximum asset price in the grid.</param>
    /// <param name="iMax">Number of grid divisions in the time dimension.</param>
    /// <param name="jMax">Number of grid divisions in the asset price dimension.</param>
    public DiscretizationParameters(double minS, double maxS, int iMax, int jMax)
    {
      this.minS = minS;
      this.maxS = maxS;
      this.iMax = iMax;
      this.jMax = jMax;
    }

    /// <summary>
    /// Computes the time increment.
    /// </summary>
    /// <value>The time increment.</value>
    public double h
    {
      get
      {
        return (maxS - minS) / jMax;
      }
    }

    /// <summary>
    /// Computes the asset price at a given node.
    /// </summary>
    /// <param name="j">The node where the asset price should be calculated.</param>
    /// <returns>The asset price.</returns>
    public double S(int j)
    {
       return minS + h * j;
    }

    /// <summary>
    /// Computes the asset price increment.
    /// </summary>
    /// <param name="option">Option features.</param>
    /// <returns>The asset price increment.</returns>
    public double k(IOption option)
    {
      return option.T / iMax;
    }

    /// <summary>
    /// Computes the time at a given node.
    /// </summary>
    /// <param name="option">Option features.</param>
    /// <param name="i">The node where the time should be calculated.</param>
    /// <returns>The time.</returns>
    public double t(IOption option, int i)
    {
      return k(option) * i;
    }
  }
}