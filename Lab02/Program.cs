
using System;

class Program
{
    static void Main()
    {
        Random random = new Random(20);

        int[] numbers = new int[20];

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(-9, 10);
        }

        Console.WriteLine("Лабораторна робота 2");
        Console.WriteLine("N = 20, K = 3, a = 2, b = 0, c = 0");

        Console.WriteLine("\nПочатковий масив:");
        PrintArray(numbers);

        AnalyzeArray(numbers);

        int[] filtered = FilterArray(numbers);
        Console.WriteLine("\nМасив після фільтрації (числа < 3):");
        PrintArray(filtered);

        FindLongestSeries(numbers);

        Console.WriteLine("\nРеверс масиву:");
        ReverseArray(numbers);
        PrintArray(numbers);

        int[,] matrix = new int[4, 5];

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = random.Next(-9, 10);
            }
        }

        ProcessMatrix(matrix);

        Console.WriteLine("\nПеревірка крайових випадків:");

        int[] empty = new int[0];
        int[] single = { 5 };
        int[] same = { 4, 4, 4, 4 };

        Console.WriteLine("\nПорожній масив:");
        AnalyzeArray(empty);
        PrintArray(FilterArray(empty));
        FindLongestSeries(empty);
        ReverseArray(empty);
        PrintArray(empty);

        Console.WriteLine("\nМасив з одного елемента:");
        AnalyzeArray(single);
        PrintArray(FilterArray(single));
        FindLongestSeries(single);
        ReverseArray(single);
        PrintArray(single);

        Console.WriteLine("\nМасив з однакових елементів:");
        AnalyzeArray(same);
        FindLongestSeries(same);
    }

    static void PrintArray(int[] a)
    {
        if (a.Length == 0)
        {
            Console.WriteLine("Масив порожній.");
            return;
        }

        for (int i = 0; i < a.Length; i++)
        {
            Console.Write(a[i] + " ");
        }

        Console.WriteLine();
    }

    static void AnalyzeArray(int[] a)
    {
        if (a.Length == 0)
        {
            Console.WriteLine("Аналіз не визначений: масив порожній.");
            return;
        }

        int sum = 0;
        int min = a[0];
        int max = a[0];
        int minIndex = 0;
        int maxIndex = 0;
        int zeros = 0;

        for (int i = 0; i < a.Length; i++)
        {
            sum += a[i];

            if (a[i] < min)
            {
                min = a[i];
                minIndex = i;
            }

            if (a[i] > max)
            {
                max = a[i];
                maxIndex = i;
            }

            if (a[i] == 0)
            {
                zeros++;
            }
        }

        double average = (double)sum / a.Length;

        Console.WriteLine("Сума: " + sum);
        Console.WriteLine("Середнє: " + average);
        Console.WriteLine("Мінімум: " + min + ", індекс: " + minIndex);
        Console.WriteLine("Максимум: " + max + ", індекс: " + maxIndex);
        Console.WriteLine("Кількість нулів: " + zeros);
    }

    static int[] FilterArray(int[] a)
    {
        int count = 0;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] < 3)
            {
                count++;
            }
        }

        int[] result = new int[count];
        int index = 0;

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] < 3)
            {
                result[index] = a[i];
                index++;
            }
        }

        return result;
    }

    static void FindLongestSeries(int[] a)
    {
        if (a.Length == 0)
        {
            Console.WriteLine("Найдовша серія не визначена: масив порожній.");
            return;
        }

        int currentValue = a[0];
        int currentLength = 1;
        int bestValue = a[0];
        int bestLength = 1;
        int bestStart = 0;
        int currentStart = 0;

        for (int i = 1; i < a.Length; i++)
        {
            if (a[i] == currentValue)
            {
                currentLength++;
            }
            else
            {
                if (currentLength > bestLength)
                {
                    bestLength = currentLength;
                    bestValue = currentValue;
                    bestStart = currentStart;
                }

                currentValue = a[i];
                currentLength = 1;
                currentStart = i;
            }
        }

        if (currentLength > bestLength)
        {
            bestLength = currentLength;
            bestValue = currentValue;
            bestStart = currentStart;
        }

        Console.WriteLine("Значення найдовшої серії: " + bestValue);
        Console.WriteLine("Довжина серії: " + bestLength);
        Console.WriteLine("Індекс початку: " + bestStart);
    }

    static void ReverseArray(int[] a)
    {
        int left = 0;
        int right = a.Length - 1;

        while (left < right)
        {
            int temp = a[left];
            a[left] = a[right];
            a[right] = temp;

            left++;
            right--;
        }
    }

    static void ProcessMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        Console.WriteLine("\nМатриця:");

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(4));
            }

            Console.WriteLine();
        }

        int largestRow = 0;
        int largestSum = int.MinValue;

        Console.WriteLine("\nСуми рядків:");

        for (int i = 0; i < rows; i++)
        {
            int rowSum = 0;

            for (int j = 0; j < cols; j++)
            {
                rowSum += matrix[i, j];
            }

            Console.WriteLine("Рядок " + i + ": " + rowSum);

            if (rowSum > largestSum)
            {
                largestSum = rowSum;
                largestRow = i;
            }
        }

        Console.WriteLine("\nМаксимуми стовпців:");

        for (int j = 0; j < cols; j++)
        {
            int columnMax = matrix[0, j];

            for (int i = 1; i < rows; i++)
            {
                if (matrix[i, j] > columnMax)
                {
                    columnMax = matrix[i, j];
                }
            }

            Console.WriteLine("Стовпець " + j + ": " + columnMax);
        }

        Console.WriteLine(
            "\nРядок з найбільшою сумою: " +
            largestRow + ", сума: " + largestSum);
    }
}

