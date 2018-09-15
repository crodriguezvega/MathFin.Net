using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra.Double;

using MathFin.Net.Options;
using MathFin.Net.PDE;
using MathFin.Net.ExtensionMethods;

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

      var V = DenseMatrix.OfArray(new double[,] { { 1, 2 }, { 3, 4 } });
      var val = V.PriceAt(0.25, 5, option, parameters);

      Assert.AreEqual(1.75, val);
    }
  }
}