namespace Containers;

public class KontenerNaPlyny : Kontener, IHazardNotifier
{
    public bool IsHazardous { get; set; }

    public KontenerNaPlyny(double loadMass, double height, double ownWeight, double depth,  double maxLoad, bool isHazardous)
        : base(loadMass, height, ownWeight, depth, "L", maxLoad)
    {
        IsHazardous = isHazardous;
    }

    public override void Load(double mass)
    {
        double allowedLoad = IsHazardous ? MaxLoad * 0.5 : MaxLoad * 0.9;
        if ((LoadMass + mass) > allowedLoad)
        {
            NotifyHazard(); 
            throw new OverfillException($"Nie można załadować {mass}kg, ponieważ przekroczy to dozwoloną maksymalną masę dla kontenera o ID {SerialNumber}.");
        }

        LoadMass += mass;
    }

    public override void Unload()
    {
        LoadMass = 0;
    }
    
    public void NotifyHazard()
    {
        Console.WriteLine($"Niebezpieczna sytuacja dla kontenera o numerze {SerialNumber}.");
    }
    
    public override void WypiszInformacje()
    {
        Console.WriteLine($"Kontener na plyny: {SerialNumber}, Masa: {LoadMass}, Czy jest szkodliwa? : {IsHazardous}.");
    }
   
}