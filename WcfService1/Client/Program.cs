using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceReference1;
using Client.ServiceReference2;

namespace Client
{
    class Program
    {
        static void Log(string msg)
        {
            Console.WriteLine(msg);
            System.Diagnostics.Debug.WriteLine(msg);
        }

        static void Log(bool success, string msg)
        {
            var prefix = success ? "✓ " : "✕ ";
            Log(prefix + msg);
        }

        class CallResult<T> where T : IComparable
        {
            public CallResult(T value)
            {
                Value = value;
            }
            public CallResult(Exception e)
            {
                Exception = e;
            }
            public T Value;
            public Exception Exception;
            public bool DidThrow { get { return Exception != null; } }

            public bool AreEqual(CallResult<T> other)
            {
                return
                    (DidThrow && other.DidThrow) ||
                    ((DidThrow == other.DidThrow) &&
                     (Value.CompareTo(other.Value) == 0));
            }
        }

        static CallResult<T> InvokeRequest<T>(Func<T, T> request, T n) where T : IComparable
        {
            try
            {
                var value = request(n);
                return new CallResult<T>(value);
            }
            catch (Exception e)
            {
                return new CallResult<T>(e);
            }
        }

        static void AssertFib(ServiceReference1.IRedPill svc1, ServiceReference2.IRedPill svc2, long n)
        {
            var svc1_result = InvokeRequest(x => svc1.FibonacciNumber(x), n);
            var svc2_result = InvokeRequest(x => svc2.FibonacciNumber(x), n);
            if (svc1_result.AreEqual(svc2_result))
            {
                Log(true, "Fib values same (" + svc1_result.Value + ") for n == " + n);
            }
            else
            {
                Log(false, "WARNING: Fib values different for n == " + n);
            }
        }

        static void TestFibonacci(ServiceReference1.IRedPill readify, ServiceReference2.IRedPill cechner)
        {
            AssertFib(readify, cechner, 0);
            AssertFib(readify, cechner, 1);
            AssertFib(readify, cechner, 10);
            AssertFib(readify, cechner, -10);
            AssertFib(readify, cechner, 92);
            AssertFib(readify, cechner, 93);
            AssertFib(readify, cechner, -92);
        }

        static void AssertReverse(ServiceReference1.IRedPill svc1, ServiceReference2.IRedPill svc2, string s)
        {
            var svc1_result = InvokeRequest(x => svc1.ReverseWords(x), s);
            var svc2_result = InvokeRequest(x => svc2.ReverseWords(x), s);
            if (svc1_result.AreEqual(svc2_result))
            {
                Log(true, "String reverse values same (" + svc1_result.Value + ") for s == '" + (s ?? "NULL") + "'");
            }
            else
            {
                Log(false, "String reverse values different for s == '" + (s ?? "NULL") + "'");
            }
        }

        static void TestReverseWords(ServiceReference1.IRedPill readify, ServiceReference2.IRedPill cechner)
        {
            AssertReverse(readify, cechner, " word1 word2     word3  ");
            AssertReverse(readify, cechner, "");
            AssertReverse(readify, cechner, null);
        }

        static void AssertTriangle(ServiceReference1.IRedPill svc1, ServiceReference2.IRedPill svc2, int a, int b, int c)
        {
            // neither should throw exceptions so 
            var res1 = svc1.WhatShapeIsThis(a, b, c);
            var res2 = svc2.WhatShapeIsThis(a, b, c);
            if ((int)res1 == (int)res2)
            {
                Log(true, "Triangle types same (" + res1 + ") for a, b, c == " + a + ", "+ b + ", " + c);
            }
            else
            {
                Log(false, "WARNING: Triangle types different for a, b, c == " + a + ", " + b + ", " + c);
            }
        }

        static void TestTriangle(ServiceReference1.IRedPill readify, ServiceReference2.IRedPill cechner)
        {
            AssertTriangle(readify, cechner, -1, 1, 1); // err: negative side
            AssertTriangle(readify, cechner, 0, 1, 1); // err: zero side
            AssertTriangle(readify, cechner, 10, 1, 1); // err: impossible
            AssertTriangle(readify, cechner, 2, 1, 1); // err: line
            AssertTriangle(readify, cechner, int.MaxValue, int.MaxValue, int.MaxValue); // equ overflow test
            AssertTriangle(readify, cechner, 1, 1, 1); // equilateral
            AssertTriangle(readify, cechner, 2, 3, 4); // scalene
            AssertTriangle(readify, cechner, 2, 4, 4); // isosceles
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            var readify = new ServiceReference1.RedPillClient("ReadifyService");
            var cechner = new ServiceReference2.RedPillClient("CechnerService");

            Log("Readify token: " + readify.WhatIsYourToken());
            Log("Cechner token: " + cechner.WhatIsYourToken());

            TestFibonacci(readify, cechner);
            TestReverseWords(readify, cechner);
            TestTriangle(readify, cechner);

            readify.Close();
            cechner.Close();
        }
    }
}
