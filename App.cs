using Collections;

namespace Zoo
{
    internal class App
    {
        Collections.ICollection<IEnclosure> enclosures = new Vector<IEnclosure>();
        Collections.ICollection<IAnimal> animals = new Vector<IAnimal>();
        Collections.ICollection<ISpecies> species  = new BinaryTree<ISpecies>();
        Collections.ICollection<IEmployee> employees = new BinaryTree<IEmployee>();
        Collections.ICollection<IVisitor> visitors = new DoubleLinkList<IVisitor>();

        public void Init(Dictionary<string, IEnclosure> enclosures,
            Dictionary<string, IAnimal> animals,
            Dictionary<string, ISpecies> species,
            Dictionary<string, IEmployee> employees,
            Dictionary<string, IVisitor> visitors)
        {
            foreach (var enclosure in enclosures.Values)
                this.enclosures.Add(enclosure);
            foreach (var animal in animals.Values)
                this.animals.Add(animal);
            foreach (var specie in species.Values)
                this.species.Add(specie);
            foreach (var employee in employees.Values)
                this.employees.Add(employee);
            foreach (var visitor in visitors.Values)
                this.visitors.Add(visitor);
        }
        public void Start()
        {

        }
    }
}
