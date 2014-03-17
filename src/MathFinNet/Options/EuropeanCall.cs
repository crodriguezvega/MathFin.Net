using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFinNet.Options
{
  /// <summary>
  /// European call.
  /// </summary>
  public class EuropeanCall : IOption
  {
    public double T { get; set; }
    public double[] K { get; set; }

    /// <summary>
    /// Initializes a new instance of the EuropeanCall class.
    /// </summary>
    public EuropeanCall(double T, params double[] K)
    {
      this.T = T;
      this.K = K;
      Array.Sort(this.K);
    }

    public double Payoff(double S)
    {
      if (S > K[0]) 
      {
        return S - K[0];
      }
      return 0.0;
    }
  }
}