namespace apbd_tutor_3;

class Program
{
    public static void Main(string[] args)
    {
        test();
    }
    
    static void test()
    {
        Console.WriteLine("Containers creation");
        Container liquidContainer = new LiquidContainer(100, 100, 100, 500, true);
        Container gasContainer = new GasContainer(100, 100, 100, 300, 10);
        Container refrigeratedContainer = new RefrigeratedContainer(100, 100, 100, 200, "Bananas", 15);
        try
        {
            Container failedRefrigeratedContainer = new RefrigeratedContainer(100, 100, 100, 200, "Bananas", -1);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Exception: {exception.Message}");
        }
        Container[] containers = { liquidContainer, gasContainer, refrigeratedContainer };
        Console.WriteLine(liquidContainer);
        Console.WriteLine(gasContainer);
        Console.WriteLine(refrigeratedContainer);
        Console.WriteLine();

        Console.WriteLine("Containers overfilling");
        foreach (Container container in containers)
        {
            OverfillContainer(container);
            Console.WriteLine(container);
        }
        Console.WriteLine();

        Console.WriteLine("Containers filling");
        foreach (Container container in containers)
        {
            container.Load(container.MaxPayload * 0.5);
            Console.WriteLine(container);
        }
        Console.WriteLine();
        
        Console.WriteLine("Containers emptying");
        foreach (Container container in containers)
        {
            container.Empty();
            Console.WriteLine(container);
        }
        Console.WriteLine();
        
        Console.WriteLine("Containers filling after emptying");
        foreach (Container container in containers)
        {
            container.Load(container.MaxPayload * 0.5);
            Console.WriteLine(container);
        }
        Console.WriteLine();

        Console.WriteLine("Ship creation");
        Ship ship = new Ship(10, 2, 650);
        Ship anotherShip = new Ship(5, 1, 400);
        Console.WriteLine(ship);
        Console.WriteLine();
        
        Console.WriteLine("Loading containers one by one");
        try
        {
            foreach (Container container in containers)
            {
                ship.LoadContainer(container);
            }
        } catch (OverfillException exception)
        {
            Console.WriteLine($"Exception: {exception.Message}");
        }
        Console.WriteLine(ship);
        Console.WriteLine();

        Console.WriteLine("Removing containers by serial numbers");
        foreach (Container container in containers)
        {
            ship.RemoveContainer(container.SerialNumber);
        }
        Console.WriteLine(ship);
        Console.WriteLine();

        Console.WriteLine("Loading a list of containers");
        List<Container> containersList = new List<Container> { liquidContainer, gasContainer};
        ship.LoadContainers(containersList);
        Console.WriteLine(ship);
        Console.WriteLine();
        
        Console.WriteLine("Container replacement");
        ship.ReplaceContainerWith(gasContainer.SerialNumber, refrigeratedContainer);
        Console.WriteLine(ship);
        Console.WriteLine();

        Console.WriteLine("Container transferring");
        Ship.TransferContainer(liquidContainer.SerialNumber, ship, anotherShip);
        Console.WriteLine(ship);
        Console.WriteLine(anotherShip);
        Console.WriteLine();

        Console.WriteLine("Ship emptying");
        ship.Empty();
        Console.WriteLine(ship);
        Console.WriteLine();
    }

    static void OverfillContainer(Container container)
    {
        try
        {
            if (container is LiquidContainer)
            {
                LiquidContainer liquidContainer = (LiquidContainer)container;
                if (liquidContainer.StoresHazardous)
                {
                    liquidContainer.Load(liquidContainer.MaxPayload * 0.51);
                }
                else
                {
                    liquidContainer.Load(liquidContainer.MaxPayload * 0.91);
                }
            }
            else
            {
                container.Load(container.MaxPayload * 1.01);
            }
        }
        catch (OverfillException exception)
        {
            Console.WriteLine($"Exception: {exception.Message}");
        }
    }

    static Container FindContainerBySerialNumber(List<Container> containers, string serialNumber)
    {
        foreach (Container container in containers)
        {
            if (container.SerialNumber.Equals(serialNumber))
            {
                return container;
            }
        }

        return null;
    }

    static Ship FindShipById(List<Ship> ships, int id)
    {
        foreach (Ship ship in ships)
        {
            if (ship.Id == id)
            {
                return ship;
            }
        }

        return null;
    }
}

