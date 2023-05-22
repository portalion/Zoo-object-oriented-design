using Zoo;

namespace ZooApp
{
    public class CommandQueue
    {
        List<Command> commands = new List<Command>();

        public void AddCommand(string userLine)
        {
            if(!userLine.StartsWith("queue"))
            {
                var toPerform = Command.GetCommand(userLine);
                if(userLine == "exit")
                {
                    toPerform.Execute();
                    return;
                }
                try
                {
                    toPerform = Command.GetCommand(userLine);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Bad argument");
                    return;
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Bad command");
                    return;
                }
                commands.Add(toPerform);
                return;
            }

            switch(userLine.Split(" ")[1]) 
            {
                case "commit":
                    commit();
                    break;
                case "print":
                    print();
                    break;
                case "dismiss":
                    dismiss();
                    break;

            }
        }

        void print()
        {
            foreach (var command in commands)
                Console.WriteLine(command.ToString());
        }

        void commit()
        {
            foreach(var command in commands)
                command.Execute();
            commands.Clear();
        }

        void dismiss()
        {
            commands.Clear();
        }
    }
}
