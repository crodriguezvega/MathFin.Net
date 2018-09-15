using System;
using NUnit.Framework;

using MathFin.Net.Options;

namespace UnitTests.OptionsTests
{
  /// <summary>
  /// EuropeanPut test case.
  /// </summary>
  [TestFixture]
  public class EuropeanPutTest
  {
    /// <summary>
    /// Verifies that the EuropeanPut class throws an exception when used without a strike price.
    /// </summary>
    [Test]
    public void EuropeanPutOptionWithoutStrikePrice()
    {
      var option = new EuropeanPut(1);
      Assert.Throws<IndexOutOfRangeException>(() => option.Payoff(100));
    }

    /// <summary>
    /// Verifies that the EuropeanPut class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(100, ExpectedResult = 50, TestName = "EuropeanPutOptionWithSpotPriceBelowStrikePrice")]
    [TestCase(150, ExpectedResult = 0, TestName = "EuropeanPutOptionWithSpotPriceAboveStrikePrice")]
    public double EuropeanPutOptionWithStrikePrice(double spotPrice)
    {
      var option = new EuropeanPut(1, 150);
      return option.Payoff(spotPrice);
    }
  }
}