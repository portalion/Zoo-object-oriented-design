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

    public abstract class CommandWithFactory : Command
    {
        //protected IEditableFactory factory;
        public CommandWithFactory(string userLine): base(userLine) 
        {
            var entity = arguments[0];
            arguments = arguments.Skip(1).ToArray();
           
        }
    }

    public abstract class CommandWithPredicateArgument : Command
    {
        public CommandWithPredicateArgument(string userLine) : base(userLine)
        {
        }
    }

    public class AddCommand : CommandWithFactory
    {
        public AddCommand(string userLine) : base(userLine)
        {
            if(arguments.Length < 1) throw new ArgumentException();
        }

        public override void Execute()
        {
           /* IEditableByUser result;
            if (arguments[0] == "base") result = factory.CreateBaseRepresentation();
            else result = factory.CreateThirdRepresentation();

            Console.WriteLine($"Possible fields: [{String.Join(", ", result.settersForUsers.Keys)}]");

            while (true)
            {
                var input = Console.ReadLine();
                if (input == null) continue;
                if (input == "EXIT") break;
                if (input == "DONE")
                {
                    factory.GetCollection().Add(result);
                    break;
                }
            }*/
        }
    }

    public class ListCommand : CommandWithFactory
    {
        public ListCommand(string userLine) : base(userLine) { }

        public override void Execute()
        {
            //Algorithms.Print(factory.GetCollection().GetIterator(), new TruePredicate());
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
