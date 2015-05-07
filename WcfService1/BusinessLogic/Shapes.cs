using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    // values chosen to line up with wire protocol values
    public enum TriangleType : int
    {
        Error = 0,
        Equilateral = 1,
        Isosceles = 2,
        Scalene = 3,
    }

    public static class Shapes
    {
        private static bool IsValidSide(int side, int other1, int other2)
        {
            if (side <= 0) return false;
            if (side <= Math.Abs(other1 - other2)) return false;
            return true;
        }

        public static TriangleType Calculate(int a, int b, int c)
        {
            if (!IsValidSide(a, b, c) || !IsValidSide(b, a, c) || !IsValidSide(c, a, b))
            {
                return TriangleType.Error;
            }

            if (a == b && b == c) return TriangleType.Equilateral;
            if (a == b || b == c) return TriangleType.Isosceles;
            return TriangleType.Scalene;
        }
    }
}
