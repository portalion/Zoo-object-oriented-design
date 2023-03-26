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