
using System;
using System.IO;

class Program
{
    static int maxDepth;

    static void Main()
    {
        Console.WriteLine("Лабораторна робота 3 — Рекурсивні алгоритми");
        Console.WriteLine("N = 20, K = 3, a = 2, b = 0, c = 0");

        Console.WriteLine("\nЗавдання 1. Розмір усіх файлів");
        Console.Write("Введіть шлях до папки: ");
        string path = Console.ReadLine() ?? "";

        if (Directory.Exists(path))
        {
            long totalSize = GetDirectorySize(path);
            Console.WriteLine("Загальний розмір файлів: " + totalSize + " байт");
        }
        else
        {
            Console.WriteLine("Папку не знайдено.");
        }

        Console.WriteLine("\nЗавдання 2. Перевірка однакових елементів");

        Random random = new Random(20);
        int[] numbers = new int[16];

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(-20, 21);
        }

        PrintArray(numbers);

        maxDepth = 0;
        bool recursiveResult = Solve(numbers, 0, numbers.Length - 1, 1);

        Console.WriteLine("Усі елементи однакові: " + recursiveResult);
        Console.WriteLine("Максимальна глибина рекурсії: " + maxDepth);
        Console.WriteLine("log2(16) = 4; рівнів поділу навпіл: 4.");

        Console.WriteLine("\nЗавдання 3. Усі перестановки літер ВАД");

        char[] letters = { 'В', 'А', 'Д' };
        int permutationCount = 0;

        GeneratePermutations(letters, 0, ref permutationCount);

        Console.WriteLine("Кількість перестановок: " + permutationCount);

        Console.WriteLine("\nЗавдання 4. Трасування рекурсії");

        int[] traceArray = { 7, -2, 15, 4 };
        maxDepth = 0;

        bool traceResult = SolveTraced(
            traceArray, 0, traceArray.Length - 1, 0);

        Console.WriteLine("Результат трасування: " + traceResult);

        Console.WriteLine("\nЗавдання 5. Ітеративна версія");

        bool iterativeResult = SolveIterative(numbers);

        Console.WriteLine("Рекурсивний результат: " + recursiveResult);
        Console.WriteLine("Ітеративний результат: " + iterativeResult);
        Console.WriteLine(
            "Результати збігаються: " +
            (recursiveResult == iterativeResult));
    }

    static long GetDirectorySize(string path)
    {
        long total = 0;

        try
        {
            string[] files = Directory.GetFiles(path);

            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    FileInfo info = new FileInfo(files[i]);
                    total += info.Length;
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Немає доступу до файлу: " + files[i]);
                }
                catch (IOException)
                {
                    Console.WriteLine("Не вдалося прочитати файл: " + files[i]);
                }
            }

            string[] directories = Directory.GetDirectories(path);

            for (int i = 0; i < directories.Length; i++)
            {
                total += GetDirectorySize(directories[i]);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Немає доступу до папки: " + path);
        }
        catch (IOException)
        {
            Console.WriteLine("Не вдалося прочитати папку: " + path);
        }

        return total;
    }

    static bool Solve(int[] a, int lo, int hi, int depth)
    {
        if (depth > maxDepth)
        {
            maxDepth = depth;
        }

        if (lo >= hi)
        {
            return true;
        }

        if (hi == lo + 1)
        {
            return a[lo] == a[hi];
        }

        int mid = lo + (hi - lo) / 2;

        bool left = Solve(a, lo, mid, depth + 1);
        bool right = Solve(a, mid + 1, hi, depth + 1);

        return left && right && a[lo] == a[mid + 1];
    }

    static void GeneratePermutations(
        char[] letters, int start, ref int count)
    {
        if (start == letters.Length)
        {
            Console.WriteLine(new string(letters));
            count++;
            return;
        }

        for (int i = start; i < letters.Length; i++)
        {
            char temp = letters[start];
            letters[start] = letters[i];
            letters[i] = temp;

            GeneratePermutations(letters, start + 1, ref count);

            temp = letters[start];
            letters[start] = letters[i];
            letters[i] = temp;
        }
    }

    static bool SolveTraced(int[] a, int lo, int hi, int depth)
    {
        string indent = new string(' ', depth * 2);

        Console.WriteLine(
            indent + "enter Solve(" + lo + ".." + hi + ")");

        bool result;

        if (lo >= hi)
        {
            result = true;
        }
        else if (hi == lo + 1)
        {
            result = a[lo] == a[hi];
        }
        else
        {
            int mid = lo + (hi - lo) / 2;

            bool left = SolveTraced(a, lo, mid, depth + 1);
            bool right = SolveTraced(a, mid + 1, hi, depth + 1);

            result = left && right && a[lo] == a[mid + 1];
        }

        Console.WriteLine(
            indent + "exit  Solve(" + lo + ".." + hi + ") -> " + result);

        return result;
    }

    static bool SolveIterative(int[] a)
    {
        if (a.Length <= 1)
        {
            return true;
        }

        for (int i = 1; i < a.Length; i++)
        {
            if (a[i] != a[0])
            {
                return false;
            }
        }

        return true;
    }

    static void PrintArray(int[] a)
    {
        Console.Write("Масив: ");

        for (int i = 0; i < a.Length; i++)
        {
            Console.Write(a[i] + " ");
        }

        Console.WriteLine();
    }
}
