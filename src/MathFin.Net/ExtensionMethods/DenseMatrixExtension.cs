﻿using System;
using MathNet.Numerics.LinearAlgebra.Double;

using MathFin.Net.Options;
using MathFin.Net.PDE;

namespace MathFin.Net.ExtensionMethods
{
  /// <summary>
  /// Extension methods for DenseMatrix.
  /// </summary>
  public static class DenseMatrixExtension
  {
    /// <summary>
    /// Calculates by bilinear interpolation the option price at a point that is not a grid node.
    /// </summary>
    /// <param name="V">The matrix of option prices.</param>
    /// <param name="t">Time at which we are interrested to interpolate the option price.</param>
    /// <param name="t">Asset price for which we are interrested to interpolate the option price.</param>
    /// <param name="option">Option features.</param>
    /// <param name="dp">Discretization parameters for the grid.</param>
    public static double PriceAt(this DenseMatrix V, double t, double S, IOption option, DiscretizationParameters dp)
    {
      var i = Convert.ToInt32(Math.Floor(t / dp.k(option)));
      var j = Convert.ToInt32(Math.Floor(S / dp.h));
      var l1 = (t - dp.t(option, i)) / dp.k(option);
      var l0 = 1.0 - l1;
      var w1 = (S - dp.S(j)) / dp.h;
      var w0 = 1.0 - w1;

      return l1 * w1 * V.At(i + 1, j + 1) + l1 * w0 * V.At(i + 1, j) + l0 * w1 * V.At(i, j + 1) + l0 * w0 * V.At(i, j);
    }
  }
}