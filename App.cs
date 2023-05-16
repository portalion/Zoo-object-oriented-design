using Collections;

namespace Zoo
{
    public sealed class App
    {
        public readonly ICollection enclosures = new DoubleLinkList();
        public readonly ICollection animals = new Vector();
        public readonly ICollection species  = new BinaryTree();
        public readonly ICollection employees = new BinaryTree();
        public readonly ICollection visitors = new DoubleLinkList();

        public void Init(Dictionary<string, IEnclosure> enclosures,
            Dictionary<string, IAnimal> animals,
            Dictionary<string, ISpecies> species,
            Dictionary<string, IEmployee> employees,
            Dictionary<string, IVisitor> visitors)
        {
            foreach (var enclosure in enclosures.Values)
                this.enclosures.Add(enclosure);
            foreach (var animal in animals.Values)
                this.animals.Add(animal);
            foreach (var specie in species.Values)
                this.species.Add(specie);
            foreach (var employee in employees.Values)
                this.employees.Add(employee);
            foreach (var visitor in visitors.Values)
                this.visitors.Add(visitor);
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
