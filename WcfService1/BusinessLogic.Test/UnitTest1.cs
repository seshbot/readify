using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.Test
{
    [TestClass]
    public class UnitTest1
    {
        private long FibResult(long n)
        {
            return Fibonacci.Calculate(n);
        }

        [TestMethod]
        public void NegativeValuesMirrorPositiveValues()
        {
            Assert.AreEqual(FibResult(10), -1 * FibResult(-10));
        }

        [TestMethod]
        public void ReversedStringsBecomeOriginalWhenReversedAgain()
        {
            var str1 = " this is a   test";
            var str2 = ReverseWords.Calculate(str1);
            System.Diagnostics.Debug.WriteLine(str1);
            System.Diagnostics.Debug.WriteLine(str2);
            Assert.AreEqual(str1, ReverseWords.Calculate(str2));
        }

        [TestMethod]
        public void ImpossibleTriangleDimensionsResultInError()
        {
            Assert.AreEqual(TriangleType.Error, Shapes.Calculate(-1, 1, 1)); // err: negative side
            Assert.AreEqual(TriangleType.Error, Shapes.Calculate(0, 1, 1)); // err: zero side
            Assert.AreEqual(TriangleType.Error, Shapes.Calculate(10, 1, 1)); // err: impossible
            Assert.AreEqual(TriangleType.Error, Shapes.Calculate(2, 1, 1)); // err: line
        }

        [TestMethod]
        public void ValidTriangleDimensionsResultInValidTriangles()
        {
            Assert.AreEqual(TriangleType.Equilateral, Shapes.Calculate(int.MaxValue, int.MaxValue, int.MaxValue)); // equ overflow test
            Assert.AreEqual(TriangleType.Scalene, Shapes.Calculate(int.MaxValue, 1, int.MaxValue)); // scalene overflow test
            Assert.AreEqual(TriangleType.Equilateral, Shapes.Calculate(1, 1, 1)); // equilateral
            Assert.AreEqual(TriangleType.Scalene, Shapes.Calculate(2, 3, 4)); // scalene
            Assert.AreEqual(TriangleType.Isosceles, Shapes.Calculate(2, 4, 4)); // isosceles
        }
    }
}
