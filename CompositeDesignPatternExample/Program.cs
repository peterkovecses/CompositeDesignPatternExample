namespace CompositeDesignPatternExample
{
    // IComponent interface
    public interface IVehicle
    {
        public int Mileage { get; set; }
        public double Refueling { get; set; }
        double GetConsumption();
    }
    // Leaf class - Leaf node in tree structure
    public class Vehicle : IVehicle
    {
        public string LicensePlateNumber { get; set; }
        public int Mileage { get; set; }
        public double Refueling { get; set; }

        public Vehicle(string licensePlateNumber, int mileage, int refueling)
        {
            LicensePlateNumber = licensePlateNumber;
            Mileage = mileage;
            Refueling = refueling;
        }

        public double GetConsumption()
        {
            return Refueling / Mileage * 100;
        }
    }

    // Composite class - Branch node in tree structure
    public class Fleet : IVehicle
    {
        // If I had objects from multiple leaf classes implementing IVehicle, I could add any of them.
        private readonly List<IVehicle> _vehicles;
        public int Mileage { get; set; }
        public double Refueling { get; set; }

        public Fleet(IVehicle vehicle)
        {
            _vehicles = new List<IVehicle>();
            this.AddVehicle(vehicle);
            SetData();
        }

        public Fleet(ICollection<IVehicle> vehicles)
        {
            _vehicles = new List<IVehicle>();
            this.AddVehicles(vehicles);
            SetData();
        }

        private void SetData()
        {
            Mileage = _vehicles.Select(v => v.Mileage).Sum();
            Refueling = _vehicles.Select(v => v.Refueling).Sum();
        }

        public double GetConsumption()
        {
            return Refueling / Mileage * 100;
        }

        public void AddVehicle(IVehicle vehicle)
        {
            _vehicles.Add(vehicle);
            SetData();
        }

        public void AddVehicles(ICollection<IVehicle> vehicles)
        {
            _vehicles.AddRange(vehicles);
            SetData();
        }
    }

    public static class Print
    {
        // Whether I get an instance of a leaf class or an instance of a composition class, it works the same way.
        public static void PrintConsumption(IVehicle vehicles)
        {
            Console.WriteLine(vehicles.GetConsumption());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var vehicle = new Vehicle("KTT-101", 1000, 95);
            var fleet = new Fleet(vehicle);
            fleet.AddVehicle(new Vehicle("KTT-102", 1100, 99));
            Print.PrintConsumption(vehicle);
            Print.PrintConsumption(fleet);
        }
    }
}
