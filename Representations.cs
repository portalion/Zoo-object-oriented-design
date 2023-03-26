using System.Text;
using Zoo;

namespace Zoo
{
    public interface IEnclosure
    {
        string name { get; }
        IEnumerable<ISpecies> animals { get; }
        IEmployee employee { get; }
    }
    public interface IAnimal
    {
        string name { get; }
        int age { get; }
        ISpecies species { get; }
    }
    public interface IEmployee
    { 
        string name { get; }
        string surname { get; }
        int age { get; }
        IEnumerable<IEnclosure> enclosures { get; }
    }
    public interface ISpecies
    {
        string name { get; }
        IEnumerable<ISpecies> favouriteFoods { get; }
    }
    public interface IVisitor
    {
        string name { get; }
        string surname { get; }
        IEnumerable<IEnclosure> visitedEnclosures { get; }
    }
}

namespace MainRepresentation
{
    public class Enclosure
    {
        public string name { get; set; }
        public List<Species> animals { get; set; }
        public Employee employee { get; set; }

        public Enclosure(string name, List<Species> animals, Employee employee)
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
        Enclosure(string data)
        {
            this.data = data;
        }
    }
    public class Animal
    {
        public string data;
        Animal(string data)
        {
            this.data = data;
        }
    }
    public class Species
    {
        public string data;
        Species(string data)
        {
            this.data = data;   
        }
    }
    public class Employee
    {
        public string data;
        Employee(string data)
        {
            this.data = data;
        }
    }
    public class Visitor
    {
        public string data;
        Visitor(string data)
        {
            this.data = data;
        }
    }
}