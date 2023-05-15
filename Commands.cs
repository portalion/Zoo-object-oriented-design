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
        protected IEnumerable<string> arguments;
        protected string operation;
        protected string entity;
        protected ICollection? entities;

        public Command(string userLine)
        {
            var userLineSplited = userLine.Split(" ");
            operation = userLineSplited[0];
            if (userLineSplited.Length < 2)
            {
                entity = "";
                arguments = new List<string>();
                entities = null;
                return;
            }
            entity = userLineSplited[1];
            arguments = userLineSplited.Skip(2);
        }
        protected void SetEntities()
        {
            switch (entity.ToLower())
            {
                case "enclosure":
                    entities = App.enclosures;
                    break;
                case "animal": 
                    entities = App.animals;
                    break;
                case "employee":
                    entities = App.employees;
                    break;
                case "visitor":
                    entities = App.visitors;
                    break;
                case "species":
                    entities = App.species;
                    break;
                default: 
                    entities = null;
                    break;
            }
        }
        protected virtual bool ValidateArguments() { return true; } //should return false if invalid arguments
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
                default: throw new InvalidOperationException();
            }
            if (!result.ValidateArguments()) throw new ArgumentException();
            return result;
        }
    }

    public class ListCommand : Command
    {
        public ListCommand(string userLine) : base(userLine) { SetEntities(); }

        protected override bool ValidateArguments()
        {
            if (entities == null) return false;
            return true;
        }
        public override void Execute()
        {
            Algorithms.Print(entities.GetIterator(), new TruePredicate());
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
