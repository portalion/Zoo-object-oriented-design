using Collections;
using System.Linq;
using System.Xml.Schema;

namespace Zoo
{
    internal class App
    {
        static Collections.ICollection<IEnclosure> enclosures = new DoubleLinkList<IEnclosure>();
        static Collections.ICollection<IAnimal> animals = new Vector<IAnimal>();
        static Collections.ICollection<ISpecies> species  = new BinaryTree<ISpecies>();
        static Collections.ICollection<IEmployee> employees = new BinaryTree<IEmployee>();
        static Collections.ICollection<IVisitor> visitors = new DoubleLinkList<IVisitor>();

        abstract class Command
        {
            public string[] arguments { get; set; }
            public string type { get; set; }

            public Command(string userLine)
            {
                type = userLine.Split(" ")[0];
                arguments = userLine.Split(" ").Skip(1).ToList().ToArray();
            }
            public void execute()
            {
                switch (type)
                {
                    case "list":
                        list();
                        break;
                    case "find":
                        find();
                        break;
                    case "add":
                        add();
                        break;
                }
            }
            public void find()
            {
                if (arguments.Length == 1)
                { 
                    list(); 
                    return; 
                }
                for(int i = 1; i < arguments.Length; i++)
                    if (!ValidatePredicate(arguments[i]))
                    {
                        Console.WriteLine($"Invalid argument: {arguments[i]}");
                        return;
                    }
                printWithPredicate();
                Console.WriteLine("Yep we got this");
            }
            public void add()
            {
                startAdding();
                while(true)
                {
                    var input = Console.ReadLine();
                    if(input == "EXIT")
                    {
                        Console.WriteLine($"[{arguments[1]} creation abandoned]");
                        break;
                    }
                    else if(input == "DONE")
                    {
                        Console.WriteLine($"[{arguments[1]} created]");
                        Add();
                        break;
                    }
                    if (!input.Contains("="))
                    {
                        Console.WriteLine("Specify field and value");
                        continue;
                    }
                    if(!ValidatePredicate(input))
                    {
                        Console.WriteLine("Bad argument");
                        continue;
                    }
                    performOperation(input);
                }
            }
            public abstract void startAdding();
            public abstract void Add();
            public abstract void performOperation(string operation);
            public abstract bool ValidatePredicate(string arg);
            public abstract void printWithPredicate();
            public abstract void list();
            public static Command GetCommand(string entity, string val)
            {
                switch(entity.ToLower())
                {
                    case "enclosure": return new EnclosureCommand(val);
                    case "animal": return new AnimalCommand(val);
                    case "employee": return new EmployeeCommand(val);
                    case "visitor": return new VisitorCommand(val);
                    case "species": return new SpeciesCommand(val);
                }
                throw new ArgumentException();
            }
        }
        class EnclosureCommand : Command
        {
            public EnclosureCommand(string userLine): base(userLine)
            {
            }

            public override void list()
            {
                Collections.Algorithms.Print(enclosures.GetIterator(), new TruePredicate<IEnclosure>());
            }
            public override bool ValidatePredicate(string arg)
            {
                string[] tmp;
                if (arg.Contains("="))
                    tmp = arg.Split('=');
                else if (arg.Contains("<"))
                    tmp = arg.Split('<');
                else if (arg.Contains(">"))
                    tmp = arg.Split('>');
                else return false;

                return tmp[0] == "name" || tmp[0] == "employee";
            }

            public override void printWithPredicate()
            {
                IEnclosure? toCheck;
                Iterator<IEnclosure> it = enclosures.GetIterator();
                while ((toCheck = it.MoveNext()) != null)
                {
                    bool print = true;
                    for (int i = 1; i < arguments.Length; i++)
                        if (!isGood(arguments[i], toCheck)) print = false;
                    if (print) Zoo.PrintEnclosure(toCheck);
                }
                    
            }
            private bool isGood(string arg, IEnclosure val)
            {
                string[] tmp;
                int sortVal;
                if (arg.Contains("="))
                {
                    tmp = arg.Split('=');
                    sortVal = 0;
                }
                else if (arg.Contains("<"))
                {
                    tmp = arg.Split('<');
                    sortVal = -1;
                }
                else if (arg.Contains(">"))
                {
                    tmp = arg.Split('>');
                    sortVal = 1;
                }
                else return false;

                int result = 0;
                if (tmp[0] == "name")
                    result = string.Compare(val.name, tmp[1]);
                else if (tmp[0] == "employee")
                    result = string.Compare(val.employee.name, tmp[1]); 

                if (result < 0 && sortVal == -1) return true;
                else if (result == 0 && sortVal == 0) return true;
                else if (result > 0 && sortVal == 1) return true;
                else return false;
            }

            IEnclosure toAdd;
            public override void startAdding()
            {
                if (arguments[1] == "base")
                    toAdd = new MainRepresentation.EnclosureAdapter(new MainRepresentation.Enclosure("", new List<MainRepresentation.Animal>(), null));
                else toAdd = new ThirdRepresentation.EnclosureAdapter(new ThirdRepresentation.Enclosure(0, new Dictionary<string, string>()), Zoo.thspecies, Zoo.themployees, Zoo.thenclosures, Zoo.thanimals);
                Console.WriteLine("[Available fields: name]");
            }
            public override void Add()
            {
                enclosures.Add(toAdd);
            }
            public override void performOperation(string operation)
            {
                var tmp = operation.Split("=");
                switch (tmp[0])
                {
                    case "name":
                        toAdd.name = tmp[1];
                        break;
                }
            }
        }
        class AnimalCommand : Command
        {
            public AnimalCommand(string userLine) : base(userLine)
            {
            }

            public override void list()
            {
                Collections.Algorithms.Print(animals.GetIterator(), new TruePredicate<IAnimal>());
            }
            public override bool ValidatePredicate(string arg)
            {
                string[] tmp;
                if (arg.Contains("="))
                    tmp = arg.Split('=');
                else if (arg.Contains("<"))
                    tmp = arg.Split('<');
                else if (arg.Contains(">"))
                    tmp = arg.Split('>');
                else return false;

                return tmp[0] == "name" || tmp[0] == "age" || tmp[0] == "species";
            }
            public override void printWithPredicate()
            {
                IAnimal? toCheck;
                Iterator<IAnimal> it = animals.GetIterator();
                while ((toCheck = it.MoveNext()) != null)
                {
                    bool print = true;
                    for (int i = 1; i < arguments.Length; i++)
                        if (!isGood(arguments[i], toCheck)) print = false;
                    if (print) Zoo.PrintAnimal(toCheck);
                }
            }
            private bool isGood(string arg, IAnimal val)
            {
                string[] tmp;
                int sortVal;
                if (arg.Contains("="))
                {
                    tmp = arg.Split('=');
                    sortVal = 0;
                }
                else if (arg.Contains("<"))
                {
                    tmp = arg.Split('<');
                    sortVal = -1;
                }
                else if (arg.Contains(">"))
                {
                    tmp = arg.Split('>');
                    sortVal = 1;
                }
                else return false;

                int result = 0;
                if (tmp[0] == "name")
                    result = string.Compare(val.name, tmp[1]);
                else if (tmp[0] == "species")
                    result = string.Compare(val.species.name, tmp[1]);
                else if (tmp[0] == "age")
                    result = int.Parse(tmp[1]).CompareTo(val.age) * -1;

                if (result < 0 && sortVal == -1) return true;
                else if (result == 0 && sortVal == 0) return true;
                else if (result > 0 && sortVal == 1) return true;
                else return false;
            }

            IAnimal toAdd;
            public override void startAdding()
            {
                if (arguments[1] == "base")
                    toAdd = new MainRepresentation.AnimalAdapter(new MainRepresentation.Animal("", 0, null));
                else toAdd = new ThirdRepresentation.AnimalAdapter(new ThirdRepresentation.Animal(0, new Dictionary<string, string>()), Zoo.thspecies);
                Console.WriteLine("[Available fields: name, age]");
            }
            public override void Add()
            {
                animals.Add(toAdd);
            }
            public override void performOperation(string operation)
            {
                var tmp = operation.Split("=");
                switch (tmp[0])
                {
                    case "name":
                        toAdd.name = tmp[1];
                        break;
                    case "age":
                        toAdd.age = int.Parse(tmp[1]);
                        break;
                }
            }
        }
        class VisitorCommand : Command
        {
            public VisitorCommand(string userLine) : base(userLine)
            {
            }

            public override void list()
            {
                Collections.Algorithms.Print(visitors.GetIterator(), new TruePredicate<IVisitor>());
            }
            public override bool ValidatePredicate(string arg)
            {
                string[] tmp;
                if (arg.Contains("="))
                    tmp = arg.Split('=');
                else if (arg.Contains("<"))
                    tmp = arg.Split('<');
                else if (arg.Contains(">"))
                    tmp = arg.Split('>');
                else return false;

                return tmp[0] == "name" || tmp[0] == "surname";
            }
            public override void printWithPredicate()
            {
                IVisitor? toCheck;
                Iterator<IVisitor> it = visitors.GetIterator();
                while ((toCheck = it.MoveNext()) != null)
                {
                    bool print = true;
                    for (int i = 1; i < arguments.Length; i++)
                        if (!isGood(arguments[i], toCheck)) print = false;
                    if (print) Zoo.PrintVisitor(toCheck);
                }
            }
            private bool isGood(string arg, IVisitor val)
            {
                string[] tmp;
                int sortVal;
                if (arg.Contains("="))
                {
                    tmp = arg.Split('=');
                    sortVal = 0;
                }
                else if (arg.Contains("<"))
                {
                    tmp = arg.Split('<');
                    sortVal = -1;
                }
                else if (arg.Contains(">"))
                {
                    tmp = arg.Split('>');
                    sortVal = 1;
                }
                else return false;

                int result = 0;
                if (tmp[0] == "name")
                    result = string.Compare(val.name, tmp[1]);
                else if (tmp[0] == "surname")
                    result = string.Compare(val.surname, tmp[1]);

                if (result < 0 && sortVal == -1) return true;
                else if (result == 0 && sortVal == 0) return true;
                else if (result > 0 && sortVal == 1) return true;
                else return false;
            }

            IVisitor toAdd;
            public override void startAdding()
            {
                if (arguments[1] == "base")
                    toAdd = new MainRepresentation.VisitorAdapter(new MainRepresentation.Visitor("", "", new List<MainRepresentation.Enclosure>()));
                else toAdd = new ThirdRepresentation.VisitorAdapter(new ThirdRepresentation.Visitor(0, new Dictionary<string, string>()), Zoo.themployees, Zoo.thspecies, Zoo.thenclosures, Zoo.thanimals);
                Console.WriteLine("[Available fields: name, surname]");
            }
            public override void Add()
            {
                visitors.Add(toAdd);
            }
            public override void performOperation(string operation)
            {
                var tmp = operation.Split("=");
                switch (tmp[0])
                {
                    case "name":
                        toAdd.name = tmp[1];
                        break;
                    case "surname":
                        toAdd.surname = tmp[1];
                        break;
                }
            }
        }
        class EmployeeCommand : Command
        {
            public EmployeeCommand(string userLine) : base(userLine)
            {
            }

            public override void list()
            {
                Collections.Algorithms.Print(employees.GetIterator(), new TruePredicate<IEmployee>());
            }
            public override bool ValidatePredicate(string arg)
            {
                string[] tmp;
                if (arg.Contains("="))
                    tmp = arg.Split('=');
                else if (arg.Contains("<"))
                    tmp = arg.Split('<');
                else if (arg.Contains(">"))
                    tmp = arg.Split('>');
                else return false;

                return tmp[0] == "name" || tmp[0] == "surname" || tmp[0] == "age";
            }
            public override void printWithPredicate()
            {
                IEmployee? toCheck;
                Iterator<IEmployee> it = employees.GetIterator();
                while ((toCheck = it.MoveNext()) != null)
                {
                    bool print = true;
                    for (int i = 1; i < arguments.Length; i++)
                        if (!isGood(arguments[i], toCheck)) print = false;
                    if (print) Zoo.PrintEmployee(toCheck);
                }
            }
            private bool isGood(string arg, IEmployee val)
            {
                string[] tmp;
                int sortVal;
                if (arg.Contains("="))
                {
                    tmp = arg.Split('=');
                    sortVal = 0;
                }
                else if (arg.Contains("<"))
                {
                    tmp = arg.Split('<');
                    sortVal = -1;
                }
                else if (arg.Contains(">"))
                {
                    tmp = arg.Split('>');
                    sortVal = 1;
                }
                else return false;

                int result = 0;
                if (tmp[0] == "name")
                    result = string.Compare(val.name, tmp[1]);
                else if (tmp[0] == "surname")
                    result = string.Compare(val.surname, tmp[1]);
                else if (tmp[0] == "age")
                    result = int.Parse(tmp[1]).CompareTo(val.age) * -1;

                if (result < 0 && sortVal == -1) return true;
                else if (result == 0 && sortVal == 0) return true;
                else if (result > 0 && sortVal == 1) return true;
                else return false;
            }

            IEmployee toAdd;
            public override void startAdding()
            {
                if (arguments[1] == "base")
                    toAdd = new MainRepresentation.EmployeeAdapter(new MainRepresentation.Employee("", "", 0, null));
                else toAdd = new ThirdRepresentation.EmployeeAdapter(new ThirdRepresentation.Employee(0, new Dictionary<string, string>()), Zoo.thspecies, Zoo.themployees, Zoo.thenclosures, Zoo.thanimals);
                Console.WriteLine("[Available fields: name, surname, age]");
            }
            public override void Add()
            {
                employees.Add(toAdd);
            }
            public override void performOperation(string operation)
            {
                var tmp = operation.Split("=");
                switch (tmp[0])
                {
                    case "name":
                        toAdd.name = tmp[1];
                        break;
                    case "surname":
                        toAdd.surname = tmp[1];
                        break;
                    case "age":
                        toAdd.age = int.Parse(tmp[1]);
                        break;
                }
            }
        }
        class SpeciesCommand : Command
        {
            public SpeciesCommand(string userLine) : base(userLine)
            {
            }

            public override void list()
            {
                Collections.Algorithms.Print(species.GetIterator(), new TruePredicate<ISpecies>());
            }
            public override bool ValidatePredicate(string arg)
            {
                string[] tmp;
                if (arg.Contains("="))
                    tmp = arg.Split('=');
                else if (arg.Contains("<"))
                    tmp = arg.Split('<');
                else if (arg.Contains(">"))
                    tmp = arg.Split('>');
                else return false;

                return tmp[0] == "name";
            }
            public override void printWithPredicate()
            {
                ISpecies? toCheck;
                Iterator<ISpecies> it = species.GetIterator();
                while ((toCheck = it.MoveNext()) != null)
                {
                    bool print = true;
                    for (int i = 1; i < arguments.Length; i++)
                        if (!isGood(arguments[i], toCheck)) print = false;
                    if (print) Zoo.PrintSpecies(toCheck);
                }
            }
            private bool isGood(string arg, ISpecies val)
            {
                string[] tmp;
                int sortVal;
                if (arg.Contains("="))
                {
                    tmp = arg.Split('=');
                    sortVal = 0;
                }
                else if (arg.Contains("<"))
                {
                    tmp = arg.Split('<');
                    sortVal = -1;
                }
                else if (arg.Contains(">"))
                {
                    tmp = arg.Split('>');
                    sortVal = 1;
                }
                else return false;

                int result = 0;
                if (tmp[0] == "name")
                    result = string.Compare(val.name, tmp[1]);

                if (result < 0 && sortVal == -1) return true;
                else if (result == 0 && sortVal == 0) return true;
                else if (result > 0 && sortVal == 1) return true;
                else return false;
            }

            ISpecies toAdd;
            public override void startAdding()
            {
                if (arguments[1] == "base")
                    toAdd = new MainRepresentation.SpeciesAdapter(new MainRepresentation.Species("", null));
                else toAdd = new ThirdRepresentation.SpeciesAdapter(new ThirdRepresentation.Species(0, new Dictionary<string, string>()), Zoo.thspecies);
                Console.WriteLine("[Available fields: name]");
            }
            public override void Add()
            {
                species.Add(toAdd);
            }
            public override void performOperation(string operation)
            {
                var tmp = operation.Split("=");
                switch (tmp[0])
                {
                    case "name":
                        toAdd.name = tmp[1];
                        break;
                }
            }
        }

        public void Init(Dictionary<string, IEnclosure> enclosures,
            Dictionary<string, IAnimal> animals,
            Dictionary<string, ISpecies> species,
            Dictionary<string, IEmployee> employees,
            Dictionary<string, IVisitor> visitors)
        {
            foreach (var enclosure in enclosures.Values)
                App.enclosures.Add(enclosure);
            foreach (var animal in animals.Values)
                App.animals.Add(animal);
            foreach (var specie in species.Values)
                App.species.Add(specie);
            foreach (var employee in employees.Values)
                App.employees.Add(employee);
            foreach (var visitor in visitors.Values)
                App.visitors.Add(visitor);
        }
        public void Start()
        {
            while(true)
            {
                var input = Console.ReadLine();
                if (input == null) continue;
                var commands = input.Split(" ");

                if (commands[0] == "exit")
                    break;
                if (commands.Length < 2) continue;

                Command command;
                try
                {
                    command = Command.GetCommand(commands[1], input);
                }
                catch
                {
                    Console.WriteLine("Bad argument");
                    continue;
                }
                command.execute();
            }
        }
    }
}
