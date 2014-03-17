using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using MathFinNet.Options;

namespace UnitTests.OptionsTests
{
  /// <summary>
  /// EuropeanBearSpread test case.
  /// </summary>
  [TestFixture]
  public class EuropeanBearSpreadTest
  {
    /// <summary>
    /// Verifies that the EuropeanBearSpread class throws an exception when used without strike prices.
    /// </summary>
    [Test]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void EuropeanBearSpreadOptionWithoutStrikePrice()
    {
      var option = new EuropeanBearSpread(1);
      option.Payoff(100);
    }

    /// <summary>
    /// Verifies that the EuropeanBearSpread class throws an exception when used with only one strike price.
    /// </summary>
    [Test]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void EuropeanBearSpreadOptionWithOneStrikePrice()
    {
      var option = new EuropeanBearSpread(1, 100);
      option.Payoff(150);
    }

    /// <summary>
    /// Verifies that the EuropeanBearSpread class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(50, Result = 200, TestName = "EuropeanBearSpreadOptionWithSpotPriceBelowLowerStrikePrice")]
    [TestCase(150, Result = 150, TestName = "EuropeanBearSpreadOptionWithSpotPriceBetweenLowerAndUpperStrikePrices")]
    [TestCase(400, Result = 0, TestName = "EuropeanBearSpreadOptionWithSpotPriceAboveUpperStrikePrice")]
    public double EuropeanBearSpreadOptionWithStrikePrices(double spotPrice)
    {
      var option = new EuropeanBearSpread(1, 100, 300);
      return option.Payoff(spotPrice);
    }
  }
}