using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServer_Service.Util
{
    public static class StringComparator
    {
        public static List<string> Compare(string input1, string input2)
        {
            var diffOffset = new List<string>();
            for (int i = 0; i <= input1.Length - 1; i++)
            {
                if (input1[i].ToString() != input2[i].ToString())
                {
                    diffOffset.Add(input1[i].ToString());
                    diffOffset.Add(input2[i].ToString());
                }
            }

            return diffOffset;
        }
    }
}
