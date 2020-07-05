using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;
namespace Algorithms_Assignment_1
{
    class Program
    {
        static int call = 0;
        static void Main(string[] args)
        {
            Stopwatch stpwatch = Stopwatch.StartNew();
            BigInteger num1 = BigInteger.Parse("203");
            BigInteger num2 = BigInteger.Parse("100");

            BigInteger ans = Karatsuba_Algorithm(num1, num2);

            BigInteger Karatsuba_Algorithm(BigInteger n1, BigInteger n2)
            {
                int len = n1.ToString().Length;
                int len1 = n2.ToString().Length;
                bool equal_len = true;
                if (len == 1 && len1 == 1)
                {
                    return n1 * n2;
                }
                else if (len != len1)
                {
                    equal_len = false;
                    if (len > len1)
                    {
                        n2 = n2 * (BigInteger)Math.Pow(10, len - len1);
                    }
                    else
                    {
                        n1 = n1 * (BigInteger)Math.Pow(10, len1 - len);
                    }
                }
                int common_len =Math.Max(len,len1);
                int power = common_len;
                if (common_len % 2 != 0)
                {
                    power = common_len - 1;
                }
                BigInteger divisor = 1;
                for(int i=1;i<=common_len/2;i++)
                {
                    divisor *= 10;
                }
                BigInteger coe1 = 1;
                BigInteger coe2 = 1;
                for(int i=1;i<=power;i++)
                {
                    coe1 *= 10;
                    if(i==power/2)
                    {
                        coe2 = coe1;
                    }
                }
                
                BigInteger a = n1 / divisor;
                BigInteger b = n1 % divisor;
                BigInteger c = n2 / divisor;
                BigInteger d = n2 % divisor;
                BigInteger ac = BigInteger.Parse(Karatsuba_Algorithm(a, c).ToString());
                BigInteger bd = BigInteger.Parse(Karatsuba_Algorithm(b, d).ToString());
                BigInteger abcd = BigInteger.Parse(Karatsuba_Algorithm(a + b, c + d).ToString());
                BigInteger prod = coe1 * ac + coe2 * (abcd -ac- bd) + bd;
                //BigInteger ad = BigInteger.Parse(Karatsuba_Algorithm(a, d).ToString());
                //BigInteger bc = BigInteger.Parse(Karatsuba_Algorithm(b, c).ToString());
                // BigInteger prod = (BigInteger)Math.Pow(10, power) * ac + ((BigInteger)Math.Pow(10, power / 2) * (ad + bc)) + bd;

                if (equal_len)
                {
                    return prod;
                }
                else
                {
                    return prod / (BigInteger)Math.Pow(10, Math.Abs(len - len1));
                }
            }
            Console.WriteLine(" {0}", num1 * num2);
            Console.WriteLine(" {0}", ans);
            Console.WriteLine("it took {0} ", stpwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
