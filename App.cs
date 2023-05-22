using Collections;
using ZooApp;

namespace Zoo
{
    public sealed class App
    {
        public static readonly ICollection enclosures = new DoubleLinkList();
        public static readonly ICollection animals = new Vector();
        public static readonly ICollection species  = new BinaryTree();
        public static readonly ICollection employees = new BinaryTree();
        public static readonly ICollection visitors = new DoubleLinkList();

        public static Dictionary<string, ICollection> nameToColectionDictionary =
            new Dictionary<string, ICollection>();

        public static Func<IEditableByUser> GetFunctionForCreatingObject(string entity, RepresentationFactory factory)
        {
            switch(entity)
            {
                case "animal":
                    return factory.CreateAnimal;
                case "employee":
                    return factory.CreateEmployee;
                case "visitor":
                    return factory.CreateVisitor;
                case "species":
                    return factory.CreateSpecies;
                case "enclosure":
                    return factory.CreateEnclosure;
            }
            throw new ArgumentException();
        }

        public void Init(Dictionary<string, IEnclosure> enclosuresDict,
            Dictionary<string, IAnimal> animalsDict,
            Dictionary<string, ISpecies> speciesDict,
            Dictionary<string, IEmployee> employeesDict,
            Dictionary<string, IVisitor> visitorsDict)
        {
            foreach (var enclosure in enclosuresDict.Values)
                enclosures.Add(enclosure);
            foreach (var animal in animalsDict.Values)
                animals.Add(animal);
            foreach (var specie in speciesDict.Values)
                species.Add(specie);
            foreach (var employee in employeesDict.Values)
                employees.Add(employee);
            foreach (var visitor in visitorsDict.Values)
                visitors.Add(visitor);

            nameToColectionDictionary["enclosure"] = enclosures;
            nameToColectionDictionary["animal"] = animals;
            nameToColectionDictionary["visitor"] = visitors;
            nameToColectionDictionary["employee"] = employees;
            nameToColectionDictionary["species"] = species;
        }
        
        private App() { }
        private static App? instance = null;

        public static App GetInstance()
        {
            if (instance == null)
                instance = new App();
            
            return instance;
        }

        CommandQueue commands = new CommandQueue();

        public bool running { get; set; }
        public void Start()
        {
            running = true;
            while(running)
            {
                var input = Console.ReadLine();
                if (input == null) continue;

                commands.AddCommand(input);
            }
        }
    }
}
