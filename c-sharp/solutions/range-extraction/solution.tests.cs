#pragma warning disable NUnit2005

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Solutions.RangeExtraction
{
  [TestFixture]
  public class RangeExtractorTest
  {
    [Test(Description = "Simple tests")]
    public void SimpleTests()
    {
      Assert.AreEqual("1,2", RangeExtraction.Extract(new[] { 1, 2 }));
      Assert.AreEqual("1-3", RangeExtraction.Extract(new[] { 1, 2, 3 }));

            Assert.AreEqual(
        "1",
        RangeExtraction.Extract(new[] { 1 })
      );

            Assert.AreEqual(
        "-6,-3-1,3-5,7-11,14,15,17-20",
        RangeExtraction.Extract(new[] { -6, -3, -2, -1, 0, 1, 3, 4, 5, 7, 8, 9, 10, 11, 14, 15, 17, 18, 19, 20 })
      );

      Assert.AreEqual(
        "-3--1,2,10,15,16,18-20",
        RangeExtraction.Extract(new[] { -3, -2, -1, 2, 10, 15, 16, 18, 19, 20 })
      );
      }
  }
}
