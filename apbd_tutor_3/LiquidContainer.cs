namespace apbd_tutor_3;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool StoresHazardous { get; init; }

    public LiquidContainer(double height, double weight, double depth, double maxPayload, bool storesHazardous) : base(height, weight, depth, maxPayload)
    {
        StoresHazardous = storesHazardous;
        SerialNumber = $"KON-L-{Container.counter++}";
    }

    public override void Load(double cargoMass)
    {
        double maxPayload = StoresHazardous ? MaxPayload * 0.5 : MaxPayload * 0.9;
        if (CargoMass + cargoMass > maxPayload)
        {
            Notify($"Can't load {cargoMass}. Current load is {CargoMass}, max payload is {maxPayload}");
        }
        else
        {
            CargoMass += cargoMass;
            Console.WriteLine($"{SerialNumber} was loaded with {cargoMass}");
        }
    }

    public void Notify(string message) => Console.WriteLine($"{SerialNumber}: {message}");
}