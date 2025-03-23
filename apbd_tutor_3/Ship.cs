namespace apbd_tutor_3;

public class Ship
{
    private static int counter = 0;
    public int Id { get; }
    private HashSet<Container> Containers { get; set; }
    private double Speed { get; set; }
    private int MaxContainers { get; set; }
    private double MaxContainersWeight { get; set; }

    public Ship(double speed, int maxContainers, double maxContainersWeight)
    {
        Id = counter++;
        Containers = new HashSet<Container>();
        Speed = speed;
        MaxContainers = maxContainers;
        MaxContainersWeight = maxContainersWeight;
    }

    public void LoadContainer(Container container)
    {
        if (GetContainersCount() == MaxContainers ||
            GetContainersWeight() + container.TotalWeight > MaxContainersWeight)
        {
            throw new OverfillException(
                $"Can't load {container.SerialNumber} to {Id}, current load: {GetContainersWeight()}(Max load: {MaxContainersWeight})");
        }

        Containers.Add(container);
    }

    public void LoadContainers(List<Container> containers)
    {
        foreach (Container container in containers)
        {
            LoadContainer(container);
        }
    }

    public void RemoveContainer(string containerSerialNumber)
    {
        foreach (Container container in Containers)
        {
            if (container.SerialNumber.Equals(containerSerialNumber))
            {
                Containers.Remove(container);
                return;
            }
        }
    }

    public void ReplaceContainerWith(string serialNumberOfContainerToReplace, Container containerToReplaceWith)
    {
        foreach (Container container in Containers)
        {
            if (container.SerialNumber.Equals(serialNumberOfContainerToReplace))
            {
                if (GetContainersWeight() - container.TotalWeight + containerToReplaceWith.TotalWeight >
                    MaxContainersWeight)
                {
                    throw new OverfillException(
                        $"Can't replace {serialNumberOfContainerToReplace} with {containerToReplaceWith.SerialNumber} at {Id}");
                }
                else
                {
                    Containers.Remove(container);
                    Containers.Add(containerToReplaceWith);
                }

                return;
            }
        }
    }

    public static void TransferContainer(string containerSerialNumber, Ship fromShip, Ship toShip)
    {
        foreach (Container container in fromShip.Containers)
        {
            if (container.SerialNumber.Equals(containerSerialNumber))
            {
                if (toShip.GetContainersCount() == toShip.MaxContainers ||
                    toShip.GetContainersWeight() + container.TotalWeight > toShip.MaxContainersWeight)
                {
                    throw new OverfillException(
                        $"Can't transfer {containerSerialNumber} from {fromShip.Id} to {toShip.Id}");
                }
                else
                {
                    fromShip.Containers.Remove(container);
                    toShip.Containers.Add(container);
                }

                return;
            }
        }
    }

    public void Empty()
    {
        Containers.Clear();
    }

    private int GetContainersCount()
    {
        return Containers.Count;
    }

    private double GetContainersWeight()
    {
        double sum = 0;
        foreach (Container container in Containers)
        {
            sum += container.TotalWeight;
        }

        return sum;
    }

    public override string ToString()
    {
        string repr =
            $"{Id}: Max speed: {Speed}, Max containers: {MaxContainers}, Max containers weight: {MaxContainersWeight}, Containers:";
        foreach (Container container in Containers)
        {
            repr += "\n" + container;
        }

        return repr;
    }
}