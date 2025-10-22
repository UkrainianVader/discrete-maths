using System;
using System.Text;

class Program
{
    // Глобальні змінні для "малювання" на полотні
    static char[,] canvas;
    static int rows = 16;
    static int cols = 80;

    // Функція для розміщення тексту на полотні
    static void Put(int r, int c, string s)
    {
        if (r < 0 || r >= rows) return;
        for (int i = 0; i < s.Length; i++)
        {
            int cc = c + i;
            if (cc >= 0 && cc < cols) canvas[r, cc] = s[i];
        }
    }

    // Допоміжна функція для малювання одного блоку паралельних контактів
    static void DrawParallelBlock(int y_mid, int start_c, string label_top, string label_bottom)
    {
        int y_top = y_mid - 2;
        int y_bottom = y_mid + 2;
        int contact_c = start_c + 5;
        int end_c = start_c + 12;

        // Вертикальні з'єднання (розгалуження і злиття)
        for (int r = y_top; r <= y_bottom; r++)
        {
            Put(r, start_c, "|");
            Put(r, end_c, "|");
        }

        // Верхня гілка
        Put(y_top, start_c + 1, "----[ ]----");
        Put(y_top - 1, contact_c + 1, label_top);

        // Нижня гілка
        Put(y_bottom, start_c + 1, "----[ ]----");
        Put(y_bottom - 1, contact_c + 1, label_bottom);
    }


    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Ініціалізація полотна
        canvas = new char[rows, cols];
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                canvas[r, c] = ' ';

        // === Малювання схеми ===
        Put(0, 0, "Контактна схема для (a+b)(b+c)(ā+c)");
        int midY = 8; // Центральна вісь y

        // Координати початку блоків
        int block1_start = 10;
        int block2_start = 30;
        int block3_start = 50;

        // Малюємо блоки
        DrawParallelBlock(midY, block1_start, "a", "b");   // Блок (a+b)
        DrawParallelBlock(midY, block2_start, "b", "c");   // Блок (b+c)
        DrawParallelBlock(midY, block3_start, "a'", "c");  // Блок (ā+c)

        // З'єднувальні лінії між блоками (послідовне з'єднання)
        Put(midY, 0, "Вхід -----");
        Put(midY, 22, "--------");
        Put(midY, 42, "--------");
        Put(midY, 62, "---- Вихід");

        // Виведення полотна на екран
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
                Console.Write(canvas[r, c]);
            Console.WriteLine();
        }

        // --- Оновлений аналіз схеми (Таблиця істинності) ---
        Console.WriteLine("\n\nАналіз схеми та таблиця істинності");
        Console.WriteLine("Формула: Y = (a ∨ b) ∧ (b ∨ c) ∧ (¬a ∨ c)");
        
        // Оновлений заголовок таблиці
        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine(" a | b | c | (a+b) | (b+c) | (ā+c) |     (a+b)(b+c)(ā+c) ");
        Console.WriteLine("----------------------------------------------------------------");

        for (int a_val = 0; a_val <= 1; a_val++)
        for (int b_val = 0; b_val <= 1; b_val++)
        for (int c_val = 0; c_val <= 1; c_val++)
        {
            bool a = a_val == 1;
            bool b = b_val == 1;
            bool c = c_val == 1;

            // Обчислення проміжних результатів (кожної дужки)
            bool P1 = a || b;   // (a+b)
            bool P2 = b || c;   // (b+c)
            bool P3 = !a || c;  // (ā+c)

            // Обчислюємо кінцевий результат (кон'юнкція трьох дужок)
            bool Y = P1 && P2 && P3; 
            
            // Перетворення булевих значень на 0 або 1
            int res_P1 = P1 ? 1 : 0;
            int res_P2 = P2 ? 1 : 0;
            int res_P3 = P3 ? 1 : 0;
            int result_Y = Y ? 1 : 0;
            
            // Форматоване виведення рядка таблиці
            Console.WriteLine($" {a_val} | {b_val} | {c_val} |   {res_P1}   |   {res_P2}   |   {res_P3}   |           {result_Y}           ");
        }
        Console.WriteLine("----------------------------------------------------------------");
    }
}