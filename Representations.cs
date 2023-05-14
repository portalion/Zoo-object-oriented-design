namespace Zoo
{
    public interface IEnclosure
    {
        string name { get; set; }
        IEnumerable<IAnimal> animals { get; }
        IEmployee employee { get; }
    }
    public interface IAnimal
    {
        string name { get; set; }
        int age { get; set; }
        ISpecies species { get; }
    }
    public interface IEmployee
    { 
        string name { get; set; }
        string surname { get; set; }
        int age { get; set; }
        IEnumerable<IEnclosure> enclosures { get; }
    }
    public interface ISpecies
    {
        string name { get; set; }
        IEnumerable<ISpecies> favouriteFoods { get; }
    }
    public interface IVisitor
    {
        string name { get; set; }
        string surname { get; set; }
        IEnumerable<IEnclosure> visitedEnclosures { get; }
    }
}

namespace MainRepresentation
{
    public class Enclosure
    {
        public string name { get; set; }
        public List<Animal> animals { get; set; }
        public Employee employee { get; set; }

        public Enclosure(string name, List<Animal> animals, Employee employee)
        {
            this.name = name;
            this.animals = animals;
            this.employee = employee;
        }
    }
    public class Animal
    {
        public string name { get; set; }
        public int age { get; set; }
        public Species species { get; set; }
        public Animal(string name, int age, Species species)
        {
            this.name = name;
            this.age = age;
            this.species = species;
        }
    }
    public class Species
    {
        public string name { get; set; }
        public List<Species> favouriteFoods { get; set; }
        public Species(string name, List<Species> favouriteFoods)
        {
            this.name = name;
            this.favouriteFoods = favouriteFoods;
        }
    }
    public class Employee
    {
        public string name { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        public List<Enclosure> enclosures { get; set; }

        public Employee(string name, string surname, int age, List<Enclosure> enclosures)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.enclosures = enclosures;
        }
    }
    public class Visitor
    {
        public string name { get; set; }
        public string surname { get; set; }
        public List<Enclosure> visitedEnclosures { get; set; }
        public Visitor(string name, string surname, List<Enclosure> visitedEnclosures)
        {
            this.name = name;
            this.surname = surname;
            this.visitedEnclosures = visitedEnclosures;
        }
    }

}

namespace SecondRepresentation
{
    public class Enclosure
    {
        public string data;
        public Enclosure(string data)
        {
            this.data = data;
        }
    }
    public class Animal
    {
        public string data;
        public Animal(string data)
        {
            this.data = data;
        }
    }
    public class Species
    {
        public string data;
        public Species(string data)
        {
            this.data = data;   
        }
    }
    public class Employee
    {
        public string data;
        public Employee(string data)
        {
            this.data = data;
        }
    }
    public class Visitor
    {
        public string data;
        public Visitor(string data)
        {
            this.data = data;
        }
    }
}

namespace ThirdRepresentation
{
    public class Enclosure
    {
        public int id;
        public Dictionary<string, string> data;
        public Enclosure(int id, Dictionary<string, string> data)
        {
            this.id = id;
            this.data = data;
        }
    }
    public class Animal
    {
        public int id;
        public Dictionary<string, string> data;
        public Animal(int id, Dictionary<string, string> data)
        {
            this.id = id;
            this.data = data;
        }
    }
    public class Species
    {
        public int id;
        public Dictionary<string, string> data;
        public Species(int id, Dictionary<string, string> data)
        {
            this.id = id;
            this.data = data;
        }
    }
    public class Employee
    {
        public int id;
        public Dictionary<string, string> data;
        public Employee(int id, Dictionary<string, string> data)
        {
            this.id = id;
            this.data = data;
        }
    }
    public class Visitor
    {
        public int id;
        public Dictionary<string, string> data;
        public Visitor(int id, Dictionary<string, string> data)
        {
            this.id = id;
            this.data = data;
        }
    }
}