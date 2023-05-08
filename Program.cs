using Collections;
using System.Text;

namespace Zoo
{
    class Zoo
    {
        static Dictionary<string, (string name, string[] animals, string employee)> EnclosureData = new Dictionary<string, (string, string[], string)>
        {
            ["311"] = ("311", new string[] { "Arch", "Ubuntu", "Fedora", "Conda", "Samuel", "Jett" }, "Ricardo Stallmano"),
            ["Break"] = ("Break", new string[] { "Arrow", "Andy", "Harold", "Ryan" }, "Steve Irvin"),
            ["Jurasic Park"] = ("Jurasic Park", new string[] { "Jenkins", "Kaka", "Ada", "Claire" }, "Steve Irvin")
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

        static Dictionary<string, IEnclosure> MainRepresentationEnclosures = new Dictionary<string, IEnclosure>();
        static Dictionary<string, IAnimal> MainRepresentationAnimals = new Dictionary<string, IAnimal>();
        static Dictionary<string, ISpecies> MainRepresentationSpecies = new Dictionary<string, ISpecies>();
        static Dictionary<string, IEmployee> MainRepresentationEmployees = new Dictionary<string, IEmployee>();
        static Dictionary<string, IVisitor> MainRepresentationVisitors = new Dictionary<string, IVisitor>();

        static Dictionary<string, IEnclosure> SecondRepresentationEnclosures = new Dictionary<string, IEnclosure>();
        static Dictionary<string, IAnimal> SecondRepresentationAnimals = new Dictionary<string, IAnimal>();
        static Dictionary<string, ISpecies> SecondRepresentationSpecies = new Dictionary<string, ISpecies>();
        static Dictionary<string, IEmployee> SecondRepresentationEmployees = new Dictionary<string, IEmployee>();
        static Dictionary<string, IVisitor> SecondRepresentationVisitors = new Dictionary<string, IVisitor>();

        static Dictionary<int, IEnclosure> ThirdRepresentationEnclosures = new Dictionary<int, IEnclosure>();
        static Dictionary<int, IAnimal> ThirdRepresentationAnimals = new Dictionary<int, IAnimal>();
        static Dictionary<int, ISpecies> ThirdRepresentationSpecies = new Dictionary<int, ISpecies>();
        static Dictionary<int, IEmployee> ThirdRepresentationEmployees = new Dictionary<int, IEmployee>();
        static Dictionary<int, IVisitor> ThirdRepresentationVisitors = new Dictionary<int, IVisitor>();

        public static void PrintObject(object obj)
        {
            if (obj is IEnclosure) PrintEnclosure((IEnclosure)obj);
            else if (obj is IEmployee) PrintEmployee((IEmployee)obj);
            else if (obj is IAnimal) PrintAnimal((IAnimal)obj);
            else if (obj is ISpecies) PrintSpecies((ISpecies)obj);
            else if (obj is IVisitor) PrintVisitor((IVisitor)obj);
            else throw new InvalidOperationException();
        }
        public static void PrintEnclosure(IEnclosure enclosure)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{enclosure.name}, [");
            sb.AppendJoin(", ", enclosure.animals.Select(animal => animal.name));
            sb.Append($"], {enclosure.employee.name} {enclosure.employee.surname}");
            Console.WriteLine(sb);
        }
        public static void PrintAnimal(IAnimal animal)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{animal.name}, {animal.age}, {animal.species.name}");
            Console.WriteLine(sb);
        }
        public static void PrintSpecies(ISpecies species)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{species.name}, [");
            sb.AppendJoin(", ", species.favouriteFoods.Select(specie => specie.name));
            sb.Append($"]");
            Console.WriteLine(sb);
        }
        public static void PrintEmployee(IEmployee employee)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{employee.name}, {employee.surname}, {employee.age}, [");
            sb.AppendJoin(", ", employee.enclosures.Select(enclosure => enclosure.name));
            sb.Append($"]");
            Console.WriteLine(sb);
        }
        public static void PrintVisitor(IVisitor visitor)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{visitor.name}, {visitor.surname}, [");
            sb.AppendJoin(", ", visitor.visitedEnclosures.Select(enclosure => enclosure.name));
            sb.Append($"]");
            Console.WriteLine(sb);
        }

        static void CreateMainRepresentationObjects()
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
                    new List<MainRepresentation.Animal>(), 
                    employees[enclosure.Value.employee]);
            
            // Appending lists
            foreach (var enclosure in EnclosureData)
                foreach (var animal in enclosure.Value.animals)
                    enclosures[enclosure.Key].animals.Add(animals[animal]);

            foreach (var visitor in VisitorData)
                foreach (var enclosure in visitor.Value.enclosures)
                    visitors[visitor.Key].visitedEnclosures.Add(enclosures[enclosure]);

            foreach (var specie in SpeciesData)
                foreach (var food in specie.Value.foods)
                    species[specie.Key].favouriteFoods.Add(species[food]);

            foreach (var employee in EmployeeData)
                foreach (var enclosure in employee.Value.enclosures)
                    employees[employee.Key].enclosures.Add(enclosures[enclosure]);

            //Adapters and task2
            foreach (var enclosure in enclosures)
                MainRepresentationEnclosures.Add(enclosure.Key, new MainRepresentation.EnclosureAdapter(enclosure.Value));
            foreach (var animal in animals)
                MainRepresentationAnimals.Add(animal.Key, new MainRepresentation.AnimalAdapter(animal.Value));
            foreach (var specie in species)MainRepresentationSpecies.Add(specie.Key, new MainRepresentation.SpeciesAdapter(specie.Value));
            foreach (var employee in employees)MainRepresentationEmployees.Add(employee.Key, new MainRepresentation.EmployeeAdapter(employee.Value));
            foreach (var visitor in visitors) MainRepresentationVisitors.Add(visitor.Key, new MainRepresentation.VisitorAdapter(visitor.Value));
        }
        static void CreateSecondRepresentationObjects() 
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

            //Adapters and task2
            foreach (var enclosure in enclosures)
                SecondRepresentationEnclosures.Add(enclosure.Key, 
                    new SecondRepresentation.EnclosureAdapter(
                        enclosure.Value, species, employees, enclosures, animals));
            foreach (var animal in animals)
                SecondRepresentationAnimals.Add(animal.Key, 
                    new SecondRepresentation.AnimalAdapter(animal.Value, species));
            foreach (var specie in species) 
                SecondRepresentationSpecies.Add(specie.Key, 
                    new SecondRepresentation.SpeciesAdapter(specie.Value, species));
            foreach (var employee in employees) 
                SecondRepresentationEmployees.Add(employee.Key, 
                    new SecondRepresentation.EmployeeAdapter(employee.Value, species, employees, enclosures, animals));
            foreach (var visitor in visitors) 
                SecondRepresentationVisitors.Add(visitor.Key, 
                    new SecondRepresentation.VisitorAdapter(visitor.Value, employees, species, enclosures, animals));
        }
        static void CreateThirdRepresentationObjects() 
        {
            Dictionary<int, ThirdRepresentation.Enclosure> enclosures = new Dictionary<int, ThirdRepresentation.Enclosure>();
            Dictionary<int, ThirdRepresentation.Animal> animals = new Dictionary<int, ThirdRepresentation.Animal>();
            Dictionary<int, ThirdRepresentation.Employee> employees = new Dictionary<int, ThirdRepresentation.Employee>();
            Dictionary<int, ThirdRepresentation.Species> species = new Dictionary<int, ThirdRepresentation.Species>();
            Dictionary<int, ThirdRepresentation.Visitor> visitors = new Dictionary<int, ThirdRepresentation.Visitor>();

            //Create Objects
            int id = 0;
            foreach (var speciesData in SpeciesData.Values) 
            {
                species[id] = new ThirdRepresentation.Species(id, new Dictionary<string, string>());
                species[id].data["name"] = speciesData.name;
                species[id].data["favouriteFoods.Size()"] = speciesData.foods.Length.ToString();
                for(int i = 0; i < speciesData.foods.Length; i++)
                    species[id].data[$"favouriteFoods[{i}]"] = speciesData.foods[i];
                id++;
            }

            foreach (var specie in species)
                for (int i = 0; i < int.Parse(specie.Value.data["favouriteFoods.Size()"]); i++)
                    specie.Value.data[$"favouriteFoods[{i}]"] = species.Where(spe => spe.Value.data["name"] ==
                    specie.Value.data[$"favouriteFoods[{i}]"]).First().Key.ToString();

            id = 0;
            foreach (var animalData in AnimalData.Values)
            {
                animals[id] = new ThirdRepresentation.Animal(id, new Dictionary<string, string>());
                animals[id].data["name"] = animalData.name;
                animals[id].data["age"] = animalData.age.ToString();
                animals[id].data["species"] = 
                    species.Where(spe => spe.Value.data["name"] == animalData.species)
                    .First().Key.ToString();
                id++;
            }

            id = 0;
            foreach (var enclosureData in EnclosureData.Values)
            {
                enclosures[id] = new ThirdRepresentation.Enclosure(id, new Dictionary<string, string>());
                enclosures[id].data["name"] = enclosureData.name;
                enclosures[id].data["animals.Size()"] = enclosureData.animals.Length.ToString();
                enclosures[id].data["employee"] = enclosureData.employee;
                for(int i = 0; i < enclosureData.animals.Length; i++)
                    enclosures[id].data[$"animals[{i}]"] = 
                        animals.Where(spe => spe.Value.data["name"] == enclosureData.animals[i]).First().Key.ToString();
                id++;
            }

            id = 0;
            foreach (var employeeData in EmployeeData.Values) 
            {
                employees[id] = new ThirdRepresentation.Employee(id, new Dictionary<string, string>());
                employees[id].data["name"] = employeeData.name;
                employees[id].data["surname"] = employeeData.surname;
                employees[id].data["age"] = employeeData.age.ToString();
                employees[id].data["enclosures.Size()"] = employeeData.enclosures.Length.ToString();
                for(int i = 0; i < employeeData.enclosures.Length; i++)
                    employees[id].data[$"enclosures[{i}]"] = 
                        enclosures.Where(enc => (enc.Value.data["name"] == employeeData.enclosures[i]))
                        .First().Key.ToString();
                id++;
            }

            id = 0;
            foreach(var visitor in VisitorData.Values)
            {
                visitors[id] = new ThirdRepresentation.Visitor(id, new Dictionary<string, string>());
                visitors[id].data["name"] = visitor.name;
                visitors[id].data["surname"] = visitor.surname;
                visitors[id].data["visitedEnclosures.Size()"] = visitor.enclosures.Length.ToString();
                for (int i = 0; i < visitor.enclosures.Length; i++)
                    visitors[id].data[$"visitedEnclosures[{i}]"] =
                        enclosures.Where(enc => (enc.Value.data["name"] == visitor.enclosures[i]))
                        .First().Key.ToString();
                id++;
            }

            foreach (var enclosure in enclosures)
                enclosure.Value.data["employee"] = employees.Where(emp =>
                (emp.Value.data["name"] + " " + emp.Value.data["surname"]) == enclosure.Value.data["employee"])
                    .First().Key.ToString();

            //Adapters
            foreach (var enclosure in enclosures)
                ThirdRepresentationEnclosures.Add(enclosure.Key,
                    new ThirdRepresentation.EnclosureAdapter(
                        enclosure.Value, species, employees, enclosures, animals));
            foreach (var animal in animals)
                ThirdRepresentationAnimals.Add(animal.Key,
                    new ThirdRepresentation.AnimalAdapter(animal.Value, species));
            foreach (var specie in species)
                ThirdRepresentationSpecies.Add(specie.Key,
                    new ThirdRepresentation.SpeciesAdapter(specie.Value, species));
            foreach (var employee in employees)
                ThirdRepresentationEmployees.Add(employee.Key,
                    new ThirdRepresentation.EmployeeAdapter(employee.Value, species, employees, enclosures, animals));
            foreach (var visitor in visitors)
                ThirdRepresentationVisitors.Add(visitor.Key,
                    new ThirdRepresentation.VisitorAdapter(visitor.Value, employees, species, enclosures, animals));
        }

        static void Main()
        {
            CreateMainRepresentationObjects();
            CreateSecondRepresentationObjects();
            CreateThirdRepresentationObjects();
            App app = new App();
            app.Init(MainRepresentationEnclosures, MainRepresentationAnimals, MainRepresentationSpecies,
                MainRepresentationEmployees, MainRepresentationVisitors);
            app.Start();
        }
    }
}