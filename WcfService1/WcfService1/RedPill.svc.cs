using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WcfService1
{
    public class RedPill : IRedPill
    {
        public Guid WhatIsYourToken()
        {
            return new Guid("0bd6725a-ebb1-48fe-aae0-a2ffa56ebbdd");
        }

        public long FibonacciNumber(long n)
        {
            try
            {
                return BusinessLogic.Fibonacci.Calculate(n);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new FaultException<ArgumentOutOfRangeException>(ex);
            }
        }

        public string ReverseWords(string s)
        {
            try
            {
                return BusinessLogic.ReverseWords.Calculate(s);
            }
            catch (ArgumentNullException ex)
            {
                throw new FaultException<ArgumentNullException>(ex);
            }
        }

        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {
            var result = BusinessLogic.Shapes.Calculate(a, b, c);
            return (TriangleType)(int)result;
        }
    }
}
