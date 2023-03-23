using System.Text;

namespace Zoo
{
    class Zoo
    {
        static Dictionary<string, (string name, string[] animals, string employee)> EnclosureData = new Dictionary<string, (string, string[], string)>
        {
            ["311"] = ("311", new string[] { "Penguin", "Python", "Panda" }, "Ricardo Stallmano"),
            ["Break"] = ("Break", new string[] { "Cats", "Gopher", "Meerkat" }, "Steve Irvin"),
            ["Jurasic Park"] = ("Jurasic Park", new string[] { "Kakapo", "Bengal Tiger", "Dungeness Crab" }, "Steve Irvin")
        };
        static Dictionary<string, (string name, int age, string species)> AnimalData = new Dictionary<string, (string, int, string)>
        {
            ["Harold"] = ("Harold", 2, "Meerkat"),
            ["Ryan"] = ("Ryan", 1, "Meerkat"),
            ["Jenkins"] = ("Jenkins", 15, "Kakapo"),
            ["Kaka"] = ("Kaka", 10, "Kakapo"),
            ["Ada"] = ("Ada", 13, "Bengal Tiger"),
            ["Jett"] = ("Jett", 2, "Panda"),
            ["Conda"] = ("Conda", 4, "Python"),
            ["Samuel"] = ("Samuel", 2, "Python"),
            ["Claire"] = ("Claire", 2, "Dungeness Crab"),
            ["Andy"] = ("Andy", 3, "Gopher"),
            ["Arrow"] = ("Arrow", 5, "Cats"),
            ["Arch"] = ("Arch", 1, "Penguin"),
            ["Ubuntu"] = ("Ubuntu", 1, "Penguin"),
            ["Fedora"] = ("Fedora", 1, "Penguin")
        };
        static Dictionary<string, (string name, string[] foods)> SpeciesData = new Dictionary<string, (string, string[])>
        {
            ["Meerkat"] = ("Meerkat", new string[] { "Meerkat" }),
            ["Kakapo"] = ("Kakapo", new string[] { }),
            ["Bengal Tiger"] = ("Bengal Tiger", new string[] { "Panda", "Gopher", "Cats" }),
            ["Panda"] = ("Panda", new string[] { }),
            ["Python"] = ("Python", new string[] { "Panda", "Bengal Tiger" }),
            ["Dungeness Crab"] = ("Dungeness Crab", new string[] { "Python" }),
            ["Gopher"] = ("Gopher", new string[] { }),
            ["Cats"] = ("Cats", new string[] { "Gopher" }),
            ["Penguin"] = ("Penguin", new string[] { "Bengal Tiger" })
        };
        static Dictionary<string, (string name, string surname, int age, string[] enclosures)> EmployeeData = new Dictionary<string, (string, string, int, string[])>
        {
            ["Ricardo Stallmano"] = ("Ricardo", "Stallmano", 73, new string[] { "311" }),
            ["Steve Irvin"] = ("Steve", "Irvin", 43, new string[] { "Break", "Jurasic Park" })
        };
        static Dictionary<string, (string name, string surname, string[] enclosures)> VisitorData = new Dictionary<string, (string, string, string[])>
        {
            ["Tomas German"] = ("Tomas", "German", new string[] { "311", "Break" }),
            ["Silvester Ileen"] = ("Silvester", "Ileen", new string[] { "Jurasic Park" }),
            ["Basil Bailey"] = ("Basil", "Bailey", new string[] { "311", "Jurasic Park" }),
            ["Ryker Polly"] = ("Ryker", "Polly", new string[] { "Break" })
        };

        static void Main()
        {

        }

    }
}