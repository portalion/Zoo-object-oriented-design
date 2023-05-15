using Collections;

namespace Zoo
{
    public sealed class App
    {
        public readonly static ICollection enclosures = new DoubleLinkList();
        public readonly static ICollection animals = new Vector();
        public readonly static ICollection species  = new BinaryTree();
        public readonly static ICollection employees = new BinaryTree();
        public readonly static ICollection visitors = new DoubleLinkList();

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
        
        private App() { }
        private static App? instance = null;

        public static App GetInstance()
        {
            if (instance == null)
                instance = new App();
            
            return instance;
        }

        public bool running { get; set; }
        public void Start()
        {
            running = true;
            while(running)
            {
                var input = Console.ReadLine();
                if (input == null) continue;

                Command command;
                try
                {
                    command = Command.GetCommand(input);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Bad argument");
                    continue;
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine("Bad command");
                    continue;
                }
                command.Execute();
            }
        }
    }
}
