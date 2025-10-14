using System;

public class Program
{
    static bool Implies(bool p, bool q) => !p || q;

    // Функція для перетворення boolean в 0 або 1 для виводу
    static int ToBit(bool value) => value ? 1 : 0;

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string header = " a b c | a→b | b→c | c→a | (b→c)→(c→a) | (a → b) → ((b → c) → (c → a))";
        Console.WriteLine(header);
        Console.WriteLine(new string('-', header.Length));

        bool allResultsTrue = true;
        bool atLeastOneTrue = false;

        
        for (int aVal = 0; aVal <= 1; aVal++)
        for (int bVal = 0; bVal <= 1; bVal++)
        for (int cVal = 0; cVal <= 1; cVal++)
        {
            
            bool a = aVal == 1;
            bool b = bVal == 1;
            bool c = cVal == 1;

            bool res_a_b = Implies(a, b);          // (a → b)
            bool res_b_c = Implies(b, c);          // (b → c)
            bool res_c_a = Implies(c, a);          // (c → a)
            bool res_mid = Implies(res_b_c, res_c_a); // ((b → c) → (c → a))
            
            // Обчислюємо результат формули
            bool finalResult = Implies(res_a_b, res_mid);
            
            Console.WriteLine($" {ToBit(a)} {ToBit(b)} {ToBit(c)} |  {ToBit(res_a_b)}  |  {ToBit(res_b_c)}  |  {ToBit(res_c_a)}  |      {ToBit(res_mid)}      |     {ToBit(finalResult)}");
            
            if (finalResult)
            {
                atLeastOneTrue = true;
            }
            else
            {
                allResultsTrue = false;
            }
        }

        Console.WriteLine("------------------");
        Console.WriteLine("\nОцінка формули:");

        if (allResultsTrue)
        {
            Console.WriteLine("Формула є тавтологією та виконуваною.");
        }
        else if (atLeastOneTrue)
        {
            Console.WriteLine("Формула є виконуваною (нейтральною).");
        }
        else
        {
            Console.WriteLine("Формула є суперечністю та НЕвиконуваною.");
        }
    }
}