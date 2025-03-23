namespace apbd_tutor_3;

public class RefrigeratedContainer : Container
{
    private string? ProductType { get; set; }
    private double MaintainedTemperature { get; set; }
    
    private static Dictionary<string, double> ProductsTemperatures = new Dictionary<string, double>()
    {
        ["Bananas"] = 13.3,
        ["Chocolate"] = 18,
        ["Fish"] = 2,
        ["Meat"] = -15,
        ["Ice cream"] = -18,
        ["Frozen pizza"] = -30,
        ["Cheese"] = 7.2,
        ["Sausages"] = 5,
        ["Butter"] = 20.5,
        ["Eggs"] = 19
    };

    public RefrigeratedContainer(double height, double weight, double depth, double maxPayload, string? productType, double maintainedTemperature) : base(height, weight, depth, maxPayload)
    {
        ProductType = productType;
        MaintainedTemperature = maintainedTemperature;
        SerialNumber = $"KON-R-{Container.counter++}";
        
        if (!ProductsTemperatures.ContainsKey(productType))
        {
            throw new Exception("Unknown product type");
        }

        if (MaintainedTemperature < ProductsTemperatures[productType])
        {
            throw new Exception("Container's temperature can't be lower than the product's required temperature");
        }
    }

    public override void Load(double cargoMass)
    {
        if (CargoMass + cargoMass > MaxPayload)
        {
            throw new OverfillException($"Can't load {cargoMass} to {SerialNumber}. Current load is {CargoMass}, max payload is {MaxPayload}");
        }

        CargoMass += cargoMass;
        Console.WriteLine($"{SerialNumber} was loaded with {cargoMass}");
    }
}