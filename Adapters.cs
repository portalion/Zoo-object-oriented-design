using Zoo;

namespace MainRepresentation
{
    public class EnclosureAdapter : IEnclosure
    {
        Enclosure adaptee;
        public EnclosureAdapter(Enclosure adaptee) { this.adaptee = adaptee; }

        public string name { get { return adaptee.name; } }
        public IEmployee employee { get { return new EmployeeAdapter(adaptee.employee); } }
        public IEnumerable<IAnimal> animals
        {
            get
            {
                foreach (var animal in adaptee.animals)
                    yield return new AnimalAdapter(animal);
            }
        }
    }
    public class AnimalAdapter : IAnimal
    {
        Animal adaptee;
        public AnimalAdapter(Animal adaptee) { this.adaptee = adaptee; }
        public string name { get { return adaptee.name; } }
        public int age { get { return adaptee.age; } }
        public ISpecies species { get { return new SpeciesAdapter(adaptee.species); } }
    }
    public class SpeciesAdapter : ISpecies
    {
        Species adaptee;
        public SpeciesAdapter(Species adaptee) { this.adaptee = adaptee; }
        public string name { get { return adaptee.name; } }
        public IEnumerable<ISpecies> favouriteFoods
        {
            get
            {
                foreach (var species in adaptee.favouriteFoods)
                    yield return new SpeciesAdapter(species);
            }
        }
    }
    public class EmployeeAdapter : IEmployee
    {
        Employee adaptee;
        public EmployeeAdapter(Employee adaptee) { this.adaptee = adaptee; }
        public string name { get { return adaptee.name; } }
        public string surname { get { return adaptee.surname; } }
        public int age { get { return adaptee.age; } }
        public IEnumerable<IEnclosure> enclosures
        {
            get
            {
                foreach (var enclosure in adaptee.enclosures)
                    yield return new EnclosureAdapter(enclosure);
            }
        }
    }
    public class VisitorAdapter : IVisitor
    {
        Visitor adaptee;
        public VisitorAdapter(Visitor adaptee) { this.adaptee = adaptee; }
        public string name { get { return adaptee.name; } }
        public string surname { get { return adaptee.surname; } }
        public IEnumerable<IEnclosure> visitedEnclosures
        {
            get
            {
                foreach (var enclosure in adaptee.visitedEnclosures)
                    yield return new EnclosureAdapter(enclosure);
            }
        }
    }
}

namespace SecondRepresentation
{
    public class EnclosureAdapter : IEnclosure
    {
        Enclosure adaptee;
        Dictionary<string, Species> species;
        Dictionary<string, Employee> employees;
        Dictionary<string, Enclosure> enclosures;
        Dictionary<string, Animal> animalsDict;
        public EnclosureAdapter(Enclosure adaptee, Dictionary<string, Species> species, 
            Dictionary<string, Employee> employees, Dictionary<string, Enclosure> enclosures, Dictionary<string, Animal> animalsDict)
        {
            this.adaptee = adaptee;
            this.species = species;
            this.enclosures = enclosures;
            this.employees = employees;
            this.animalsDict = animalsDict;
        }

        public string name { get { return adaptee.data.Split('@')[0]; } }
        public IEmployee employee 
        { 
            get 
            {
                var key = adaptee.data.Split('@')[1].Split(',')[0];
                if (employees.ContainsKey(key))
                    return new EmployeeAdapter(employees[key], species, employees, enclosures, animalsDict);
                else throw new KeyNotFoundException();
            } 
        }
        public IEnumerable<IAnimal> animals
        { 
            get 
            {
                var names = adaptee.data.Split(',').Skip(1);
                foreach (var specie in names)
                    if (species.ContainsKey(specie)) yield return new AnimalAdapter(animalsDict[specie], species);
                    else throw new KeyNotFoundException();
            } 
        }

    }
    public class SpeciesAdapter : ISpecies
    {
        Species adaptee;
        Dictionary<string, Species> species;
        public SpeciesAdapter(Species adaptee, Dictionary<string, Species> species) 
        {
            this.adaptee = adaptee;
            this.species = species;
        }

        public string name
        {
            get
            {
                return adaptee.data.Split('$')[0];
            }
        }

        public IEnumerable<ISpecies> favouriteFoods
        {
            get
            {
                var check = adaptee.data.Split('$')[1];
                if (check == "") yield break;
                var foodsnames = check.Split(',');
                foreach (var specie in foodsnames)
                    if(species.ContainsKey(specie)) yield return new SpeciesAdapter(species[specie], species);
                    else throw new KeyNotFoundException();
            }
        }
    }
    public class AnimalAdapter : IAnimal
    {
        Animal adaptee;
        Dictionary<string, Species> speciesDict;
        public AnimalAdapter(Animal adaptee, Dictionary<string, Species> species)
        {
            this.adaptee = adaptee;
            speciesDict = species;
        }

        public string name { get { return adaptee.data.Split('(')[0]; } }
        public int age {  get { return int.Parse(adaptee.data.Split('(')[1].Split(')')[0]); } }
        public ISpecies species 
        { 
            get 
            {
                string key = adaptee.data.Split('%')[1];
                if(speciesDict.ContainsKey(key)) return new SpeciesAdapter(speciesDict[key], speciesDict);
                else throw new KeyNotFoundException();
            } 
        }
    }
    public class EmployeeAdapter : IEmployee
    {
        Employee adaptee;
        Dictionary<string, Species> speciesDict;
        Dictionary<string, Employee> employeeDict;
        Dictionary<string, Enclosure> enclosureDict;
        Dictionary<string, Animal> animalsDict;
        public EmployeeAdapter(Employee adaptee, Dictionary<string, Species> speciesDict,
            Dictionary<string, Employee> employeeDict, Dictionary<string, Enclosure> enclosureDict, Dictionary<string, Animal> animalsDict)
        {
            this.adaptee = adaptee;
            this.speciesDict = speciesDict;
            this.employeeDict = employeeDict;
            this.enclosureDict = enclosureDict;
            this.animalsDict = animalsDict;
        }

        public string name { get { return adaptee.data.Split(' ')[0]; } }
        public string surname { get { return adaptee.data.Split(' ')[1].Split("(")[0]; } }
        public int age {  get { return int.Parse(adaptee.data.Split('(')[1].Split(')')[0]); } }
        public IEnumerable<IEnclosure> enclosures
        {
            get
            {
                var keys = adaptee.data.Split('@')[1].Split(',');
                foreach (var key in keys)
                    if (enclosureDict.ContainsKey(key))
                        yield return new EnclosureAdapter(enclosureDict[key], speciesDict, employeeDict, enclosureDict, animalsDict);
                    else throw new KeyNotFoundException();
            }
        }
    }
    public class VisitorAdapter : IVisitor
    {
        Visitor adaptee;
        Dictionary<string, Employee> employeeDict;
        Dictionary<string, Species> speciesDict;
        Dictionary<string, Enclosure> enclosureDict;
        Dictionary<string, Animal> animalsDict;

        public VisitorAdapter(Visitor adaptee, Dictionary<string, Employee> employeeDict, Dictionary<string, Species> speciesDict, Dictionary<string, Enclosure> enclosureDict, Dictionary<string, Animal> animalsDict)
        {
            this.adaptee = adaptee;
            this.employeeDict = employeeDict;
            this.speciesDict = speciesDict;
            this.enclosureDict = enclosureDict;
            this.animalsDict = animalsDict;
        }

        public string name { get { return adaptee.data.Split(' ')[0]; } }
        public string surname { get { return adaptee.data.Split(' ')[1].Split("@")[0]; } }
        public IEnumerable<IEnclosure> visitedEnclosures
        {
            get
            {
                var keys = adaptee.data.Split('@')[1].Split(',');
                foreach(var key in keys) 
                {
                    if (enclosureDict.ContainsKey(key))
                        yield return new EnclosureAdapter(enclosureDict[key], speciesDict,
                            employeeDict, enclosureDict, animalsDict);
                    else throw new KeyNotFoundException();
                }
            }
        }
    }
}

namespace ThirdRepresentation
{
    public class EnclosureAdapter : IEnclosure
    {
        Enclosure adaptee;
        Dictionary<int, Species> species;
        Dictionary<int, Animal> animalsDict;
        Dictionary<int, Employee> employees;
        Dictionary<int, Enclosure> enclosures;
        public EnclosureAdapter(Enclosure adaptee, Dictionary<int, Species> species,
            Dictionary<int, Employee> employees, Dictionary<int, Enclosure> enclosures, Dictionary<int, Animal> animals)
        {
            this.adaptee = adaptee;
            this.species = species;
            this.enclosures = enclosures;
            this.employees = employees;
            this.animalsDict = animals;
        }

        public string name { get { return adaptee.data["name"]; } }
        public IEmployee employee
        {
            get
            {
                var id = int.Parse(adaptee.data["employee"]);
                return new EmployeeAdapter(employees[id], species, employees, enclosures, animalsDict);
            }
        }
        public IEnumerable<IAnimal> animals
        {
            get
            {
                int size = int.Parse(adaptee.data["animals.Size()"]);
                for (int i = 0; i < size; i++)
                    yield return new AnimalAdapter(animalsDict[int.Parse(adaptee.data[$"animals[{i}]"])], species);
            }
        }

    }
    public class SpeciesAdapter : ISpecies
    {
        Species adaptee;
        Dictionary<int, Species> species;
        public SpeciesAdapter(Species adaptee, Dictionary<int, Species> species)
        {
            this.adaptee = adaptee;
            this.species = species;
        }

        public string name
        {
            get
            {
                return adaptee.data["name"];
            }
        }

        public IEnumerable<ISpecies> favouriteFoods
        {
            get
            {
                int size = int.Parse(adaptee.data["favouriteFoods.Size()"]);
                for (int i = 0; i < size; i++)
                    yield return new SpeciesAdapter(
                        species[int.Parse(adaptee.data[$"favouriteFoods[{i}]"])], species);
            }
        }
    }
    public class AnimalAdapter : IAnimal
    {
        Animal adaptee;
        Dictionary<int, Species> speciesDict;
        public AnimalAdapter(Animal adaptee, Dictionary<int, Species> species)
        {
            this.adaptee = adaptee;
            speciesDict = species;
        }

        public string name { get { return adaptee.data["name"]; } }
        public int age { get { return int.Parse(adaptee.data["age"]); } }
        public ISpecies species
        {
            get
            {
                return new SpeciesAdapter(speciesDict[int.Parse(adaptee.data["species"])], speciesDict);
            }
        }
    }
    public class EmployeeAdapter : IEmployee
    {
        Employee adaptee;
        Dictionary<int, Animal> animalsDict;
        Dictionary<int, Species> speciesDict;
        Dictionary<int, Employee> employeeDict;
        Dictionary<int, Enclosure> enclosureDict;
        public EmployeeAdapter(Employee adaptee, Dictionary<int, Species> speciesDict,
            Dictionary<int, Employee> employeeDict, Dictionary<int, Enclosure> enclosureDict, Dictionary<int, Animal> animalsDict)
        {
            this.adaptee = adaptee;
            this.speciesDict = speciesDict;
            this.employeeDict = employeeDict;
            this.enclosureDict = enclosureDict;
            this.animalsDict = animalsDict;
        }

        public string name { get { return adaptee.data["name"]; } }
        public string surname { get { return adaptee.data["surname"]; } }
        public int age { get { return int.Parse(adaptee.data["age"]); } }
        public IEnumerable<IEnclosure> enclosures
        {
            get
            {
                int size = int.Parse(adaptee.data["enclosures.Size()"]);
                for (int i = 0; i < size; i++)
                    yield return new EnclosureAdapter(enclosureDict
                        [int.Parse(adaptee.data[$"enclosures[{i}]"])], 
                        speciesDict, employeeDict, enclosureDict, animalsDict);
                
            }
        }
    }
    public class VisitorAdapter : IVisitor
    {
        Visitor adaptee;
        Dictionary<int, Employee> employeeDict;
        Dictionary<int, Species> speciesDict;
        Dictionary<int, Enclosure> enclosureDict;
        Dictionary<int, Animal> animalsDict;

        public VisitorAdapter(Visitor adaptee, Dictionary<int, Employee> employeeDict, Dictionary<int, Species> speciesDict, Dictionary<int, Enclosure> enclosureDict, Dictionary<int, Animal> animalsDict)
        {
            this.adaptee = adaptee;
            this.employeeDict = employeeDict;
            this.speciesDict = speciesDict;
            this.enclosureDict = enclosureDict;
            this.animalsDict = animalsDict;
        }

        public string name { get { return adaptee.data["name"]; } }
        public string surname { get { return adaptee.data["surname"]; } }
        public IEnumerable<IEnclosure> visitedEnclosures
        {
            get
            {
                int size = int.Parse(adaptee.data["visitedEnclosures.Size()"]);
                for (int i = 0; i < size; i++)
                    yield return new EnclosureAdapter(enclosureDict
                        [int.Parse(adaptee.data[$"visitedEnclosures[{i}]"])],
                        speciesDict, employeeDict, enclosureDict, animalsDict);
            }
        }
    }
}