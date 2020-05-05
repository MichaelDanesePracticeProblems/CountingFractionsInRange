using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace CountingFractionsInRange
{
    /* This is a Project Euler problem, # 73:  https://projecteuler.net/problem=73
    Consider the fraction, n/d, where n and d are positive integers. If n<d and HCF(n,d)=1, it is called a reduced proper fraction.

    If we list the set of reduced proper fractions for d ≤ 8 in ascending order of size, we get:

    1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8

    It can be seen that there are 3 fractions between 1/3 and 1/2.

    How many fractions lie between 1/3 and 1/2 in the sorted set of reduced proper fractions for d ≤ 12,000?
    */
    //Michael Danese
    class Program
    {
        static void Main(string[] args)
        {//Please give at least to minute to run. My machine ran at roughly 10 seconds.
            int d = 12000, count = 0, ceiling, floor;
            double min = (1.0 / 3.0), max = (1.0 / 2.0), value;
            HCF hcf = new HCF(min, max, d, PrimeGenerator.GeneratePrimes(d));
            for (int i = 1; i <= d; i++)
            {//This will iterate the denominator from 1 to the ceiling given
                ceiling = (int)((i * max) + 1);
                floor = (int)(i * min);
                for (int j = floor; j < ceiling; j++)
                {//This will iterate the numerator from the floor to the ceiling
                    value = (double)j / (double)i;
                    if (value > min && value < max && hcf.FindFactor(j, i) == 1)
                    {
                        count++;//Adds to the total count of the reduced proper fractions
                    }
                }
            }

            output(count);
        }  

        static void output(int count)
        {//Will take the count calculated in the main and output it to the console
            Console.Write("The number of reduced proper fractions between the given range is: ");
            Console.WriteLine(count);
        }
    }

    static class PrimeGenerator
    {//This class will generate a List of primes under the given ceiling
        public static List<int> GeneratePrimes(int max)
        {//The main portion of the Prime Gen
            bool prime = true;
            List<int> primes = new List<int>();
            primes.Add(2);
            for (int i = 3; i < max; i += 2)
            {
                foreach (int element in primes)
                {
                    if (element > (i / 2))
                        break;
                    else if (i % element == 0)
                    {

                        prime = false;
                        break;
                    }
                }
                if (prime)
                {
                    primes.Add(i);
                }
                prime = true;
            }
            foreach (int element in primes)
            {
                if (element > 100) break;
            }
            return primes;
        }
    }
    class HCF
    {//Used to compare the numerator and the denominator
        List<int> primes;
        double min, max;
        int d;
        public HCF(double minInput, double maxInput, int dInput, List<int> input)
        {
            primes = input;
            min = minInput;
            max = maxInput;
            d = dInput;
        }
        public int FindFactor(int n, int d)
        {//Will find a factor of the two numbers. Will try to find something higher than one and will return a 1 if nothing else. If one of the numbers is a zero it will return a zero
            int currentFactor = 1, maxPrime = (n/2) + 1;
            if (d == 0 || n == 0) return 0;
            if (d % n == 0) return n;
            foreach (int element in primes)
            {
                if (element > maxPrime) break;
                if (n % element == 0 && d % element == 0)
                {
                    currentFactor = element;
                    break;
                }
            }
            return currentFactor;
        }
    }
}
