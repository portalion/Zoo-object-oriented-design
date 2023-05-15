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

        public static Dictionary<string, IEnclosure> MainRepresentationEnclosures = new Dictionary<string, IEnclosure>();
        public static Dictionary<string, IAnimal> MainRepresentationAnimals = new Dictionary<string, IAnimal>();
        public static Dictionary<string, ISpecies> MainRepresentationSpecies = new Dictionary<string, ISpecies>();
        public static Dictionary<string, IEmployee> MainRepresentationEmployees = new Dictionary<string, IEmployee>();
        public static Dictionary<string, IVisitor> MainRepresentationVisitors = new Dictionary<string, IVisitor>();

        public static Dictionary<string, IEnclosure> SecondRepresentationEnclosures = new Dictionary<string, IEnclosure>();
        public static Dictionary<string, IAnimal> SecondRepresentationAnimals = new Dictionary<string, IAnimal>();
        public static Dictionary<string, ISpecies> SecondRepresentationSpecies = new Dictionary<string, ISpecies>();
        public static Dictionary<string, IEmployee> SecondRepresentationEmployees = new Dictionary<string, IEmployee>();
        public static Dictionary<string, IVisitor> SecondRepresentationVisitors = new Dictionary<string, IVisitor>();

        public static Dictionary<int, IEnclosure> ThirdRepresentationEnclosures = new Dictionary<int, IEnclosure>();
        public static Dictionary<int, IAnimal> ThirdRepresentationAnimals = new Dictionary<int, IAnimal>();
        public static Dictionary<int, ISpecies> ThirdRepresentationSpecies = new Dictionary<int, ISpecies>();
        public static Dictionary<int, IEmployee> ThirdRepresentationEmployees = new Dictionary<int, IEmployee>();
        public static Dictionary<int, IVisitor> ThirdRepresentationVisitors = new Dictionary<int, IVisitor>();

        public static Dictionary<int, ThirdRepresentation.Enclosure> thenclosures = new Dictionary<int, ThirdRepresentation.Enclosure>();
        public static Dictionary<int, ThirdRepresentation.Animal> thanimals = new Dictionary<int, ThirdRepresentation.Animal>();
        public static Dictionary<int, ThirdRepresentation.Employee> themployees = new Dictionary<int, ThirdRepresentation.Employee>();
        public static Dictionary<int, ThirdRepresentation.Species> thspecies = new Dictionary<int, ThirdRepresentation.Species>();
        public static Dictionary<int, ThirdRepresentation.Visitor> thvisitors = new Dictionary<int, ThirdRepresentation.Visitor>();

        public static void PrintToUser(IEditableByUser obj)
        {
            foreach(var field in obj.gettersForUsers)
                Console.WriteLine($"{field.Key}: {field.Value()}");
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
        //Create Objects
        int id = 0;
            foreach (var speciesData in SpeciesData.Values) 
            {
                thspecies[id] = new ThirdRepresentation.Species(id, new Dictionary<string, string>());
                thspecies[id].data["name"] = speciesData.name;
                thspecies[id].data["favouriteFoods.Size()"] = speciesData.foods.Length.ToString();
                for(int i = 0; i < speciesData.foods.Length; i++)
                    thspecies[id].data[$"favouriteFoods[{i}]"] = speciesData.foods[i];
                id++;
            }

            foreach (var specie in thspecies)
                for (int i = 0; i < int.Parse(specie.Value.data["favouriteFoods.Size()"]); i++)
                    specie.Value.data[$"favouriteFoods[{i}]"] = thspecies.Where(spe => spe.Value.data["name"] ==
                    specie.Value.data[$"favouriteFoods[{i}]"]).First().Key.ToString();

            id = 0;
            foreach (var animalData in AnimalData.Values)
            {
                thanimals[id] = new ThirdRepresentation.Animal(id, new Dictionary<string, string>());
                thanimals[id].data["name"] = animalData.name;
                thanimals[id].data["age"] = animalData.age.ToString();
                thanimals[id].data["species"] = 
                    thspecies.Where(spe => spe.Value.data["name"] == animalData.species)
                    .First().Key.ToString();
                id++;
            }

            id = 0;
            foreach (var enclosureData in EnclosureData.Values)
            {
                thenclosures[id] = new ThirdRepresentation.Enclosure(id, new Dictionary<string, string>());
                thenclosures[id].data["name"] = enclosureData.name;
                thenclosures[id].data["animals.Size()"] = enclosureData.animals.Length.ToString();
                thenclosures[id].data["employee"] = enclosureData.employee;
                for(int i = 0; i < enclosureData.animals.Length; i++)
                    thenclosures[id].data[$"animals[{i}]"] = 
                        thanimals.Where(spe => spe.Value.data["name"] == enclosureData.animals[i]).First().Key.ToString();
                id++;
            }

            id = 0;
            foreach (var employeeData in EmployeeData.Values) 
            {
                themployees[id] = new ThirdRepresentation.Employee(id, new Dictionary<string, string>());
                themployees[id].data["name"] = employeeData.name;
                themployees[id].data["surname"] = employeeData.surname;
                themployees[id].data["age"] = employeeData.age.ToString();
                themployees[id].data["enclosures.Size()"] = employeeData.enclosures.Length.ToString();
                for(int i = 0; i < employeeData.enclosures.Length; i++)
                    themployees[id].data[$"enclosures[{i}]"] = 
                        thenclosures.Where(enc => (enc.Value.data["name"] == employeeData.enclosures[i]))
                        .First().Key.ToString();
                id++;
            }

            id = 0;
            foreach(var visitor in VisitorData.Values)
            {
                thvisitors[id] = new ThirdRepresentation.Visitor(id, new Dictionary<string, string>());
                thvisitors[id].data["name"] = visitor.name;
                thvisitors[id].data["surname"] = visitor.surname;
                thvisitors[id].data["visitedEnclosures.Size()"] = visitor.enclosures.Length.ToString();
                for (int i = 0; i < visitor.enclosures.Length; i++)
                    thvisitors[id].data[$"visitedEnclosures[{i}]"] =
                        thenclosures.Where(enc => (enc.Value.data["name"] == visitor.enclosures[i]))
                        .First().Key.ToString();
                id++;
            }

            foreach (var enclosure in thenclosures)
                enclosure.Value.data["employee"] = themployees.Where(emp =>
                (emp.Value.data["name"] + " " + emp.Value.data["surname"]) == enclosure.Value.data["employee"])
                    .First().Key.ToString();

            //Adapters
            foreach (var enclosure in thenclosures)
                ThirdRepresentationEnclosures.Add(enclosure.Key,
                    new ThirdRepresentation.EnclosureAdapter(
                        enclosure.Value, thspecies, themployees, thenclosures, thanimals));
            foreach (var animal in thanimals)
                ThirdRepresentationAnimals.Add(animal.Key,
                    new ThirdRepresentation.AnimalAdapter(animal.Value, thspecies));
            foreach (var specie in thspecies)
                ThirdRepresentationSpecies.Add(specie.Key,
                    new ThirdRepresentation.SpeciesAdapter(specie.Value, thspecies));
            foreach (var employee in themployees)
                ThirdRepresentationEmployees.Add(employee.Key,
                    new ThirdRepresentation.EmployeeAdapter(employee.Value, thspecies, themployees, thenclosures, thanimals));
            foreach (var visitor in thvisitors)
                ThirdRepresentationVisitors.Add(visitor.Key,
                    new ThirdRepresentation.VisitorAdapter(visitor.Value, themployees, thspecies, thenclosures, thanimals));
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