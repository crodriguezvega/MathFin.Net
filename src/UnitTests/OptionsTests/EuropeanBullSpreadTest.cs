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
  /// EuropeanBullSpread test case.
  /// </summary>
  [TestFixture]
  public class EuropeanBullSpreadTest
  {
    /// <summary>
    /// Verifies that the EuropeanBullSpread class throws an exception when used without strike prices.
    /// </summary>
    [Test]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void EuropeanBullSpreadOptionWithoutStrikePrice()
    {
      var option = new EuropeanBullSpread(1);
      option.Payoff(100);
    }

    /// <summary>
    /// Verifies that the EuropeanBullSpread class throws an exception when used with only one strike price.
    /// </summary>
    [Test]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void EuropeanBullSpreadOptionWithOneStrikePrice()
    {
      var option = new EuropeanBullSpread(1, 100);
      option.Payoff(150);
    }

    /// <summary>
    /// Verifies that the EuropeanBullSpread class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(50, Result = 0, TestName = "EuropeanBullSpreadOptionWithSpotPriceBelowLowerStrikePrice")]
    [TestCase(150, Result = 50, TestName = "EuropeanBullSpreadOptionWithSpotPriceBetweenLowerAndUpperStrikePrices")]
    [TestCase(400, Result = 200, TestName = "EuropeanBullSpreadOptionWithSpotPriceAboveUpperStrikePrice")]
    public double EuropeanBullSpreadOptionWithStrikePrices(double spotPrice)
    {
      var option = new EuropeanBullSpread(1, 100, 300);
      return option.Payoff(spotPrice);
    }
  }
}