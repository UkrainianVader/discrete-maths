using System;
using System.Text;

namespace BinomialCalculator
{
    class Program
    {
        // Власна реалізація піднесення до степеня, оскільки Math.Pow працює з double
        // (int baseVal, int exp) - 'base' є ключовим словом в C#, тому змінено на 'baseVal'
        public static long IntPow(int baseVal, int exp)
        {
            long result = 1;
            for (int i = 0; i < exp; i++)
            {
                result *= baseVal;
            }
            return result;
        }

        // факторіал
        public static long Fact(int n)
        {
            if (n == 0 || n == 1) return 1;
            long res = 1;
            for (int i = 2; i <= n; i++) res *= i;
            return res;
        }

        // комбінація
        public static long C(int n, int k)
        {
            if (k < 0 || k > n)
            {
                return 0; 
            }
            return Fact(n) / (Fact(k) * Fact(n - k));
        }

        // (1) Розклад (ax + by)^n
        public static void BinomialExpansion(int a, int b, int n)
        {
            Console.WriteLine("\n(1) РОЗКЛАД БІНОМА (ax + by)^n");
            Console.Write($"({a}x + {b}y)^{n} = ");

            bool first = true;
            for (int j = 0; j <= n; j++)
            {
                long coef = C(n, j) * IntPow(a, j) * IntPow(b, n - j);

                if (!first)
                {
                    if (coef >= 0) Console.Write(" + ");
                    else Console.Write(" - ");
                }
                else if (coef < 0)
                {
                    Console.Write("-");
                }
                
                Console.Write($"{Math.Abs(coef)}x^{j}y^{n - j}");
                first = false;
            }
            Console.WriteLine();
        }

        // (2) Коефіцієнт біля x^j * y^(n-j)
        public static void BinomialCoefficient(int a, int b, int n, int j)
        {
            Console.WriteLine("\n(2) КОЕФІЦІЄНТ БІЛЯ x^j * y^(n-j) У (ax + by)^n");

            if (j < 0 || j > n)
            {
                Console.WriteLine(" j має бути в межах 0 <= j <= n!");
                return;
            }

            long coef = C(n, j) * IntPow(a, j) * IntPow(b, n - j);
            Console.WriteLine($"Коефіцієнт біля x^{j}y^{n - j} = {coef}");
        }

        // (3) Розклад (ax + by + cz)^n
        public static void PolynomialExpansion(int a, int b, int c, int n)
        {
            Console.WriteLine("\n(3) РОЗКЛАД ПОЛІНОМА (ax + by + cz)^n");
            Console.Write($"({a}x + {b}y + {c}z)^{n} = ");

            bool first = true;
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n - i; j++)
                {
                    int k = n - i - j;
                    long coef = Fact(n) / (Fact(i) * Fact(j) * Fact(k));
                    long termCoef = coef * IntPow(a, i) * IntPow(b, j) * IntPow(c, k);

                    if (!first)
                    {
                        if (termCoef >= 0) Console.Write(" + ");
                        else Console.Write(" - ");
                    }
                    else if (termCoef < 0)
                    {
                        Console.Write("-");
                    }

                    Console.Write($"{Math.Abs(termCoef)}x^{i}y^{j}z^{k}");
                    first = false;
                }
            }
            Console.WriteLine();
        }

        // (4) Коефіцієнт біля x^i * y^j * z^k
        public static void MultinomialCoefficient(int a, int b, int c, int n, int i, int j, int k)
        {
            Console.WriteLine("\n(4) КОЕФІЦІЄНТ БІЛЯ x^i * y^j * z^k У (ax + by + cz)^n");

            if (i + j + k != n)
            {
                Console.WriteLine(" i + j + k ≠ n ");
                return;
            }

            long coef = Fact(n) / (Fact(i) * Fact(j) * Fact(k));
            long result = coef * IntPow(a, i) * IntPow(b, j) * IntPow(c, k);
            Console.WriteLine($"Коефіцієнт біля x^{i}y^{j}z^{k} = {result}");
        }

        public static void Main(string[] args)
        {
            // Встановлюємо кодування для коректного відображення кирилиці в консолі
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            int a, b, c, n, i, j, k;
            string[] parts;

            try
            {
                // (1)
                Console.Write("Введіть a, b, n для розкладу (ax + by)^n (через пробіл): ");
                parts = Console.ReadLine().Split(' ');
                a = int.Parse(parts[0]);
                b = int.Parse(parts[1]);
                n = int.Parse(parts[2]);
                BinomialExpansion(a, b, n);

                // (2)
                Console.Write("\nВведіть a, b, n, j для знаходження коефіцієнта (через пробіл): ");
                parts = Console.ReadLine().Split(' ');
                a = int.Parse(parts[0]);
                b = int.Parse(parts[1]);
                n = int.Parse(parts[2]);
                j = int.Parse(parts[3]);
                BinomialCoefficient(a, b, n, j);

                // (3)
                Console.Write("\nВведіть a, b, c, n для розкладу (ax + by + cz)^n (через пробіл): ");
                parts = Console.ReadLine().Split(' ');
                a = int.Parse(parts[0]);
                b = int.Parse(parts[1]);
                c = int.Parse(parts[2]);
                n = int.Parse(parts[3]);
                PolynomialExpansion(a, b, c, n);

                // (4)
                Console.Write("\nВведіть a, b, c, n, i, j, k для знаходження коефіцієнта (через пробіл): ");
                parts = Console.ReadLine().Split(' ');
                a = int.Parse(parts[0]);
                b = int.Parse(parts[1]);
                c = int.Parse(parts[2]);
                n = int.Parse(parts[3]);
                i = int.Parse(parts[4]);
                j = int.Parse(parts[5]);
                k = int.Parse(parts[6]);
                MultinomialCoefficient(a, b, c, n, i, j, k);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nВиникла помилка: {ex.Message}");
                Console.WriteLine("Будь ласка, переконайтеся, що ви вводите числа коректно, розділяючи їх пробілами.");
            }
        }
    }
}