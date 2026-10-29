namespace OpamLab07;

public class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public int Score { get; set; }

    public Student(string name, string group, int score)
    {
        Name = name;
        Group = group;
        Score = score;
    }

    public string Surname
    {
        get
        {
            string[] parts = Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[0] : Name;
        }
    }

    public override string ToString() => $"{Name} | {Group} | {Score}";
}
