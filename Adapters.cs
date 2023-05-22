using Zoo;

namespace MainRepresentation
{
    public class EnclosureAdapter : IEnclosure
    {
        Enclosure adaptee;
        public EnclosureAdapter(Enclosure adaptee) { this.adaptee = adaptee; }
        public string name 
        { 
            get { return adaptee.name; }
            set { adaptee.name = value; } 
        }
        public IEmployee? employee 
        { 
            get { return adaptee.employee == null ? null : new EmployeeAdapter(adaptee.employee); }
            set { employee = value; }
        }
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
        public string name { get { return adaptee.name; } set { adaptee.name = value; } }
        public int age { get { return adaptee.age; } set { adaptee.age = value; } }
        public ISpecies? species 
        { 
            get { return adaptee.species == null ? null : new SpeciesAdapter(adaptee.species); } 
            set { species = value; }
        }
    }
    public class SpeciesAdapter : ISpecies
    {
        Species adaptee;
        public SpeciesAdapter(Species adaptee) { this.adaptee = adaptee; }
        public string name { get { return adaptee.name; } set { adaptee.name = value; } }
        public IEnumerable<ISpecies> favouriteFoods
        {
            get
            {
                if(adaptee.favouriteFoods == null) yield break;
                foreach (var species in adaptee.favouriteFoods)
                    yield return new SpeciesAdapter(species);
            }
        }
    }
    public class EmployeeAdapter : IEmployee
    {
        Employee adaptee;
        public EmployeeAdapter(Employee adaptee) { this.adaptee = adaptee; }
        public string name { get { return adaptee.name; } set { adaptee.name = value; } }
        public string surname { get { return adaptee.surname; } set { adaptee.surname = value; } }
        public int age { get { return adaptee.age; } set { adaptee.age = value; } }
        public IEnumerable<IEnclosure> enclosures
        {
            get
            {
                if (adaptee.enclosures == null) yield break;
                foreach (var enclosure in adaptee.enclosures)
                    yield return new EnclosureAdapter(enclosure);
            }
        }
    }
    public class VisitorAdapter : IVisitor
    {
        Visitor adaptee;
        public VisitorAdapter(Visitor adaptee) { this.adaptee = adaptee; }
        public string name { get { return adaptee.name; } set { adaptee.name = value; } }
        public string surname { get { return adaptee.surname; }set { adaptee.surname = value; } }
        public IEnumerable<IEnclosure> visitedEnclosures
        {
            get
            {
                if (adaptee.visitedEnclosures == null) yield break;
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

        public string name { get { return adaptee.data.Split('@')[0]; } set => throw new NotImplementedException(); }
        public IEmployee? employee 
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
                    if (animalsDict.ContainsKey(specie)) yield return new AnimalAdapter(animalsDict[specie], species);
                    else throw new KeyNotFoundException();
            } 
        }

        IEmployee? IEnclosure.employee { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
            set { }
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

        string IAnimal.name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IAnimal.age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ISpecies? IAnimal.species { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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

        string IEmployee.name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IEmployee.surname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IEmployee.age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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

        string IVisitor.name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IVisitor.surname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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

        public string name { get { return adaptee.data["name"]; }
            set
            {
                if (adaptee.data.ContainsKey("name"))
                    adaptee.data["name"] = value;
                else adaptee.data.Add("name", value);
            }
        }
        public IEmployee? employee
        {
            get
            {
                if (!adaptee.data.ContainsKey("employee"))return null;
                var id = int.Parse(adaptee.data["employee"]);
                return new EmployeeAdapter(employees[id], species, employees, enclosures, animalsDict);
            }
        }
        public IEnumerable<IAnimal> animals
        {
            get
            {
                if (!adaptee.data.ContainsKey("animals.Size()")) yield break;
                int size = int.Parse(adaptee.data["animals.Size()"]);
                for (int i = 0; i < size; i++)
                    yield return new AnimalAdapter(animalsDict[int.Parse(adaptee.data[$"animals[{i}]"])], species);
            }
        }

        IEmployee? IEnclosure.employee { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
            set
            {
                if (adaptee.data.ContainsKey("name"))
                    adaptee.data["name"] = value;
                else adaptee.data.Add("name", value);
            }
        }

        public IEnumerable<ISpecies> favouriteFoods
        {
            get
            {
                if (!adaptee.data.ContainsKey("favouriteFoods.Size()")) yield break;
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

        public string name { get { return adaptee.data["name"]; }
            set
            {
                if (adaptee.data.ContainsKey("name"))
                    adaptee.data["name"] = value;
                else adaptee.data.Add("name", value);
            }
        }
        public int age { get { return int.Parse(adaptee.data["age"]); }
            set
            {
                if (adaptee.data.ContainsKey("age"))
                    adaptee.data["age"] = $"{value}";
                else adaptee.data.Add("age", $"{value}");
            }
        }
        public ISpecies? species
        {
            get
            {
                if (!adaptee.data.ContainsKey("species")) return null;
                return new SpeciesAdapter(speciesDict[int.Parse(adaptee.data["species"])], speciesDict);
            }
        }

        ISpecies? IAnimal.species { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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

        public string name { get { return adaptee.data["name"]; }
            set
            {
                if (adaptee.data.ContainsKey("name"))
                    adaptee.data["name"] = value;
                else adaptee.data.Add("name", value);
            }
        }
        public string surname { get { return adaptee.data["surname"]; }
            set
            {
                if (adaptee.data.ContainsKey("surname"))
                    adaptee.data["surname"] = value;
                else adaptee.data.Add("surname", value);
            }
        }
        public int age { get { return int.Parse(adaptee.data["age"]); }
            set
            {
                if (adaptee.data.ContainsKey("age"))
                    adaptee.data["age"] = $"{value}";
                else adaptee.data.Add("age", $"{value}");
            }
        }
        public IEnumerable<IEnclosure> enclosures
        {
            get
            {
                if (!adaptee.data.ContainsKey("enclosures.Size()")) yield break;
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

        public string name { get { return adaptee.data["name"]; }
            set
            {
                if (adaptee.data.ContainsKey("name"))
                    adaptee.data["name"] = value;
                else adaptee.data.Add("name", value);
            }
        }
        public string surname { get { return adaptee.data["surname"]; }
            set
            {
                if (adaptee.data.ContainsKey("surname"))
                    adaptee.data["surname"] = value;
                else adaptee.data.Add("surname", value);
            }
        }
        public IEnumerable<IEnclosure> visitedEnclosures
        {
            get
            {
                if (!adaptee.data.ContainsKey("visitedEnclosures.Size()")) yield break;
                int size = int.Parse(adaptee.data["visitedEnclosures.Size()"]);
                for (int i = 0; i < size; i++)
                    yield return new EnclosureAdapter(enclosureDict
                        [int.Parse(adaptee.data[$"visitedEnclosures[{i}]"])],
                        speciesDict, employeeDict, enclosureDict, animalsDict);
            }
        }
    }
}