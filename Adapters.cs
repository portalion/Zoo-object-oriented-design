using Zoo;

namespace MainRepresentation
{
    public class EnclosureAdapter : IEnclosure
    {
        Enclosure adaptee;
        public EnclosureAdapter(Enclosure adaptee) { this.adaptee = adaptee; }

        public string name { get { return adaptee.name; } }
        public IEmployee employee { get { return new EmployeeAdapter(adaptee.employee); } }
        public IEnumerable<ISpecies> animals
        {
            get
            {
                foreach (var specie in adaptee.animals)
                    yield return new SpeciesAdapter(specie);
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
        public EnclosureAdapter(Enclosure adaptee, Dictionary<string, Species> species, 
            Dictionary<string, Employee> employees, Dictionary<string, Enclosure> enclosures)
        {
            this.adaptee = adaptee;
            this.species = species;
            this.enclosures = enclosures;
            this.employees = employees;
        }

        public string name { get { return adaptee.data.Split('@')[0]; } }
        public IEmployee employee 
        { 
            get 
            {
                var key = adaptee.data.Split('@')[1].Split(',')[0];
                if (employees.ContainsKey(key))
                    return new EmployeeAdapter(employees[key], species, employees, enclosures);
                else throw new KeyNotFoundException();
            } 
        }
        public IEnumerable<ISpecies> animals
        { 
            get 
            {
                var names = adaptee.data.Split(',').Skip(2);
                foreach (var specie in names)
                    if (species.ContainsKey(specie)) yield return new SpeciesAdapter(species[specie], species);
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
        public EmployeeAdapter(Employee adaptee, Dictionary<string, Species> speciesDict,
            Dictionary<string, Employee> employeeDict, Dictionary<string, Enclosure> enclosureDict)
        {
            this.adaptee = adaptee;
            this.speciesDict = speciesDict;
            this.employeeDict = employeeDict;
            this.enclosureDict = enclosureDict;
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
                        yield return new EnclosureAdapter(enclosureDict[key], speciesDict, employeeDict, enclosureDict);
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

        public VisitorAdapter(Visitor adaptee, Dictionary<string, Employee> employeeDict, Dictionary<string, Species> speciesDict, Dictionary<string, Enclosure> enclosureDict)
        {
            this.adaptee = adaptee;
            this.employeeDict = employeeDict;
            this.speciesDict = speciesDict;
            this.enclosureDict = enclosureDict;
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
                            employeeDict, enclosureDict);
                    else throw new KeyNotFoundException();
                }
            }
        }
    }
}