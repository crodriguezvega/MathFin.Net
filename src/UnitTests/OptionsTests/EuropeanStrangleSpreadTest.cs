using System;
using NUnit.Framework;

using MathFin.Net.Options;

namespace UnitTests.OptionsTests
{
  /// <summary>
  /// EuropeanStrangleSpread test case.
  /// </summary>
  [TestFixture]
  public class EuropeanStrangleSpreadTest
  {
    /// <summary>
    /// Verifies that the EuropeanStrangleSpread class throws an exception when used without strike prices.
    /// </summary>
    [Test]
    public void EuropeanStrangleSpreadOptionWithoutStrikePrice()
    {
      var option = new EuropeanStrangleSpread(1);
      Assert.Throws<IndexOutOfRangeException>(() => option.Payoff(100));
    }

    /// <summary>
    /// Verifies that the EuropeanStrangleSpread class throws an exception when used with only one strike price.
    /// </summary>
    [Test]
    public void EuropeanStrangleSpreadOptionWithOneStrikePrice()
    {
      var option = new EuropeanStrangleSpread(1, 100);
      Assert.Throws<IndexOutOfRangeException>(() => option.Payoff(150));
    }

    /// <summary>
    /// Verifies that the EuropeanStrangleSpread class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(50, ExpectedResult = 50, TestName = "EuropeanStrangleSpreadOptionWithSpotPriceBelowLowerStrikePrice")]
    [TestCase(150, ExpectedResult = 0, TestName = "EuropeanStrangleSpreadOptionWithSpotPriceBetweenLowerAndUpperStrikePrices")]
    [TestCase(400, ExpectedResult = 100, TestName = "EuropeanStrangleSpreadOptionWithSpotPriceAboveUpperStrikePrice")]
    public double EuropeanStrangleSpreadOptionWithStrikePrices(double spotPrice)
    {
      var option = new EuropeanStrangleSpread(1, 100, 300);
      return option.Payoff(spotPrice);
    }
  }
}