using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

class Student
{
    public string Name;
    public string Group;
    public int Score;

    public Student(string name, string group, int score)
    {
        Name = name;
        Group = group;
        Score = score;
    }
}

class Program
{
    const int N = 20;
    const int K = 3;
    const int A = 2;
    const int B = 0;
    const int C = 0;
    const int Threshold = K % 3 + 2;

    static readonly string TestText =
        "Алгоритм описує послідовність дій. Алгоритм можна записати словами, " +
        "а можна записати кодом. Кодом алгоритм стає тоді, коли його " +
        "виконує машина, а не людина. Одна задача — багато алгоритмів.";

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine($"N = {N}, K = {K}, a = {A}, b = {B}, c = {C}");
        Console.WriteLine("\n=== Завдання 1. Частоти довжин слів ===");
        Task1();

        Console.WriteLine("\n=== Завдання 2. Групування студентів ===");
        Task2();

        Console.WriteLine("\n=== Завдання 3. Вилучення дублікатів ===");
        Task3();

        Console.WriteLine("\n=== Завдання 4. Спільні елементи наборів ===");
        Task4();

        Console.WriteLine("\n=== Завдання 5. Вимірювання швидкодії ===");
        RunBenchmarks();

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення.");
        Console.ReadKey();
    }

    static List<string> GetWords(string text)
    {
        List<string> words = new List<string>();
        string[] parts = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

        foreach (string part in parts)
        {
            StringBuilder word = new StringBuilder();
            foreach (char ch in part)
            {
                if (char.IsLetter(ch))
                    word.Append(ch);
            }

            if (word.Length > 0)
                words.Add(word.ToString());
        }

        return words;
    }

    static Dictionary<int, int> CountWordLengthsDictionary(string text)
    {
        Dictionary<int, int> frequencies = new Dictionary<int, int>();

        foreach (string word in GetWords(text))
        {
            int length = word.Length;
            if (frequencies.TryGetValue(length, out int count))
                frequencies[length] = count + 1;
            else
                frequencies.Add(length, 1);
        }

        return frequencies;
    }

    static List<int[]> CountWordLengthsList(string text)
    {
        // Кожен елемент: [довжина слова, частота]. Словники не використовуються.
        List<int[]> frequencies = new List<int[]>();

        foreach (string word in GetWords(text))
        {
            int length = word.Length;
            bool found = false;

            foreach (int[] item in frequencies)
            {
                if (item[0] == length)
                {
                    item[1]++;
                    found = true;
                    break;
                }
            }

            if (!found)
                frequencies.Add(new int[] { length, 1 });
        }

        return frequencies;
    }

    static void Task1()
    {
        Console.WriteLine("Текст: " + TestText);
        Dictionary<int, int> dictionary = CountWordLengthsDictionary(TestText);
        Console.WriteLine($"Поріг частоти: {Threshold}");

        foreach (KeyValuePair<int, int> pair in dictionary)
        {
            if (pair.Value >= Threshold)
                Console.WriteLine($"Довжина {pair.Key}: {pair.Value} слів");
        }

        Console.WriteLine($"Кількість різних довжин: {dictionary.Count}");

        List<int[]> listResult = CountWordLengthsList(TestText);
        Console.WriteLine("Списковий спосіб:");
        foreach (int[] item in listResult)
        {
            if (item[1] >= Threshold)
                Console.WriteLine($"Довжина {item[0]}: {item[1]} слів");
        }
        Console.WriteLine($"Кількість різних довжин: {listResult.Count}");
    }

    static List<Student> CreateStudents()
    {
        return new List<Student>
        {
            new Student("Коваленко", "КН-3/1", 88),
            new Student("Мельник", "КН-3/2", 64),
            new Student("Іщенко", "КН-3/1", 93),
            new Student("Бондар", "КН-3/1", 71),
            new Student("Савченко", "КН-3/2", 55),
            new Student("Ткачук", "КН-3/2", 90),
            new Student("Кравець", "КН-3/1", 76),
            new Student("Іванів", "КН-3/1", 48),
            new Student("Мороз", "КН-3/2", 82),
            new Student("Шевчук", "КН-3/1", 67),
            new Student("Бойко", "КН-3/2", 95),
            new Student("Кушнір", "КН-3/2", 73)
        };
    }

    static Dictionary<char, List<Student>> GroupStudentsDictionary(List<Student> students)
    {
        Dictionary<char, List<Student>> groups = new Dictionary<char, List<Student>>();

        foreach (Student student in students)
        {
            char key = student.Name[0];

            if (!groups.ContainsKey(key))
                groups.Add(key, new List<Student>());

            groups[key].Add(student);
        }

        return groups;
    }

    static List<KeyValuePair<char, List<Student>>> GroupStudentsList(List<Student> students)
    {
        List<KeyValuePair<char, List<Student>>> groups =
            new List<KeyValuePair<char, List<Student>>>();

        foreach (Student student in students)
        {
            char key = student.Name[0];
            bool found = false;

            for (int i = 0; i < groups.Count; i++)
            {
                if (groups[i].Key == key)
                {
                    groups[i].Value.Add(student);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                List<Student> newGroup = new List<Student>();
                newGroup.Add(student);
                groups.Add(new KeyValuePair<char, List<Student>>(key, newGroup));
            }
        }

        return groups;
    }

    static void PrintStudentGroup(char key, List<Student> students)
    {
        int total = 0;
        foreach (Student student in students)
            total += student.Score;

        double average = students.Count == 0 ? 0 : (double)total / students.Count;
        Console.WriteLine($"Група '{key}': студентів {students.Count}, середній бал {average:F2}");
    }

    static void Task2()
    {
        List<Student> students = CreateStudents();

        Console.WriteLine("Словник:");
        Dictionary<char, List<Student>> dictionary = GroupStudentsDictionary(students);
        foreach (KeyValuePair<char, List<Student>> pair in dictionary)
            PrintStudentGroup(pair.Key, pair.Value);

        Console.WriteLine("Списковий спосіб:");
        List<KeyValuePair<char, List<Student>>> listGroups = GroupStudentsList(students);
        foreach (KeyValuePair<char, List<Student>> pair in listGroups)
            PrintStudentGroup(pair.Key, pair.Value);
    }

    static List<int> CreateRandomList(int count, int min, int maxInclusive, int seed)
    {
        Random random = new Random(seed);
        List<int> numbers = new List<int>();

        for (int i = 0; i < count; i++)
            numbers.Add(random.Next(min, maxInclusive + 1));

        return numbers;
    }

    static List<int> RemoveDuplicatesHashSet(List<int> numbers)
    {
        HashSet<int> unique = new HashSet<int>(numbers);
        return new List<int>(unique);
    }

    static List<int> RemoveDuplicatesPreserveOrder(List<int> numbers)
    {
        HashSet<int> seen = new HashSet<int>();
        List<int> result = new List<int>();

        foreach (int number in numbers)
        {
            if (seen.Add(number))
                result.Add(number);
        }

        return result;
    }

    static void PrintNumbers(string label, List<int> numbers)
    {
        Console.WriteLine(label + ": " + string.Join(", ", numbers));
    }

    static void Task3()
    {
        List<int> numbers = CreateRandomList(25, 1, K % 10 + 6, N);
        PrintNumbers("Вихідний список", numbers);
        PrintNumbers("HashSet (порядок не гарантується)", RemoveDuplicatesHashSet(numbers));
        PrintNumbers("Без дублікатів зі збереженням порядку",
            RemoveDuplicatesPreserveOrder(numbers));
    }

    static HashSet<int> CreateSet(List<int> numbers)
    {
        return new HashSet<int>(numbers);
    }

    static List<int> CommonWithHashSet(List<int> first, List<int> second)
    {
        HashSet<int> secondSet = CreateSet(second);
        HashSet<int> common = new HashSet<int>();

        foreach (int number in first)
        {
            if (secondSet.Contains(number))
                common.Add(number);
        }

        return new List<int>(common);
    }

    static List<int> CommonWithNestedLoops(List<int> first, List<int> second)
    {
        List<int> common = new List<int>();

        foreach (int x in first)
        {
            bool inSecond = false;
            foreach (int y in second)
            {
                if (x == y)
                {
                    inSecond = true;
                    break;
                }
            }

            if (inSecond && !common.Contains(x))
                common.Add(x);
        }

        return common;
    }

    static bool SameElements(List<int> first, List<int> second)
    {
        HashSet<int> set = new HashSet<int>(first);
        if (set.Count != second.Count)
            return false;

        foreach (int value in second)
        {
            if (!set.Contains(value))
                return false;
        }

        return true;
    }

    static void Task4()
    {
        List<int> first = CreateRandomList(30, 1, 20 + K, N);
        List<int> second = CreateRandomList(30, 1, 20 + K, N + 100);

        PrintNumbers("Перший набір", first);
        PrintNumbers("Другий набір", second);

        List<int> hashResult = CommonWithHashSet(first, second);
        List<int> listResult = CommonWithNestedLoops(first, second);

        PrintNumbers("Спільні елементи через HashSet", hashResult);
        PrintNumbers("Спільні елементи вкладеними циклами", listResult);
        Console.WriteLine($"Кількість спільних елементів: {hashResult.Count}");
        Console.WriteLine("Перевірка збігу результатів: " +
            (SameElements(hashResult, listResult) ? "успішна" : "ПОМИЛКА"));
    }

    static long MeasureMilliseconds(Action action)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        return stopwatch.ElapsedMilliseconds;
    }

    static bool ListContains(List<int> values, int target)
    {
        foreach (int value in values)
        {
            if (value == target)
                return true;
        }
        return false;
    }

    static int CountFrequencyList(List<string> words, string target)
    {
        int count = 0;
        foreach (string word in words)
        {
            if (word == target)
                count++;
        }
        return count;
    }

    static List<int> UniqueListSlow(List<int> values)
    {
        List<int> result = new List<int>();
        foreach (int value in values)
        {
            if (!result.Contains(value))
                result.Add(value);
        }
        return result;
    }

    static List<int> CommonListSlow(List<int> first, List<int> second)
    {
        List<int> result = new List<int>();
        foreach (int value in first)
        {
            if (second.Contains(value) && !result.Contains(value))
                result.Add(value);
        }
        return result;
    }

    static void RunBenchmarks()
    {
        // Прогрівальний прогін перед вимірюваннями.
        List<int> warmup = CreateRandomList(1000, 1, 100, 1);
        ListContains(warmup, 50);
        new HashSet<int>(warmup).Contains(50);
        UniqueListSlow(warmup);
        new HashSet<int>(warmup);
        CommonListSlow(warmup, warmup);
        new HashSet<int>(warmup).IntersectWith(warmup);

        // 1. 10 000 перевірок наявності у 100 000 елементів.
        List<int> membershipData = CreateRandomList(100000, 1, 200000, 10);
        List<int> queries = CreateRandomList(10000, 1, 200000, 11);
        HashSet<int> membershipSet = new HashSet<int>(membershipData);
        int membershipHits = 0;
        long listMembershipMs = MeasureMilliseconds(() =>
        {
            foreach (int q in queries)
                if (ListContains(membershipData, q)) membershipHits++;
        });
        int setHits = 0;
        long setMembershipMs = MeasureMilliseconds(() =>
        {
            foreach (int q in queries)
                if (membershipSet.Contains(q)) setHits++;
        });

        // 2. Частоти 50 000 слів: словник проти двох паралельних списків.
        string[] vocabulary = { "алгоритм", "дані", "список", "словник", "множина",
            "частота", "група", "число", "текст", "код" };
        Random wordRandom = new Random(50);
        List<string> words = new List<string>();
        for (int i = 0; i < 50000; i++)
            words.Add(vocabulary[wordRandom.Next(vocabulary.Length)]);

        long listFrequencyMs = MeasureMilliseconds(() =>
        {
            List<string> uniqueWords = new List<string>();
            List<int> counts = new List<int>();
            foreach (string word in words)
            {
                int index = -1;
                for (int i = 0; i < uniqueWords.Count; i++)
                {
                    if (uniqueWords[i] == word)
                    {
                        index = i;
                        break;
                    }
                }

                if (index == -1)
                {
                    uniqueWords.Add(word);
                    counts.Add(1);
                }
                else
                {
                    counts[index]++;
                }
            }
        });

        long dictionaryFrequencyMs = MeasureMilliseconds(() =>
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (counts.TryGetValue(word, out int count))
                    counts[word] = count + 1;
                else
                    counts.Add(word, 1);
            }
        });

        // 3. Вилучення дублікатів із 20 000 чисел.
        List<int> duplicateData = CreateRandomList(20000, 1, 10000, 30);
        long listUniqueMs = MeasureMilliseconds(() => UniqueListSlow(duplicateData));
        long hashUniqueMs = MeasureMilliseconds(() => new HashSet<int>(duplicateData));

        // 4. Спільні елементи двох наборів по 5 000 елементів.
        List<int> first = CreateRandomList(5000, 1, 10000, 40);
        List<int> second = CreateRandomList(5000, 1, 10000, 41);
        long listCommonMs = MeasureMilliseconds(() => CommonListSlow(first, second));
        long setCommonMs = MeasureMilliseconds(() =>
        {
            HashSet<int> set = new HashSet<int>(second);
            HashSet<int> common = new HashSet<int>();
            foreach (int value in first)
                if (set.Contains(value)) common.Add(value);
        });

        PrintBenchmark("Перевірка наявності", listMembershipMs, setMembershipMs);
        PrintBenchmark("Підрахунок частот слів", listFrequencyMs, dictionaryFrequencyMs);
        PrintBenchmark("Вилучення дублікатів", listUniqueMs, hashUniqueMs);
        PrintBenchmark("Спільні елементи наборів", listCommonMs, setCommonMs);
        Console.WriteLine($"Перевірки наявності: список знайшов {membershipHits}, HashSet — {setHits}.");
        Console.WriteLine("Час залежить від комп'ютера та запуску. Для таблиці README використовуйте саме ці результати.");
    }

    static void PrintBenchmark(string operation, long listMs, long hashMs)
    {
        double ratio = hashMs == 0
            ? (listMs == 0 ? 1.0 : double.PositiveInfinity)
            : (double)listMs / hashMs;

        string ratioText = double.IsInfinity(ratio) ? "дуже велике" : ratio.ToString("F2") + "x";
        Console.WriteLine($"{operation}: список {listMs} мс; словник/множина {hashMs} мс; виграш {ratioText}");
    }
}
