namespace apbd_tutor_3;

public class GasContainer : Container, IHazardNotifier
{
    private double Pressure { get; set; }

    public GasContainer(double height, double weight, double depth, double maxPayload, double pressure) : base(height, weight, depth, maxPayload)
    {
        Pressure = pressure;
        SerialNumber = $"KON-G-{Container.counter++}";
    }

    public override void Load(double cargoMass)
    {
        if (CargoMass + cargoMass > MaxPayload)
        {
            Notify($"Can't load {cargoMass}. Current load is {CargoMass}, max payload is {MaxPayload}");
        }
        else
        {
            CargoMass += cargoMass;
            Console.WriteLine($"{SerialNumber} was loaded with {cargoMass}");
        }
    }

    public override void Empty()
    {
        CargoMass = CargoMass * 0.05;
        Console.WriteLine($"{SerialNumber} is now empty");
    }

    public void Notify(string message) => Console.WriteLine($"{SerialNumber}: {message}");
}