using System.Text;
using Zoo;

namespace Zoo
{
    public interface IEnclosure
    {
        string GetName();
        IEnumerable<ISpecies> GetAnimals();
        IEmployee GetEmployee();
    }
    public interface IAnimal
    {
        string GetName();
        int GetAge();
        ISpecies GetSpecies();
    }
    public interface IEmployee
    { 
        string GetName();
        string GetSurname();
        int GetAge();
        IEnumerable<IEnclosure> GetEnclosureList();
    }
    public interface ISpecies
    {
        string GetName();
        IEnumerable<ISpecies> GetFavouriteFoods();
    }
    public interface IVisitor
    {
        string GetName();
        string GetSurname();
        IEnumerable<IEnclosure> GetVisitedEnclosures();
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
        public string surname { get; set;}
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

    public class EnclosureAdapter : Zoo.IEnclosure
    {
        Enclosure adaptee;
        public EnclosureAdapter(Enclosure adaptee)
        {
            this.adaptee = adaptee;
        }
        public string GetName()
        {
            return adaptee.name;
        }
        public IEmployee GetEmployee()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<ISpecies> GetAnimals()
        {
            foreach (var specie in adaptee.animals)
                yield return new SpeciesAdapter(specie);
        }
    }

    public class AnimalAdapter : Zoo.IAnimal
    {
        Animal adaptee;
        public AnimalAdapter(Animal adaptee)
        {
            this.adaptee = adaptee;
        }

        public string GetName()
        {
            return adaptee.name;
        }

        public int GetAge()
        {
            return adaptee.age;
        }

        public ISpecies GetSpecies()
        {
            return new SpeciesAdapter(adaptee.species);
        }
    }

    public class SpeciesAdapter : Zoo.ISpecies
    {
        Species adaptee;
        public SpeciesAdapter(Species adaptee)
        {
            this.adaptee = adaptee;
        }
        public string GetName()
        {
            return adaptee.name;
        }

        public IEnumerable<ISpecies> GetFavouriteFoods()
        {
            foreach (var food in adaptee.favouriteFoods)
                yield return new SpeciesAdapter(food);
        }
    }

    public class EmployeeAdapter : IEmployee
    {
        Employee adaptee;
        public EmployeeAdapter(Employee adaptee)
        {
            this.adaptee = adaptee;
        }

        public string GetName() { return adaptee.name; }
        public string GetSurname() { return adaptee.surname;}
        public int GetAge() { return adaptee.age;}
    }
}