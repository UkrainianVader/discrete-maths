using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
    static HashSet<string> InputSet(string name)
    {
        Console.Write($"Введіть елементи множини {name} (через пробіл): ");
        string input = Console.ReadLine();
        return new HashSet<string>(input.Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }

    static void PrintSet(string name, HashSet<string> set)
    {
        Console.Write($"{name} = {{ ");
        Console.Write(string.Join(", ", set.OrderBy(x => x)));
        Console.WriteLine(" }");
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("Обчислення виразу: A \\ (B ∪ C)'\n");

        HashSet<string> U = InputSet("U ");
        HashSet<string> A = InputSet("A");
        HashSet<string> B = InputSet("B");
        HashSet<string> C = InputSet("C");

        //Об'єднання B і C
        HashSet<string> unionBC = new HashSet<string>(B.Union(C));

        //Доповнення (B ∪ C)
        HashSet<string> complementUnionBC = new HashSet<string>(U.Except(unionBC));

        //Різниця A і доповнення (B ∪ C)
        HashSet<string> result = new HashSet<string>(A.Except(complementUnionBC));

        Console.WriteLine("\nРезультати:");
        PrintSet("U", U);
        PrintSet("A", A);
        PrintSet("B", B);
        PrintSet("C", C);
        Console.WriteLine("---");
        PrintSet("(B ∪ C)", unionBC);
        PrintSet("(B ∪ C)'", complementUnionBC);
        PrintSet("A \\ (B ∪ C)'", result);
    }
}