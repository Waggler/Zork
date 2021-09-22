public class Room
{
    public string Name { get; }

    public string Description { get; set; }

    public override string ToString() => Name;

    public Room(string name, string desciption = "")
    {
        Name = name;
        Description = desciption;
    }
}