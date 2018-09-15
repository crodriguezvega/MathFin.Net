using System;
using NUnit.Framework;

using MathFin.Net.Options;

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
    public void EuropeanBearSpreadOptionWithoutStrikePrice()
    {
      var option = new EuropeanBearSpread(1);
      Assert.Throws<IndexOutOfRangeException>(() => option.Payoff(100));
    }

    /// <summary>
    /// Verifies that the EuropeanBearSpread class throws an exception when used with only one strike price.
    /// </summary>
    [Test]
    public void EuropeanBearSpreadOptionWithOneStrikePrice()
    {
      var option = new EuropeanBearSpread(1, 100);
      Assert.Throws<IndexOutOfRangeException>(() => option.Payoff(150));
    }

    /// <summary>
    /// Verifies that the EuropeanBearSpread class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(50, ExpectedResult = 200, TestName = "EuropeanBearSpreadOptionWithSpotPriceBelowLowerStrikePrice")]
    [TestCase(150, ExpectedResult = 150, TestName = "EuropeanBearSpreadOptionWithSpotPriceBetweenLowerAndUpperStrikePrices")]
    [TestCase(400, ExpectedResult = 0, TestName = "EuropeanBearSpreadOptionWithSpotPriceAboveUpperStrikePrice")]
    public double EuropeanBearSpreadOptionWithStrikePrices(double spotPrice)
    {
      var option = new EuropeanBearSpread(1, 100, 300);
      return option.Payoff(spotPrice);
    }
  }
}