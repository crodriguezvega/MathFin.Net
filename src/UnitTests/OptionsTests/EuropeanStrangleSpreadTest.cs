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
  /// EuropeanStrangleSpread test case.
  /// </summary>
  [TestFixture]
  public class EuropeanStrangleSpreadTest
  {
    /// <summary>
    /// Verifies that the EuropeanStrangleSpread class throws an exception when used without strike prices.
    /// </summary>
    [Test]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void EuropeanStrangleSpreadOptionWithoutStrikePrice()
    {
      var option = new EuropeanStrangleSpread(1);
      option.Payoff(100);
    }

    /// <summary>
    /// Verifies that the EuropeanStrangleSpread class throws an exception when used with only one strike price.
    /// </summary>
    [Test]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void EuropeanStrangleSpreadOptionWithOneStrikePrice()
    {
      var option = new EuropeanStrangleSpread(1, 100);
      option.Payoff(150);
    }

    /// <summary>
    /// Verifies that the EuropeanStrangleSpread class returns the correct payoff.
    /// </summary>
    /// <param name="spotPrice">Asset's spot price.</param>
    [TestCase(50, Result = 50, TestName = "EuropeanStrangleSpreadOptionWithSpotPriceBelowLowerStrikePrice")]
    [TestCase(150, Result = 0, TestName = "EuropeanStrangleSpreadOptionWithSpotPriceBetweenLowerAndUpperStrikePrices")]
    [TestCase(400, Result = 100, TestName = "EuropeanStrangleSpreadOptionWithSpotPriceAboveUpperStrikePrice")]
    public double EuropeanStrangleSpreadOptionWithStrikePrices(double spotPrice)
    {
      var option = new EuropeanStrangleSpread(1, 100, 300);
      return option.Payoff(spotPrice);
    }
  }
}