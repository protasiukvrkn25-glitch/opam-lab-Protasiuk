
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    const int N = 20, K = 3, a = 2, b = 0, c = 0;

    static void Main()
    {
        List<int> list = new List<int>(20);
        Random rnd = new Random(N);

        for (int i = 0; i < 20; i++)
            list.Add(rnd.Next(-9, 10));

        Console.WriteLine("Основний список: " + string.Join(", ", list));

        List<int> positiveOdd = BuildPositiveOdd(list);
        Console.WriteLine("Додатні непарні: " +
            string.Join(", ", positiveOdd));
        Console.WriteLine("Count = " + positiveOdd.Count);

        List<int> wrong = RemoveForwardWrong(list);
        List<int> backward = RemoveBackward(list);
        List<int> copied = RemoveToNewList(list);

        Console.WriteLine("Хибне видалення: " + string.Join(", ", wrong));
        Console.WriteLine("Видалення з кінця: " + string.Join(", ", backward));
        Console.WriteLine("Новий список: " + string.Join(", ", copied));
        Console.WriteLine("Правильні результати однакові: " +
            EqualLists(backward, copied));

        List<int> first = MakeSorted(10 + K % 5, N);
        List<int> second = MakeSorted(6 + K % 4, N + 100);
        List<int> merged = Merge(first, second);

        Console.WriteLine("Список 1: " + string.Join(", ", first));
        Console.WriteLine("Список 2: " + string.Join(", ", second));
        Console.WriteLine("Злиття: " + string.Join(", ", merged));
        Console.WriteLine("Упорядкований: " + IsSorted(merged));

        Split(list, out List<int> negative, out List<int> nonNegative);
        Console.WriteLine("Від'ємні: " + string.Join(", ", negative));
        Console.WriteLine("Невід'ємні: " + string.Join(", ", nonNegative));
        Console.WriteLine("Count збігається: " +
            (negative.Count + nonNegative.Count == list.Count));

        Measure();
    }

    static List<int> BuildPositiveOdd(List<int> source)
    {
        List<int> result = new List<int>(source.Count);
        foreach (int x in source)
            if (x > 0 && x % 2 != 0)
                result.Add(x);
        return result;
    }

    static bool RemoveCondition(int x) => x % 5 == 0;

    static List<int> RemoveForwardWrong(List<int> source)
    {
        List<int> result = new List<int>(source);
        for (int i = 0; i < result.Count; i++)
            if (RemoveCondition(result[i]))
                result.RemoveAt(i);
        return result;
    }

    static List<int> RemoveBackward(List<int> source)
    {
        List<int> result = new List<int>(source);
        for (int i = result.Count - 1; i >= 0; i--)
            if (RemoveCondition(result[i]))
                result.RemoveAt(i);
        return result;
    }

    static List<int> RemoveToNewList(List<int> source)
    {
        List<int> result = new List<int>(source.Count);
        foreach (int x in source)
            if (!RemoveCondition(x))
                result.Add(x);
        return result;
    }

    static bool EqualLists(List<int> x, List<int> y)
    {
        if (x.Count != y.Count) return false;
        for (int i = 0; i < x.Count; i++)
            if (x[i] != y[i]) return false;
        return true;
    }

    static List<int> MakeSorted(int count, int seed)
    {
        List<int> result = new List<int>(count);
        Random rnd = new Random(seed);
        if (count == 0) return result;

        int value = rnd.Next(1, 10);
        result.Add(value);

        for (int i = 1; i < count; i++)
        {
            value += rnd.Next(1, 10);
            result.Add(value);
        }
        return result;
    }

    static List<int> Merge(List<int> x, List<int> y)
    {
        List<int> result = new List<int>(x.Count + y.Count);
        int i = 0, j = 0;

        while (i < x.Count && j < y.Count)
        {
            if (x[i] <= y[j])
                result.Add(x[i++]);
            else
                result.Add(y[j++]);
        }

        while (i < x.Count) result.Add(x[i++]);
        while (j < y.Count) result.Add(y[j++]);

        return result;
    }

    static bool IsSorted(List<int> list)
    {
        for (int i = 1; i < list.Count; i++)
            if (list[i] < list[i - 1]) return false;
        return true;
    }

    static void Split(List<int> source,
        out List<int> negative, out List<int> nonNegative)
    {
        negative = new List<int>(source.Count);
        nonNegative = new List<int>(source.Count);

        foreach (int x in source)
        {
            if (x < 0) negative.Add(x);
            else nonNegative.Add(x);
        }
    }

    static void Measure()
    {
        const int size = 100000;

        // Прогрівання.
        Warmup();

        Stopwatch sw = Stopwatch.StartNew();
        List<int> x = new List<int>();
        for (int i = 0; i < size; i++) x.Add(i);
        sw.Stop();
        Console.WriteLine($"Add без місткості: {sw.Elapsed.TotalMilliseconds:F3} мс");

        sw.Restart();
        List<int> y = new List<int>(size);
        for (int i = 0; i < size; i++) y.Add(i);
        sw.Stop();
        Console.WriteLine($"Add з місткістю: {sw.Elapsed.TotalMilliseconds:F3} мс");

        sw.Restart();
        List<int> z = new List<int>();
        for (int i = 0; i < size; i++) z.Insert(0, i);
        sw.Stop();
        Console.WriteLine($"Insert(0, x): {sw.Elapsed.TotalMilliseconds:F3} мс");

        sw.Restart();
        for (int i = 0; i < 1000; i++) x.Contains(-1);
        sw.Stop();
        Console.WriteLine($"1000 x Contains: {sw.Elapsed.TotalMilliseconds:F3} мс");
    }

    static void Warmup()
    {
        List<int> x = new List<int>();
        for (int i = 0; i < 1000; i++) x.Add(i);

        List<int> y = new List<int>(1000);
        for (int i = 0; i < 1000; i++) y.Add(i);

        List<int> z = new List<int>();
        for (int i = 0; i < 1000; i++) z.Insert(0, i);

        x.Contains(-1);
    }
}
