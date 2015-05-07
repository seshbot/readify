using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class ReverseWords
    {
        public static string ReverseWord(string s)
        {
            var chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static string Calculate(string s)
        {
            if (s == null) throw new ArgumentNullException();

            var reversed_words =
                from w in s.Split(' ')
                select ReverseWord(w);

            return string.Join(" ", reversed_words.ToList());
        }
    }
}
