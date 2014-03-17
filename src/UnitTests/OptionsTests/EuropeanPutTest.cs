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
  /// EuropeanPut test case.
  /// </summary>
  [TestFixture]
  public class EuropeanPutTest
  {
    /// <summary>
    /// Verifies that the EuropeanPut class throws an exception when used without a strike price.
    /// </summary>
    [Test]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void EuropeanPutOptionWithoutStrikePrice()
    {
      var option = new EuropeanPut(1);
      option.Payoff(100);
    }

    /// <summary>
    /// Verifies that the EuropeanPut class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(100, Result = 50, TestName = "EuropeanPutOptionWithSpotPriceBelowStrikePrice")]
    [TestCase(150, Result = 0, TestName = "EuropeanPutOptionWithSpotPriceAboveStrikePrice")]
    public double EuropeanPutOptionWithStrikePrice(double spotPrice)
    {
      var option = new EuropeanPut(1, 150);
      return option.Payoff(spotPrice);
    }
  }
}