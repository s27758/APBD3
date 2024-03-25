namespace Containers;

public class KontenerNaGaz : Kontener, IHazardNotifier
{
    public double Cisnienie;
    
    public KontenerNaGaz(double loadMass, double height, double ownWeight, double depth,  double maxLoad, double cisnienie)
        : base(loadMass, height, ownWeight, depth, "G", maxLoad)
    {
        Cisnienie = cisnienie;
    }
    
    public void NotifyHazard()
    {
        Console.WriteLine($"Niebezpieczna sytuacja dla kontenera o numerze {SerialNumber}.");
    }
    
    public override void Unload()
    {
        LoadMass *= 0.05;
    }

    public override void Load(double mass)
    {
        if (LoadMass + mass > MaxLoad)
        {
            NotifyHazard(); 
            throw new OverfillException($"Nie można załadować {mass}kg, ponieważ przekroczy to dopuszczalną maksymalną masę dla kontenera o ID {SerialNumber}.");
        }
        LoadMass += mass;
    }
    
    public override void WypiszInformacje()
    {
        Console.WriteLine($"Kontener na gaz: {SerialNumber}, Masa: {LoadMass}, Ciśnienie: {Cisnienie} atmosfer.");
    }

}