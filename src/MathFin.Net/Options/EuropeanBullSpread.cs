using System;

namespace MathFin.Net.Options
{
  /// <summary>
  /// European bull spread.
  /// </summary>
  public class EuropeanBullSpread : IOption
  {
    public double T { get; set; }
    public double[] K { get; set; }

    /// <summary>
    /// Initializes a new instance of the EuropeanBullSpread class.
    /// </summary>
    public EuropeanBullSpread(double T, params double[] K)
    {
      this.T = T;
      this.K = K;
      Array.Sort(this.K);
    }

    public double Payoff(double S)
    {
      if (S >= K[1])
      {
        return K[1] - K[0];
      }
      if (S < K[1] && S > K[0])
      {
        return S - K[0];
      }
      return 0.0;
    }
  }
}