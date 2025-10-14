using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var dnfTerms = new List<string>();
        var cnfClauses = new List<string>();

        Console.WriteLine("Введіть F (0 або 1).");
        Console.WriteLine("a b c | F");
        Console.WriteLine("--------------");

        for (int i = 0; i < 8; i++)
        {
            bool a = (i & 4) != 0;
            bool b = (i & 2) != 0;
            bool c = (i & 1) != 0;

            Console.Write($"{(a ? 1 : 0)} {(b ? 1 : 0)} {(c ? 1 : 0)} | ");

            int input;
            while (true)
            {
                string? line = Console.ReadLine();
                if (int.TryParse(line, out input) && (input == 0 || input == 1))
                {
                    break;
                }
                Console.Write("Введіть 0 або 1: ");
            }

            bool value = (input == 1);

            if (value)
            {
                string term = "(" +
                              (a ? "a" : "¬a") + " ∧ " +
                              (b ? "b" : "¬b") + " ∧ " +
                              (c ? "c" : "¬c") + ")";
                dnfTerms.Add(term);
            }
            else
            {
                string clause = "(" +
                                (a ? "¬a" : "a") + " ∨ " +
                                (b ? "¬b" : "b") + " ∨ " +
                                (c ? "¬c" : "c") + ")";
                cnfClauses.Add(clause);
            }
        }

        string dnf = dnfTerms.Count > 0 ? string.Join(" ∨ ", dnfTerms) : "0";
        string cnf = cnfClauses.Count > 0 ? string.Join(" ∧ ", cnfClauses) : "1";

        Console.WriteLine();
        Console.WriteLine("ДДНФ: " + dnf);
        Console.WriteLine("ДКНФ: " + cnf);

        
        Console.WriteLine();
    }
}


