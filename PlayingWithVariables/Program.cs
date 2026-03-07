using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace YourNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 5, b = 10, c = 15;
            Console.WriteLine("Please enter a number");
            int d = int.Parse(Console.ReadLine());

            int min = Min(a, b);
            int sum = Add(c, d);
            int div = Division(a, b);
            int times = Multiply(c, d); 
            Console.WriteLine($"The minimum of {a} and {b} is {min}");
            Console.WriteLine($"The addition of {c} and {d} is {sum}");
            Console.WriteLine($"The division of {a} by {b} is {div}");
            Console.WriteLine($"The multiplication of {c} and {d} is {times}");
        }

        static int Min(int x, int y)
        {
            return x < y ? x : y;
        }

        static int Add(int x, int y)
        {                                          
            return x + y;
        }

        static int Division(int x, int y)
        {
            return x / y;
        }

        static int Multiply(int x, int y)
        {
            return x * y;
        }
    }
}