using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    public interface IEditableByUser
    {
        Dictionary<string, Action<string>> settersForUsers { get;}
        Dictionary<string, Func<string>> gettersForUsers { get;}
    }
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
            for (int i = 1; i < arguments.Length; i++)
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
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "EXIT")
                {
                    Console.WriteLine($"[{arguments[1]} creation abandoned]");
                    break;
                }
                else if (input == "DONE")
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
                if (!ValidatePredicate(input))
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
            switch (entity.ToLower())
            {
                case "enclosure": return new App.EnclosureCommand(val);
                case "animal": return new App.AnimalCommand(val);
                case "employee": return new App.EmployeeCommand(val);
                case "visitor": return new App.VisitorCommand(val);
                case "species": return new App.SpeciesCommand(val);
            }
            throw new ArgumentException();
        }
    }
}
