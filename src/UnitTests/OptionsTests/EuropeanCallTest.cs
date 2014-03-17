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
  /// EuropeanCall test case.
  /// </summary>
  [TestFixture]
  public class EuropeanCallTest
  {
    /// <summary>
    /// Verifies that the EuropeanCall class throws an exception when used without a strike price.
    /// </summary>
    [Test]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void EuropeanCallOptionWithoutStrikePrice()
    {
      var option = new EuropeanCall(1);
      option.Payoff(100);
    }

    /// <summary>
    /// Verifies that the EuropeanCall class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(100, Result = 0, TestName = "EuropeanCallOptionWithSpotPriceBelowStrikePrice")]
    [TestCase(200, Result = 50, TestName = "EuropeanCallOptionWithSpotPriceAboveStrikePrice")]
    public double EuropeanCallOptionWithStrikePrice(double spotPrice)
    {
      var option = new EuropeanCall(1, 150);
      return option.Payoff(spotPrice);
    }
  }
}