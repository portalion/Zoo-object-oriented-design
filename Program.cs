using System.Text;
using UserRepresentation;
using DBR = DatabaseRepresentation;

namespace Zoo
{
    public class Zoo
    {
        static public void Main()
        {
            Dictionary<string, Animal> UserRepresentationAnimals = new Dictionary<string, Animal>();
            Dictionary<string, Enclosure> UserRepresentationEnclosures = new Dictionary<string, Enclosure>();
            Dictionary<string, Species> UserRepresentationSpecies = new Dictionary<string, Species>();
            Dictionary<string, Employee> UserRepresentationEmployees = new Dictionary<string, Employee>();
            Dictionary<string, Visitor> UserRepresentationVisitors = new Dictionary<string, Visitor>();

            Dictionary<string, (string name, string[] animals, string employee)> EnclosureData = new Dictionary<string, (string, string[], string)>
            {
                ["311"] = ("311", new string[] { "Penguin", "Python", "Panda" }, "Ricardo Stallmano"),
                ["Break"] = ("Break", new string[] { "Cats", "Gopher", "Meerkat" }, "Steve Irvin"),
                ["Jurasic Park"] = ("Jurasic Park", new string[] { "Kakapo", "Bengal Tiger", "Dungeness Crab" }, "Steve Irvin")
            };

            Dictionary<string, (string name, int age, string species)> AnimalData = new Dictionary<string, (string, int, string)>
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

            Dictionary<string, (string name, string[] foods)> SpeciesData = new Dictionary<string, (string, string[])>
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

            Dictionary<string, (string name, string surname, int age, string[] enclosures)> EmployeeData = new Dictionary<string, (string, string, int, string[])>
            {
                ["Ricardo Stallmano"] = ("Ricardo", "Stallmano", 73, new string[] { "311" }),
                ["Steve Irvin"] = ("Steve", "Irvin", 43, new string[] { "Break", "Jurasic Park" })
            };

            Dictionary<string, (string name, string surname, string[] enclosures)> VisitorData = new Dictionary<string, (string, string, string[])>
            {
                ["Tomas German"] = ("Tomas", "German", new string[] { "311", "Break" }),
                ["Silvester Ileen"] = ("Silvester", "Ileen", new string[] { "Jurasic Park" }),
                ["Basil Bailey"] = ("Basil", "Bailey", new string[] { "311", "Jurasic Park" }),
                ["Ryker Polly"] = ("Ryker", "Polly", new string[] { "Break" })
            };


            //Species
            foreach (var specie in SpeciesData)
                UserRepresentationSpecies[specie.Key] = new Species(specie.Value.name, null);

            foreach (var specie in SpeciesData)
                foreach (var food in specie.Value.foods)
                    UserRepresentationSpecies[specie.Key].favouriteFoods.Add(UserRepresentationSpecies[food]);

            //Employees
            foreach (var employee in EmployeeData)
                UserRepresentationEmployees[employee.Key] = new Employee(employee.Value.name, employee.Value.surname, employee.Value.age, null);

            //Animals
            foreach (var animal in AnimalData)
                UserRepresentationAnimals[animal.Key] = new Animal(animal.Value.name, animal.Value.age, UserRepresentationSpecies[animal.Value.species]);

            //Enclosures
            foreach (var enclosure in EnclosureData)
            {
                UserRepresentationEnclosures[enclosure.Key] = new Enclosure(enclosure.Value.name, null, UserRepresentationEmployees[enclosure.Value.employee]);
                foreach (var specie in enclosure.Value.animals)
                    UserRepresentationEnclosures[enclosure.Key].animals.Add(UserRepresentationSpecies[specie]);
            }

            //Visitors
            foreach (var visitor in VisitorData)
            {
                UserRepresentationVisitors[visitor.Key] = new Visitor(visitor.Value.name, visitor.Value.surname, null);
                foreach (var enclosure in visitor.Value.enclosures)
                    UserRepresentationVisitors[visitor.Key].visitedEnclosures.Add(UserRepresentationEnclosures[enclosure]);
            }

            //Employees
            foreach (var employee in EmployeeData)
                foreach (var enclosure in employee.Value.enclosures)
                    UserRepresentationEmployees[employee.Key].enclosures.Add(UserRepresentationEnclosures[enclosure]);


            foreach (var specie in UserRepresentationEnclosures.Values)
                Console.WriteLine(specie);
            Console.WriteLine();

            foreach (var specie in UserRepresentationAnimals.Values)
                Console.WriteLine(specie);
            Console.WriteLine();

            foreach (var specie in UserRepresentationSpecies.Values)
                Console.WriteLine(specie);
            Console.WriteLine();

            foreach (var specie in UserRepresentationEmployees.Values)
                Console.WriteLine(specie);
            Console.WriteLine();

            foreach (var specie in UserRepresentationVisitors.Values)
                Console.WriteLine(specie);

            //DatabaseData

            Console.WriteLine();
            Console.WriteLine("Database data");
            Console.WriteLine();

            List<DBR.Enclosure> DatabaseRepresentationEnclosures = new List<DBR.Enclosure>();
            List<DBR.Animal> DatabaseRepresentationAnimal = new List<DBR.Animal>();
            List<DBR.Employee> DatabaseRepresentationEmployee = new List<DBR.Employee>();
            List<DBR.Species> DatabaseRepresentationSpecies = new List<DBR.Species>();
            List<DBR.Visitor> DatabaseRepresentationVisitor = new List<DBR.Visitor>();

            foreach(var enclosure in EnclosureData.Values)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append($"{enclosure.name}" +
                    $"@{enclosure.employee.Split(' ')[0]} {enclosure.employee.Split(' ')[1]}" +
                    $",");
                sb.AppendJoin(",", enclosure.animals);
                DatabaseRepresentationEnclosures.Add(new DBR.Enclosure(sb.ToString()));
            }

            foreach(var animal in AnimalData.Values)
                DatabaseRepresentationAnimal.Add(new DBR.Animal($"{animal.name}({animal.age})%{animal.species}"));
            
            foreach(var specie in SpeciesData.Values)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{specie.name}$");
                sb.AppendJoin(",", specie.foods);
                DatabaseRepresentationSpecies.Add(new DBR.Species(sb.ToString()));
            }

            foreach(var employee in EmployeeData.Values)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{employee.name} {employee.surname}({employee.age})@");
                sb.AppendJoin(",", employee.enclosures);
                DatabaseRepresentationEmployee.Add(new DBR.Employee(sb.ToString()));
            }

            foreach(var visitor in VisitorData.Values)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{visitor.name} {visitor.surname}@");
                sb.AppendJoin(",", visitor.enclosures);
                DatabaseRepresentationVisitor.Add(new DBR.Visitor(sb.ToString()));
            }

            foreach(var print in DatabaseRepresentationEnclosures)Console.WriteLine(print);
            Console.WriteLine();
            foreach(var print in DatabaseRepresentationAnimal)Console.WriteLine(print);
            Console.WriteLine();
            foreach (var print in DatabaseRepresentationSpecies) Console.WriteLine(print);
            Console.WriteLine();
            foreach(var print in DatabaseRepresentationEmployee) Console.WriteLine(print);
            Console.WriteLine();
            foreach(var print in DatabaseRepresentationVisitor) Console.WriteLine(print);

        }
    }
}
