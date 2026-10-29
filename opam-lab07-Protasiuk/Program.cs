using System;
using System.Collections.Generic;
using OpamLab07;

const int N = 20;
const int K = 3;
const int a = 2, b = 0, c = 0;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine($"N = {N}, K = {K}, a = {a}, b = {b}, c = {c}");
Console.WriteLine("\n=== Дані ===");

var random = new Random(N);
var numbers = new List<int>();
for (int i = 0; i < 20; i++)
    numbers.Add(random.Next(-K, K + 1));

var words = new List<string>
{
    "сонце", "комп'ютер", "кібер-безпека", "алгоритм", "делегат",
    "код", "узагальнення", "рядок-тест", "книга", "програмування",
    "CSharp", "навчання", "дані", "інтерфейс"
};

var students = new List<Student>
{
    new("Протасюк Вадим", "КН-3/1", 88),
    new("Коваль Андрій", "КН-3/1", 72),
    new("Мельник Олена", "КН-2/2", 95),
    new("Шевчук Ірина", "КН-2/2", 59),
    new("Бондар Максим", "КН-3/1", 81),
    new("Ткаченко Софія", "КН-1/1", 67),
    new("Романюк Назар", "КН-2/2", 90),
    new("Петренко Марія", "КН-1/1", 44),
    new("Лисенко Артем", "КН-3/1", 76),
    new("Гриценко Анна", "КН-1/1", 100)
};

Console.WriteLine("Числа: " + string.Join(", ", numbers));
Console.WriteLine("Рядки: " + string.Join(", ", words));
Console.WriteLine("Студенти: " + students.Count);

// Завдання 1: a = 2
var filteredNumbers = Algorithms.Filter(numbers, x => x <= 0);
var filteredWords = Algorithms.Filter(words, s => s.Contains('-'));
var filteredStudents = Algorithms.Filter(students, s => s.Score >= 60 && s.Score <= 89);
Console.WriteLine("\n=== Завдання 1: Filter ===");
PrintList("Числа <= 0", filteredNumbers);
PrintList("Рядки з дефісом", filteredWords);
PrintList("Студенти з балом 60..89", filteredStudents);
Console.WriteLine("Тіло Filter змінювалося: 0 разів.");

// Завдання 2: b = 0
var squares = Algorithms.Map(numbers, x => x * x);
var lengths = Algorithms.Map(words, s => s.Length);
var surnames = Algorithms.Map(students, s => s.Surname);
Console.WriteLine("\n=== Завдання 2: Map ===");
PrintList("Квадрати чисел", squares);
PrintList("Довжини рядків", lengths);
PrintList("Прізвища студентів", surnames);

// Завдання 3: сума, добуток, максимум, кількість <= 0
int sum = Algorithms.Aggregate(numbers, 0, (state, x) => state + x);
System.Numerics.BigInteger product = Algorithms.Aggregate(numbers, System.Numerics.BigInteger.One, (state, x) => state * x);
int maximum = numbers.Count == 0
    ? throw new InvalidOperationException("Максимум порожнього списку не визначений.")
    : Algorithms.Aggregate(numbers, int.MinValue, (state, x) => x > state ? x : state);
int countNonPositive = Algorithms.Aggregate(numbers, 0, (state, x) => state + (x <= 0 ? 1 : 0));
Console.WriteLine("\n=== Завдання 3: Aggregate ===");
Console.WriteLine($"Сума: {sum}");
Console.WriteLine($"Добуток: {product}");
Console.WriteLine($"Максимум: {maximum}");
Console.WriteLine($"Кількість чисел <= 0: {countNonPositive}");

// Завдання 4: групування
Console.WriteLine("\n=== Завдання 4: GroupBy ===");
ShowGroups("Студенти за групою", Algorithms.GroupBy(students, s => s.Group));
ShowGroups("Студенти за першою літерою прізвища", Algorithms.GroupBy(students, s => s.Surname.Length > 0 ? s.Surname[0].ToString() : ""));
ShowGroups("Студенти за діапазоном балів", Algorithms.GroupBy(students, s =>
    s.Score <= 59 ? "0–59" : s.Score <= 74 ? "60–74" : s.Score <= 89 ? "75–89" : "90–100"));
ShowGroups("Рядки за довжиною", Algorithms.GroupBy(words, s => s.Length));

// Завдання 5: захоплення змінних
Console.WriteLine("\n=== Завдання 5: Захоплення змінних ===");
int threshold = K;
Func<int, bool> aboveThreshold = x => x > threshold;
threshold = 1000;
Console.WriteLine($"1. Поріг змінено на {threshold}; перевірка 10 > поріг: {aboveThreshold(10)}");

var forFunctions = new List<Func<int>>();
for (int i = 0; i < 3; i++)
    forFunctions.Add(() => i);
Console.WriteLine("2. for: " + string.Join(", ", Algorithms.Map(forFunctions, f => f())));

var foreachFunctions = new List<Func<int>>();
foreach (int value in new[] { 0, 1, 2 })
    foreachFunctions.Add(() => value);
Console.WriteLine("3. foreach: " + string.Join(", ", Algorithms.Map(foreachFunctions, f => f())));

Console.WriteLine("\\nГотово.");

static void PrintList<T>(string title, List<T> items)
{
    Console.WriteLine($"{title} (розмір {items.Count}): {string.Join(", ", items)}");
}

static void ShowGroups<T, TKey>(string title, Dictionary<TKey, List<T>> groups) where TKey : notnull
{
    Console.WriteLine(title + ":");
    foreach (var pair in groups)
        Console.WriteLine($"  {pair.Key}: {pair.Value.Count}");
}
