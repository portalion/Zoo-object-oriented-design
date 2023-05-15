
namespace Zoo
{
    public interface IEditableFactory
    {
        IEditableByUser CreateBaseRepresentation();
        IEditableByUser CreateThirdRepresentation();
        Collections.ICollection GetCollection();
    }
    public class EnclosureFactory : IEditableFactory
    {
        public IEditableByUser CreateBaseRepresentation() 
        {
            return new MainRepresentation.EnclosureAdapter(
                new MainRepresentation.Enclosure("", 
                new List<MainRepresentation.Animal>(), null));
        }
        public IEditableByUser CreateThirdRepresentation()
        {
            return new ThirdRepresentation.EnclosureAdapter(
                new ThirdRepresentation.Enclosure(0, new Dictionary<string, string>()),
                Zoo.thspecies, Zoo.themployees, Zoo.thenclosures, Zoo.thanimals);
        }
        public Collections.ICollection GetCollection() { return App.GetInstance().enclosures; }
    }
    public class SpeciesFactory : IEditableFactory
    {
        public IEditableByUser CreateBaseRepresentation()
        {
            return new MainRepresentation.SpeciesAdapter(
                new MainRepresentation.Species("", 
                new List<MainRepresentation.Species>()));
        }
        public IEditableByUser CreateThirdRepresentation()
        {
            return new ThirdRepresentation.SpeciesAdapter(
                new ThirdRepresentation.Species(0, new Dictionary<string, string>()),
                Zoo.thspecies);
        }
        public Collections.ICollection GetCollection() { return App.GetInstance().species; }
    }
    public class AnimalFactory : IEditableFactory
    {
        public IEditableByUser CreateBaseRepresentation()
        {
            return new MainRepresentation.AnimalAdapter(
                new MainRepresentation.Animal("", 0, null));
        }
        public IEditableByUser CreateThirdRepresentation()
        {
            return new ThirdRepresentation.AnimalAdapter(
                new ThirdRepresentation.Animal(0, new Dictionary<string, string>()), Zoo.thspecies);
        }
        public Collections.ICollection GetCollection() { return App.GetInstance().animals; }
    }
    public class EmployeeFactory : IEditableFactory
    {
        public IEditableByUser CreateBaseRepresentation()
        {
            return new MainRepresentation.EmployeeAdapter(
                new MainRepresentation.Employee("", "", 0,
                new List<MainRepresentation.Enclosure>()));
        }
        public IEditableByUser CreateThirdRepresentation()
        {
            return new ThirdRepresentation.EmployeeAdapter(
                new ThirdRepresentation.Employee(0, new Dictionary<string, string>()),
                Zoo.thspecies, Zoo.themployees, Zoo.thenclosures, Zoo.thanimals);
        }
        public Collections.ICollection GetCollection() { return App.GetInstance().employees; }
    }
    public class VisitorFactory : IEditableFactory
    {
        public IEditableByUser CreateBaseRepresentation()
        {
            return new MainRepresentation.VisitorAdapter(
                new MainRepresentation.Visitor("", "",
                new List<MainRepresentation.Enclosure>()));
        }
        public IEditableByUser CreateThirdRepresentation()
        {
            return new ThirdRepresentation.VisitorAdapter(
                new ThirdRepresentation.Visitor(0, new Dictionary<string, string>()),
                Zoo.themployees, Zoo.thspecies, Zoo.thenclosures, Zoo.thanimals);
        }
        public Collections.ICollection GetCollection() { return App.GetInstance().visitors; }
    }
}
