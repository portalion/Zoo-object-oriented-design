﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRepresentation
{
    public class Enclosure
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
    }

    public class Animal
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
    }
    public class Species
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
    }

    public class Employee
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
    }

    public class Visitor
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