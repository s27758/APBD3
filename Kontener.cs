namespace Containers;

public abstract class Kontener
{
    public double LoadMass { get; set; }
    public double Height { get; set; }
    public double OwnWeight { get; set; }
    public double Depth { get; set; }
    public string SerialNumber { get; protected set; }
    public double MaxLoad { get; set; }
    private static int nextNumber = 1; 

    protected Kontener(double loadMass, double height, double ownWeight, double depth, string type,  double maxLoad)
    {
        LoadMass = loadMass;
        Height = height;
        OwnWeight = ownWeight;
        Depth = depth;
        MaxLoad = maxLoad;
        SerialNumber = GenerateId(type);
    }
    
    private string GenerateId(string type)
    {
        return $"KON-{type}-{nextNumber++}"; 
    }
    
    public abstract void Load(double mass);

    public abstract void Unload();


    public abstract void WypiszInformacje();

}