using System;

namespace MathFin.Net.Options
{
  /// <summary>
  /// European strangle spread.
  /// </summary>
  public class EuropeanStrangleSpread : IOption
  {
    public double T { get; set; }
    public double[] K { get; set; }

    /// <summary>
    /// Initializes a new instance of the EuropeanStrangleSpread class.
    /// </summary>
    public EuropeanStrangleSpread(double T, params double[] K)
    {
      this.T = T;
      this.K = K;
      Array.Sort(this.K);
    }

    public double Payoff(double S)
    {
      if (S < K[0]) 
      {
        return K[0] - S;
      }
      if (S > K[1])
      {
        return S - K[1];
      }
      return 0.0;
    }
  }
}