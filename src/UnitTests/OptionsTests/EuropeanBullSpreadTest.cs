using System;
using NUnit.Framework;

using MathFin.Net.Options;

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
    public void EuropeanBullSpreadOptionWithoutStrikePrice()
    {
      var option = new EuropeanBullSpread(1);
      Assert.Throws<IndexOutOfRangeException>(() => option.Payoff(100));
    }

    /// <summary>
    /// Verifies that the EuropeanBullSpread class throws an exception when used with only one strike price.
    /// </summary>
    [Test]
    public void EuropeanBullSpreadOptionWithOneStrikePrice()
    {
      var option = new EuropeanBullSpread(1, 100);
        Assert.Throws<IndexOutOfRangeException>(() => option.Payoff(150));
    }

    /// <summary>
    /// Verifies that the EuropeanBullSpread class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(50, ExpectedResult = 0, TestName = "EuropeanBullSpreadOptionWithSpotPriceBelowLowerStrikePrice")]
    [TestCase(150, ExpectedResult = 50, TestName = "EuropeanBullSpreadOptionWithSpotPriceBetweenLowerAndUpperStrikePrices")]
    [TestCase(400, ExpectedResult = 200, TestName = "EuropeanBullSpreadOptionWithSpotPriceAboveUpperStrikePrice")]
    public double EuropeanBullSpreadOptionWithStrikePrices(double spotPrice)
    {
      var option = new EuropeanBullSpread(1, 100, 300);
      return option.Payoff(spotPrice);
    }
  }
}