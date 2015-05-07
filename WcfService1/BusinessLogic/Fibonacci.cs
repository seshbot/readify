using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class Fibonacci
    {
        private static long CalculateImpl(long n, IDictionary<long, long> cache)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            if (cache.ContainsKey(n))
            {
                return cache[n];
            }

            var result = CalculateImpl(n - 1, cache) + CalculateImpl(n - 2, cache);
            cache[n] = result;
            return result;
        }

        public static long Calculate(long n)
        {
            if (n > 92) throw new ArgumentOutOfRangeException("Fib(> 92) will cause a 64 - bit integer overflow.");
            if (n < -92) throw new ArgumentOutOfRangeException("Fib(< 92) will cause a 64 - bit integer overflow.");

            // sign_basis is used to ensure negatives are calculated as positives yet returned again as negatives
            var sign_basis = n < 0 ? -1 : 1;
            return CalculateImpl(n * sign_basis, new Dictionary<long, long>()) * sign_basis;
        }
    }
}
