namespace Containers;


public class KontenerChlodniczy : Kontener, IHazardNotifier
{
    public string Produkt { get; private set; }
    public double Temperatura { get; private set; }
    
    private Dictionary<string, double> minimalneTemperaturyProduktow = new Dictionary<string, double>
    {
        {"Bananas", 13.3},
        {"Chocolate", 18},
        {"Fish", 2},
        {"Meat", -15},
        {"Ice cream", -18},
        {"Frozen pizza", -30},
        {"Cheese", 7.2},
        {"Sausages", 5},
        {"Butter", 20.5},
        {"Eggs", 19}
    };

    public KontenerChlodniczy(double loadMass, double height, double ownWeight, double depth, double maxLoad, string produkt, double temperatura)
        : base(loadMass, height, ownWeight, depth, "C", maxLoad)
    {
        Produkt = produkt;
        SetTemperatura(temperatura); 
    }

    public void SetTemperatura(double temperatura)
    {
        if (minimalneTemperaturyProduktow.TryGetValue(Produkt, out double minimalnaTemperatura) && temperatura < minimalnaTemperatura)
        {
            NotifyHazard();
            throw new InvalidOperationException($"Temperatura dla produktu {Produkt} nie może być niższa niż {minimalnaTemperatura}°C.");
        }

        Temperatura = temperatura;
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

    public override void Unload()
    {
        LoadMass = 0;
    }

    public void NotifyHazard()
    {
        Console.WriteLine($"Uwaga: Niebezpieczna sytuacja w kontenerze chłodniczym o numerze {SerialNumber}.");
    }
    
    public override void WypiszInformacje()
    {
        Console.WriteLine($"Kontener chlodniczy: {SerialNumber}, Produkt: {Produkt} i jego temperatura: {Temperatura}.");
    }
}
