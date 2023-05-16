namespace Zoo
{
    public interface RepresentationFactory
    {
        IEnclosure CreateEnclosure();
        IAnimal CreateAnimal();
        IEmployee CreateEmployee();
        IVisitor CreateVisitor();
        ISpecies CreateSpecies();
    }
    public class FirstRepresentationFactory : RepresentationFactory
    {
        public IAnimal CreateAnimal()
        {
            MainRepresentation.Animal result =
                new MainRepresentation.Animal("", 0, null);
            return new MainRepresentation.AnimalAdapter(result);
        }
        public IEmployee CreateEmployee()
        {
            MainRepresentation.Employee result =
                new MainRepresentation.Employee("", "", 0, new List<MainRepresentation.Enclosure>());
            return new MainRepresentation.EmployeeAdapter(result);
        }
        public IEnclosure CreateEnclosure()
        {
            MainRepresentation.Enclosure result =
                new MainRepresentation.Enclosure("", new List<MainRepresentation.Animal>(), null);
            return new MainRepresentation.EnclosureAdapter(result);
        }
        public ISpecies CreateSpecies()
        {
            MainRepresentation.Species result =
                new MainRepresentation.Species("", new List<MainRepresentation.Species>());
            return new MainRepresentation.SpeciesAdapter(result);
        }
        public IVisitor CreateVisitor()
        {
            MainRepresentation.Visitor result =
                new MainRepresentation.Visitor("", "", new List<MainRepresentation.Enclosure>());
            return new MainRepresentation.VisitorAdapter(result);
        }
    }
}