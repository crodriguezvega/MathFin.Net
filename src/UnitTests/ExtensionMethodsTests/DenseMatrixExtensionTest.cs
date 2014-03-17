using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra.Double;

using MathFinNet.Options;
using MathFinNet.PDE;
using MathFinNet.ExtensionMethods;

namespace UnitTests.ExtensionMethodsTests
{
  /// <summary>
  /// DenseMatrixExtension test case.
  /// </summary>
  [TestFixture]
  public class DenseMatrixExtensionTest
  {
    /// <summary>
    /// Calculates the value at 25% between grid nodes from the origin.
    /// </summary>
    [Test]
    public void CalculateValueAt25PercentFromOrigin()
    {
      var option = new EuropeanCall(1, 10);
      var parameters = new DiscretizationParameters(0, 20, 1, 1);

      var V = new DenseMatrix(new double[,] { { 1, 2 }, { 3, 4 } });
      var val = V.PriceAt(0.25, 5, option, parameters);

      Assert.AreEqual(1.75, val);
    }
  }
}