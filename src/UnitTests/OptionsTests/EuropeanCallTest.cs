using System;
using NUnit.Framework;

using MathFin.Net.Options;

namespace UnitTests.OptionsTests
{
  /// <summary>
  /// EuropeanCall test case.
  /// </summary>
  [TestFixture]
  public class EuropeanCallTest
  {
    /// <summary>
    /// Verifies that the EuropeanCall class throws an exception when used without a strike price.
    /// </summary>
    [Test]
    public void EuropeanCallOptionWithoutStrikePrice()
    {
      var option = new EuropeanCall(1);
      Assert.Throws<IndexOutOfRangeException>(() => option.Payoff(100));
    }

    /// <summary>
    /// Verifies that the EuropeanCall class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(100, ExpectedResult = 0, TestName = "EuropeanCallOptionWithSpotPriceBelowStrikePrice")]
    [TestCase(200, ExpectedResult = 50, TestName = "EuropeanCallOptionWithSpotPriceAboveStrikePrice")]
    public double EuropeanCallOptionWithStrikePrice(double spotPrice)
    {
      var option = new EuropeanCall(1, 150);
      return option.Payoff(spotPrice);
    }
  }
}