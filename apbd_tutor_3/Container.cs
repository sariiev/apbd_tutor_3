namespace apbd_tutor_3;

public abstract class Container
{
    protected static int counter = 0;
    protected double CargoMass { get; set; } = 0;
    public double Height { get; init; }
    public double ContainerWeight { get; init; }
    public double Depth { get; init; }
    public string SerialNumber { get; init; }
    public double MaxPayload { get; init; }

    public double TotalWeight { get => ContainerWeight + CargoMass; }

    protected Container()
    {
        
    }

    protected Container(double height, double containerWeight, double depth, double maxPayload)
    {
        Height = height;
        ContainerWeight = containerWeight;
        Depth = depth;
        MaxPayload = maxPayload;
    }

    public virtual void Load(double cargoMass)
    {
        if (CargoMass + cargoMass > MaxPayload)
        {
            throw new OverfillException($"Can't load {cargoMass} to {SerialNumber}. Current load is {CargoMass}, max payload is {MaxPayload}");
        }

        CargoMass += cargoMass;
        Console.WriteLine($"{SerialNumber} was loaded with {cargoMass}");
    }

    public virtual void Empty()
    {
        CargoMass = 0;
        Console.WriteLine($"{SerialNumber} is now empty");
    }

    public override string ToString()
    {
        return
            $"{SerialNumber}: Total weight: {TotalWeight} (Container: {ContainerWeight}, Cargo: {CargoMass}), Max payload: {MaxPayload}, Height: {Height}, Depth: {Depth}";
    }
}