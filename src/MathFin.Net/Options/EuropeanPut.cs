using System;

namespace MathFin.Net.Options
{
  /// <summary>
  /// European put.
  /// </summary>
  public class EuropeanPut : IOption
  {
    public double T { get; set; }
    public double[] K { get; set; }

    /// <summary>
    /// Initializes a new instance of the EuropeanPut class.
    /// </summary>
    public EuropeanPut(double T, params double[] K)
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
      return 0.0;
    }
  }
}