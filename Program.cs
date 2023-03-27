using System.Security.Cryptography;
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

        static void PrintEnclosure(IEnclosure enclosure)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{enclosure.name}, [");
            sb.AppendJoin(", ", enclosure.animals.Select(animal => animal.name));
            sb.Append($"], {enclosure.employee.name} {enclosure.employee.surname}");
            Console.WriteLine(sb);
        }
        static void PrintAnimal(IAnimal animal) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{animal.name}, {animal.age}, {animal.species.name}");
            Console.WriteLine(sb);
        }
        static void PrintSpecies(ISpecies species) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{species.name}, [");
            sb.AppendJoin(", ", species.favouriteFoods.Select(specie => specie.name));
            sb.Append($"]");
            Console.WriteLine(sb);
        }
        static void PrintEmployee(IEmployee employee) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{employee.name}, {employee.surname}, {employee.age}, [");
            sb.AppendJoin(", ", employee.enclosures.Select(enclosure => enclosure.name));
            sb.Append($"]");
            Console.WriteLine(sb);
        }
        static void PrintVisitor(IVisitor visitor) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{visitor.name}, {visitor.surname}, [");
            sb.AppendJoin(", ", visitor.visitedEnclosures.Select(enclosure => enclosure.name));
            sb.Append($"]");
            Console.WriteLine(sb);
        }

        static void Task2(Dictionary<string, IEnclosure> enclosures,
            Dictionary<string, ISpecies> species, Dictionary<string, IAnimal> animals,
            Dictionary<string, IEmployee> employees, Dictionary<string, IVisitor> visitors)
        {
            foreach(var enclosure in enclosures.Values)
            {
                int age = 0;
                int count = 0;
                List<IAnimal> toPrint = new List<IAnimal>();
                foreach (var animal in animals.Values)
                    foreach(var specie in enclosure.animals)
                    if (animal.species.name == specie.name)
                    {
                        age += animal.age;
                        count++;
                        toPrint.Add(animal);
                    }
                if (count != 0 && age / count >= 3) continue;
                StringBuilder sb = new StringBuilder();
                sb.Append(enclosure.name);
                sb.Append(", [");
                sb.AppendJoin(", ", toPrint.Select(animal => $"({animal.name}, {animal.age}, {animal.species.name})"));
                sb.Append("]");
                Console.WriteLine(sb);
            }
        }

        static void MainFormatOperations()
        {
            Dictionary<string, MainRepresentation.Enclosure> enclosures = new Dictionary<string, MainRepresentation.Enclosure>();
            Dictionary<string, MainRepresentation.Animal> animals = new Dictionary<string, MainRepresentation.Animal>();
            Dictionary<string, MainRepresentation.Species> species = new Dictionary<string, MainRepresentation.Species>();
            Dictionary<string, MainRepresentation.Employee> employees = new Dictionary<string, MainRepresentation.Employee>();
            Dictionary<string, MainRepresentation.Visitor> visitors = new Dictionary<string, MainRepresentation.Visitor>();

            //Creating objects
            foreach (var specie in SpeciesData)
                species[specie.Key] = 
                    new MainRepresentation.Species(specie.Value.name,
                    new List<MainRepresentation.Species>());
            
            foreach (var animal in AnimalData)
                animals[animal.Key] =
                    new MainRepresentation.Animal(animal.Value.name,
                    animal.Value.age, species[animal.Value.species]);

            foreach (var employee in EmployeeData)
                employees[employee.Key] =
                    new MainRepresentation.Employee(employee.Value.name,
                    employee.Value.surname, employee.Value.age,
                    new List<MainRepresentation.Enclosure>());

            foreach (var visitor in VisitorData)
                visitors[visitor.Key] =
                    new MainRepresentation.Visitor(visitor.Value.name,
                    visitor.Value.surname, new List<MainRepresentation.Enclosure>());

            foreach (var enclosure in EnclosureData)
                enclosures[enclosure.Key] = 
                    new MainRepresentation.Enclosure(enclosure.Value.name,
                    new List<MainRepresentation.Species>(), 
                    employees[enclosure.Value.employee]);
            
            // Appending lists
            foreach (var enclosure in EnclosureData)
                foreach (var animal in enclosure.Value.animals)
                    enclosures[enclosure.Key].animals.Add(species[animal]);

            foreach (var visitor in VisitorData)
                foreach (var enclosure in visitor.Value.enclosures)
                    visitors[visitor.Key].visitedEnclosures.Add(enclosures[enclosure]);

            foreach (var specie in SpeciesData)
                foreach (var food in specie.Value.foods)
                    species[specie.Key].favouriteFoods.Add(species[food]);

            foreach (var employee in EmployeeData)
                foreach (var enclosure in employee.Value.enclosures)
                    employees[employee.Key].enclosures.Add(enclosures[enclosure]);

            //Printing
            Console.WriteLine("Enclosures: ");
            foreach (var enclosure in enclosures.Values)
                PrintEnclosure(new MainRepresentation.EnclosureAdapter(enclosure));
            Console.WriteLine();
            Console.WriteLine("Animals: ");
            foreach (var animal in animals.Values)
                PrintAnimal(new MainRepresentation.AnimalAdapter(animal));
            Console.WriteLine();
            Console.WriteLine("Species: ");
            foreach (var specie in species.Values) 
                PrintSpecies(new MainRepresentation.SpeciesAdapter(specie));
            Console.WriteLine();
            Console.WriteLine("Employees: ");
            foreach (var employee in employees.Values)
                PrintEmployee(new MainRepresentation.EmployeeAdapter(employee));
            Console.WriteLine();
            Console.WriteLine("Visitors: ");
            foreach (var visitor in visitors.Values)
                PrintVisitor(new MainRepresentation.VisitorAdapter(visitor));
            Console.WriteLine();

            //Adapters and task2
            Dictionary<string, IEnclosure> EnclosureAdapters = new Dictionary<string, IEnclosure>();
            Dictionary<string, IAnimal> AnimalAdapters = new Dictionary<string, IAnimal>();
            Dictionary<string, ISpecies> SpeciesAdapters = new Dictionary<string, ISpecies>();
            Dictionary<string, IEmployee> EmployeeAdapters = new Dictionary<string, IEmployee>();
            Dictionary<string, IVisitor> VisitorAdapters = new Dictionary<string, IVisitor>();

            foreach (var enclosure in enclosures)
                EnclosureAdapters.Add(enclosure.Key, new MainRepresentation.EnclosureAdapter(enclosure.Value));
            foreach (var animal in animals)
                AnimalAdapters.Add(animal.Key, new MainRepresentation.AnimalAdapter(animal.Value));
            foreach (var specie in species)SpeciesAdapters.Add(specie.Key, new MainRepresentation.SpeciesAdapter(specie.Value));
            foreach (var employee in employees)EmployeeAdapters.Add(employee.Key, new MainRepresentation.EmployeeAdapter(employee.Value));
            foreach (var visitor in visitors) VisitorAdapters.Add(visitor.Key, new MainRepresentation.VisitorAdapter(visitor.Value));

            Task2(EnclosureAdapters , SpeciesAdapters, AnimalAdapters, EmployeeAdapters, VisitorAdapters);
            Console.WriteLine();
        }
        static void SecondFormatOperations() 
        {
            Dictionary<string, SecondRepresentation.Enclosure> enclosures = new Dictionary<string, SecondRepresentation.Enclosure>();
            Dictionary<string, SecondRepresentation.Animal> animals = new Dictionary<string, SecondRepresentation.Animal>();
            Dictionary<string, SecondRepresentation.Employee> employees = new Dictionary<string, SecondRepresentation.Employee>();
            Dictionary<string, SecondRepresentation.Species> species = new Dictionary<string, SecondRepresentation.Species>();
            Dictionary<string, SecondRepresentation.Visitor> visitors = new Dictionary<string, SecondRepresentation.Visitor>();

            //Creating Dictionaries of objects
            foreach (var enclosureData in EnclosureData)
            {
                var enclosure = enclosureData.Value;
                StringBuilder sb = new StringBuilder();

                sb.Append($"{enclosure.name}" +
                    $"@{enclosure.employee.Split(' ')[0]} {enclosure.employee.Split(' ')[1]}" +
                    $",");
                sb.AppendJoin(",", enclosure.animals);
                enclosures[enclosureData.Key] = new SecondRepresentation.Enclosure(sb.ToString());
            }

            foreach (var animal in AnimalData)
                animals[animal.Key] = 
                    new SecondRepresentation.Animal($"{animal.Value.name}" +
                    $"({animal.Value.age})%{animal.Value.species}");

            foreach (var keys in SpeciesData)
            {
                var specie = keys.Value;
                StringBuilder sb = new StringBuilder();
                sb.Append($"{specie.name}$");
                sb.AppendJoin(",", specie.foods);
                species[keys.Key] = new SecondRepresentation.Species(sb.ToString());
            }

            foreach (var keys in EmployeeData)
            {
                var employee = keys.Value;
                StringBuilder sb = new StringBuilder();
                sb.Append($"{employee.name} {employee.surname}({employee.age})@");
                sb.AppendJoin(",", employee.enclosures);
                employees[keys.Key] = new SecondRepresentation.Employee(sb.ToString());
            }

            foreach (var keys in VisitorData)
            {
                var visitor = keys.Value;
                StringBuilder sb = new StringBuilder();
                sb.Append($"{visitor.name} {visitor.surname}@");
                sb.AppendJoin(",", visitor.enclosures);
                visitors[keys.Key] = new SecondRepresentation.Visitor(sb.ToString());
            }


            //Printing
            Console.WriteLine("Enclosures: ");
            foreach (var enclosure in enclosures.Values)
                PrintEnclosure(new SecondRepresentation.EnclosureAdapter(enclosure, 
                    species, employees, enclosures));
            Console.WriteLine();
            Console.WriteLine("Animals: ");
            foreach (var animal in animals.Values)
                PrintAnimal(new SecondRepresentation.AnimalAdapter(animal, species));
            Console.WriteLine();
            Console.WriteLine("Species: ");
            foreach (var specie in species.Values)
                PrintSpecies(new SecondRepresentation.SpeciesAdapter(specie, species));
            Console.WriteLine();
            Console.WriteLine("Employees: ");
            foreach (var employee in employees.Values)
                PrintEmployee(new SecondRepresentation.EmployeeAdapter(
                    employee, species, employees, enclosures));
            Console.WriteLine();
            Console.WriteLine("Visitors: ");
            foreach (var visitor in visitors.Values)
                PrintVisitor(new SecondRepresentation.VisitorAdapter(visitor,
                    employees, species, enclosures));
            Console.WriteLine();

            //Adapters and task2
            Dictionary<string, IEnclosure> EnclosureAdapters = new Dictionary<string, IEnclosure>();
            Dictionary<string, IAnimal> AnimalAdapters = new Dictionary<string, IAnimal>();
            Dictionary<string, ISpecies> SpeciesAdapters = new Dictionary<string, ISpecies>();
            Dictionary<string, IEmployee> EmployeeAdapters = new Dictionary<string, IEmployee>();
            Dictionary<string, IVisitor> VisitorAdapters = new Dictionary<string, IVisitor>();

            foreach (var enclosure in enclosures)
                EnclosureAdapters.Add(enclosure.Key, 
                    new SecondRepresentation.EnclosureAdapter(
                        enclosure.Value, species, employees, enclosures));
            foreach (var animal in animals)
                AnimalAdapters.Add(animal.Key, 
                    new SecondRepresentation.AnimalAdapter(animal.Value, species));
            foreach (var specie in species) 
                SpeciesAdapters.Add(specie.Key, 
                    new SecondRepresentation.SpeciesAdapter(specie.Value, species));
            foreach (var employee in employees) 
                EmployeeAdapters.Add(employee.Key, 
                    new SecondRepresentation.EmployeeAdapter(employee.Value, species, employees, enclosures));
            foreach (var visitor in visitors) 
                VisitorAdapters.Add(visitor.Key, 
                    new SecondRepresentation.VisitorAdapter(visitor.Value, employees, species, enclosures));

            Task2(EnclosureAdapters, SpeciesAdapters, AnimalAdapters, EmployeeAdapters, VisitorAdapters);
            Console.WriteLine();

        }
        static void ThirdFormatOperations() { }
        static void Main()
        {
            Console.WriteLine("Main representation: ");
            MainFormatOperations();
            Console.WriteLine("Second representations: ");
            SecondFormatOperations();
            Console.WriteLine("Third representations: ");
            ThirdFormatOperations();
        }

    }
}