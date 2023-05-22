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
                default: throw new InvalidOperationException();
            }
            return result;
        }
    }

    public abstract class CommandWithPredicateArgument : Command
    {
        public CommandWithPredicateArgument(string userLine) : base(userLine)
        {
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
