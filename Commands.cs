using Collections;

namespace Zoo
{
    public interface IEditableByUser
    {
        Dictionary<string, Action<string>> settersForUsers { get;}
        Dictionary<string, Func<string>> gettersForUsers { get;}
    }

    public abstract class Command
    {
        protected string[] arguments;
        protected string operation;

        public Command(string userLine)
        {
            var userLineSplited = userLine.Split(" ");
            operation = userLineSplited[0];
            arguments = userLineSplited.Skip(1).ToArray();
        }
        public abstract void Execute();

        public static Command GetCommand(string userLine)
        {
            Command result;
            switch(userLine.Split(" ")[0].ToLower())
            {
                case "exit":
                    result =  new ExitCommand(userLine);
                    break;
                case "list":
                    result = new ListCommand(userLine);
                    break;
                case "add":
                    result = new AddCommand(userLine);
                    break;
                case "find":
                    result = new FindCommand(userLine);
                    break;
                default: throw new InvalidOperationException();
            }
            return result;
        }
    }

    public abstract class CommandWithPredicateArgument : Command
    {
        protected class PredicateToAdd : Predicate
        {
            PredicateToAdd? next;

            string toCheck;

            public PredicateToAdd(PredicateToAdd? next, string toCheck)
            {
                this.next = next;
                this.toCheck = toCheck;
            }

            public bool fulfills(IEditableByUser toCheck)
            {
                if (next != null && !next.fulfills(toCheck)) return false;

                string[] arguments;
                char operation;

                if ((arguments = this.toCheck.Split("<")).Length == 2)
                    operation = '<';
                else if ((arguments = this.toCheck.Split("=")).Length == 2)
                    operation = '=';
                else if ((arguments = this.toCheck.Split(">")).Length == 2)
                    operation = '>';
                else throw new InvalidOperationException();

                if(operation == '>')
                    return toCheck.gettersForUsers[arguments[0]]().CompareTo(arguments[1]) == 1;
                if (operation == '<')
                    return toCheck.gettersForUsers[arguments[0]]().CompareTo(arguments[1]) == -1;
                return toCheck.gettersForUsers[arguments[0]]().CompareTo(arguments[1]) == 0;

            }
        }

        protected string entity;
        protected PredicateToAdd? pred = null;
        public CommandWithPredicateArgument(string userLine) : base(userLine)
        {
            entity = arguments[0];
            for(int i = 1; i < arguments.Length; i++)
                pred = new PredicateToAdd(pred, arguments[i]);
        }
    }

    public class FindCommand : CommandWithPredicateArgument
    {
        public FindCommand(string userLine) : base(userLine)
        {

        }

        public override void Execute()
        {
            if (pred == null)
            {
                Algorithms.Print(App.nameToColectionDictionary[entity].GetIterator(), 
                    new TruePredicate());
                return;
            }
            Algorithms.Print(App.nameToColectionDictionary[entity].GetIterator(), pred);
        }
    }

    public class DeleteCommand : CommandWithPredicateArgument
    {
        public DeleteCommand(string userLine) : base(userLine)
        {
        }

        public override void Execute()
        {
            var dictionary = App.nameToColectionDictionary[entity];
            if (pred == null) return;
            if (Algorithms.CountIf(dictionary.GetIterator(), pred) > 1) return;

            dictionary.Remove(Algorithms.Find(dictionary.GetIterator(), pred));
        }
    }

    public class AddCommand : Command
    {

        public AddCommand(string userLine) : base(userLine)
        {
            if(arguments.Length < 2) throw new ArgumentException();
        }

        public override void Execute()
        {
            IEditableByUser result;
            if (arguments[1] == "base")
                result = App.GetFunctionForCreatingObject(arguments[0],
                    new FirstRepresentationFactory())();
            else throw new NotImplementedException();

            Console.WriteLine($"Possible fields: [{String.Join(", ", result.settersForUsers.Keys)}]");

            while (true)
            {
                var input = Console.ReadLine();
                if (input == null) continue;
                if (input == "EXIT") break;
                if (input == "DONE")
                {
                    App.nameToColectionDictionary[arguments[0]].Add(result);
                    break;
                }
                var variable = input.Split('=');
                if (variable.Length < 2) continue;
                if (!result.settersForUsers.ContainsKey(variable[0])) continue;
                result.settersForUsers[variable[0]](variable[1]);
            }
        }
    }

    public class ListCommand : Command
    {
        public ListCommand(string userLine) : base(userLine) { }

        public override void Execute()
        {
            if (arguments.Length < 1) throw new ArgumentException();
            Algorithms.Print(App.nameToColectionDictionary[arguments[0]]
                .GetIterator(), new TruePredicate());
        }
    }

    public class ExitCommand : Command
    {
        public ExitCommand(string userLine) :base(userLine) { }
        public override void Execute() 
        {
            App.GetInstance().running = false;
        }
    }
}
