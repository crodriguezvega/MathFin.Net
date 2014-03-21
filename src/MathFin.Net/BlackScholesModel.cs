using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathFinNet
{
  /// <summary>
  /// Black-Scholes model of a financial market.
  /// </summary>
  /// <remarks>
  /// <a href="http://http://en.wikipedia.org/wiki/Black–Scholes_model">Wikipedia - Black-Scholes model.</a>
  /// </remarks>
  public class BlackScholesModel
  {
    public double S0 { get; set; }
    public double R { get; set; }
    public double Volatility { get; set; }
    public double Div { get; set; }

    /// <summary>
    /// Initializes a new instance of the BlackScholesModel class.
    /// </summary>
    /// <param name="S0">Asset's spot price.</param>
    /// <param name="R">Annualized continuously compounded risk-free interest rate.</param>
    /// <param name="volatility">Annualized asset's return volatility.</param>
    /// <param name="div">Annual dividend yield.</param>
    public BlackScholesModel(double S0, double R, double volatility, double div = 0)
    {
      this.S0 = S0;
      this.R = R;
      this.Volatility = volatility;
      this.Div = div;
    }
  }
}