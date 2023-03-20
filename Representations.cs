using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRepresentation
{
    public class Enclosure : IEnclosure
    {
        public string name { get; set; }
        public List<Species> animals { get; private set; }
        public Employee employee { get; private set; }

        public Enclosure(string name, Species[]? animals, Employee employee)
        {
            this.name = name;
            this.animals = new List<Species>();
            this.employee = employee;

            if (animals == null) return;
            foreach (var animal in animals)
                this.animals.Add(animal);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);

            sb.AppendJoin(", ", animals.Select(animal => animal.name));
            return $"{name}, [{sb}], {employee.name} {employee.surname}";
        }

        public void print()
        {
            Console.WriteLine(ToString());
        }
    }
    public class Animal : IAnimal
    {
        public string name { get; private set; }
        public int age { get; private set; }
        public Species species { get; private set; }
        public Animal(string name, int age, Species species)
        {
            this.name = name;
            this.age = age;
            this.species = species;
        }

        public override string ToString()
        {
            return $"{name}, {age}, {species.name}";
        }
        public void print()
        {
            Console.WriteLine(ToString());
        }
    }
    public class Species : ISpecies
    {
        public string name { get; private set; }
        public List<Species> favouriteFoods { get; private set; }
        public Species(string name, Species[]? favouriteFoods)
        {
            this.name = name;
            this.favouriteFoods = new List<Species>();

            if (favouriteFoods == null) return;
            foreach(var food in favouriteFoods)
                this.favouriteFoods.Add(food);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(100);
            sb.AppendJoin(", ", favouriteFoods.Select(elem => elem.name));
            return $"{name}, [{sb}]";
        }
        public void print()
        {
            Console.WriteLine(ToString());
        }
    }
    public class Employee : IEmployee
    {
        public string name { get; private set; }
        public string surname { get; private set;}
        public int age { get; private set; }
        public List<Enclosure> enclosures { get; private set; }

        public Employee(string name, string surname, int age, Enclosure[]? enclosures)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.enclosures = new List<Enclosure>();

            if (enclosures == null) return;
            foreach (var enclousure in enclosures)
                this.enclosures.Add(enclousure);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(20 * enclosures.Count);
            sb.AppendJoin(", ", enclosures.Select(elem => elem.name));
            return $"{name}, {surname}, {age}, [{sb}]";
        }
        public void print()
        {
            Console.WriteLine(ToString());
        }
    }
    public class Visitor : IVisitor
    {
        public string name { get; private set; }
        public string surname { get; private set; }
        public List<Enclosure> visitedEnclosures { get; private set; }
        public Visitor(string name, string surname, Enclosure[]? visitedEnclosures)
        {
            this.name = name;
            this.surname = surname;
            this.visitedEnclosures = new List<Enclosure>();

            if (visitedEnclosures == null) return;
            foreach(var enclousure in visitedEnclosures)
                this.visitedEnclosures.Add(enclousure);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(20 * visitedEnclosures.Count);
            sb.AppendJoin(", ", visitedEnclosures.Select(elem => elem.name));
            return $"{name} {surname}, [{sb}]";
        
        }

        public void print()
        {
            Console.WriteLine(ToString());
        }
    }

    public interface IEnclosure 
    {
        public void print();
    }
    public interface IAnimal
    {
        public void print();
    }
    public interface IEmployee
    {
        public void print();
    }
    public interface ISpecies
    {
        public void print();
    }
    public interface IVisitor
    {
        public void print();
    }
 
}


namespace DatabaseRepresentation
{
    public class Enclosure
    {
        public string data {get; private set; }

        public Enclosure(string data)
        {
            this.data = data;
        }

        public override string ToString() 
        {
            return data;
        }
    }
    public class Animal
    {
        public string data { get; private set; }

        public Animal(string data)
        { 
            this.data = data; 
        }
        public override string ToString()
        {
            return data;
        }
    }
    public class Species
    {
        public string data { get; private set; }
        public Species(string data) 
        {
            this.data = data;
        }
        public override string ToString()
        {
            return data;
        }
    }
    public class Employee
    {
        public string data { get; private set; }
        public Employee(string data)
        {
            this.data = data;
        }
        public override string ToString()
        {
            return data;
        }
    }
    public class Visitor
    {
        public string data { get; private set; }
        public Visitor(string data)
        {
            this.data = data;
        }
        public override string ToString()
        {
            return data;
        }
    }
}

namespace DatabaseToUserAdapter
{
    using UR = UserRepresentation;
    using DatabaseRepresentation;

    public class EnclosureAdapter : UR.IEnclosure
    {
        Enclosure adaptee;

        public EnclosureAdapter(Enclosure adaptee)
        {
            this.adaptee = adaptee;
        }

        public void print()
        {
            string name = adaptee.data.Split('@')[0];
            string employee = adaptee.data.Split('@')[1].Split(',')[0];
            int countOfAnimals = adaptee.data.Split('@')[1].Split(',').Length - 1;
            string[] animals = new string[countOfAnimals];
            for(int i = 0; i < countOfAnimals; i++) animals[i] = adaptee.data.Split('@')[1].Split(',')[i + 1];

            StringBuilder sb = new StringBuilder();
            sb.Append($"{name}, [");
            sb.AppendJoin(", ", animals);
            sb.Append($"], {employee}");
            Console.WriteLine(sb);
        }
    }

    public class AnimalAdapter : UR.IAnimal
    {
        Animal adaptee;

        public AnimalAdapter(Animal adaptee)
        {
            this.adaptee = adaptee;
        }

        public void print()
        {
            string name = adaptee.data.Split('(')[0];
            string age = adaptee.data.Split('(')[1].Split(')')[0];
            string species = adaptee.data.Split('%')[1];
            Console.WriteLine($"{name}, {age}, {species}");
        }
    }

    public class SpeciesAdapter : UR.ISpecies
    {
        Species adaptee;

        public SpeciesAdapter(Species adaptee)
        {
            this.adaptee = adaptee;
        }

        public void print()
        {
            string name = adaptee.data.Split('$')[0];
            string[] foods = adaptee.data.Split('$')[1].Split(',');

            StringBuilder sb = new StringBuilder();
            sb.Append($"{name}, [");
            sb.AppendJoin(", ", foods);
            sb.Append("]");

            Console.WriteLine(sb);
        }
    }

    public class EmployeeAdapter : UR.IEmployee
    {
        Employee adaptee;

        public EmployeeAdapter(Employee adaptee)
        {
            this.adaptee = adaptee;
        }

        public void print()
        {
            string name = adaptee.data.Split(' ')[0];
            string surname = adaptee.data.Split(' ')[1].Split('(')[0];
            string age = adaptee.data.Split('(')[1].Split(')')[0];
            string[] enclosures = adaptee.data.Split('@')[1].Split(",");

            StringBuilder sb = new StringBuilder();
            sb.Append($"{name}, {surname}, {age}, [");
            sb.AppendJoin(", ", enclosures);
            sb.Append("]");

            Console.WriteLine(sb);
        }
    }

    public class VisitorAdapter : UR.IVisitor
    {
        Visitor adaptee;

        public VisitorAdapter(Visitor adaptee)
        {
            this.adaptee = adaptee;
        }

        public void print()
        {
            string name = adaptee.data.Split(' ')[0];
            string surname = adaptee.data.Split(' ')[1].Split('@')[0];
            string[] enclosures = adaptee.data.Split('@')[1].Split(",");

            StringBuilder sb = new StringBuilder();
            sb.Append($"{name} {surname}, [");
            sb.AppendJoin(", ", enclosures);
            sb.Append("]");

            Console.WriteLine(sb);
        }
    }
}