using System;

namespace MathFin.Net.Options
{
  /// <summary>
  /// European bear spread.
  /// </summary>
  public class EuropeanBearSpread : IOption
  {
    public double T { get; set; }
    public double[] K { get; set; }

    /// <summary>
    /// Initializes a new instance of the EuropeanBearSpread class.
    /// </summary>
    public EuropeanBearSpread(double T, params double[] K)
    {
      this.T = T;
      this.K = K;
      Array.Sort(this.K);
    }

    public double Payoff(double S)
    {
      if (S <= K[0]) 
      {
        return K[1] - K[0];
      }
      if (S < K[1] && S > K[0])
      {
        return K[1] - S;
      }
      return 0.0;
    }
  }
}