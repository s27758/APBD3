namespace Containers;
using System.Collections.Generic;

public class Kontenerowiec
{
    public List<Kontener> Kontenery { get; private set; }
    public int MaksymalnaPredkosc { get; set; }
    public int MaksymalnaIloscKontenerow { get; set; }
    public double MaksymalnaWagaKontenerow { get; set; }

    public Kontenerowiec(int maksymalnaPredkosc, int maksymalnaIloscKontenerow, double maksymalnaWagaKontenerow)
    {
        Kontenery = new List<Kontener>();
        MaksymalnaPredkosc = maksymalnaPredkosc;
        MaksymalnaIloscKontenerow = maksymalnaIloscKontenerow;
        MaksymalnaWagaKontenerow = maksymalnaWagaKontenerow;
    }
    
    //Załadowanie ładunku do danego kontenera
    public void ZaladujLadunekDoKontenera(string serialNumber, double masa)
    {
        var kontener = Kontenery.FirstOrDefault(k => k.SerialNumber == serialNumber);

        if (kontener == null)
        {
            throw new InvalidOperationException($"Nie znaleziono kontenera o numerze seryjnym: {serialNumber}");
        }

        try {
            kontener.Load(masa);
        }catch (Exception ex) {
            Console.WriteLine($"Wystąpił problem podczas ładowania ładunku: {ex.Message}");
        }
        
    }
    
    //Stworzenie kontenera danego typu
    public static Kontener StworzKontener(string typ, double loadMass, double height, double ownWeight, double depth, double maxLoad, bool isHazardous = false, double cisnienie = 0, string produkt = "", double temperatura = 0)
    {
        switch (typ)
        {
            case "L":
                return new KontenerNaPlyny(loadMass, height, ownWeight, depth, maxLoad, isHazardous);
            case "G":
                return new KontenerNaGaz(loadMass, height, ownWeight, depth, maxLoad, cisnienie);
            case "C":
                if (!string.IsNullOrEmpty(produkt))
                {
                    return new KontenerChlodniczy(loadMass, height, ownWeight, depth, maxLoad, produkt, temperatura);
                }
                throw new ArgumentException("Produkt musi być określony dla kontenera chłodniczego.");
            default:
                throw new ArgumentException("Nieznany typ kontenera.");
        }
    }

    
    //Załadowanie kontenera na statek
    public void DodajKontener(Kontener kontener)
    {
        if (Kontenery.Count >= MaksymalnaIloscKontenerow)
        {
            throw new InvalidOperationException("Przekroczono maksymalną ilość kontenerów!");
        }
        if ((ObliczLacznaWage() + kontener.LoadMass) > MaksymalnaWagaKontenerow * 1000)
        {
            throw new InvalidOperationException("Przekroczono maksymalną wagę kontenerów!");
        }
        Kontenery.Add(kontener);
    }
    
    //Załadowanie listy kontenerów na statek
    public void DodajKontenery(List<Kontener> konteneryDoDodania)
    {
        foreach (var kontener in konteneryDoDodania)
        {
            DodajKontener(kontener);
        }
    }

    //Usunięcie kontenera ze statku
    public void UsunKontener(Kontener kontener)
    {
        Kontenery.Remove(kontener);
    }
    
    //Rozładowanie kontenera
    public void RozladujKontener(Kontener kontener)
    {
        kontener.Unload();
    }
    
    //Zastąpienie kontenera na statku o danym numerze innym kontenerem
    public void ZastapKontener(int index, Kontener nowyKontener)
    {
        if (index >= 0 && index < Kontenery.Count)
        {
            Kontenery[index] = nowyKontener;
        }
    }
    //Możliwość przeniesienie kontenera między dwoma statkami
    public static void PrzeniesKontener(Kontenerowiec zStatek, Kontenerowiec naStatek, Kontener kontener)
    {
        zStatek.UsunKontener(kontener);
        naStatek.DodajKontener(kontener);
    }
    
    //Wypisanie informacji o danym kontenerze
    public void WypiszInformacjeOKontenerze(string serialNumber)
    {
        var kontener = Kontenery.FirstOrDefault(k => k.SerialNumber == serialNumber);
        if (kontener != null)
        {
            kontener.WypiszInformacje();
        }
        else
        {
            Console.WriteLine("Nie znaleziono kontenera o podanym numerze seryjnym.");
        }
    }

    //Wypisanie informacji o danym statku i jego ładunku
    public void WypiszInformacjeOStatku()
    {
        Console.WriteLine($"Statek ma maksymalną prędkość {MaksymalnaPredkosc} węzłów, może zabrać {MaksymalnaIloscKontenerow} kontenerów, o maksymalnej wadze {MaksymalnaWagaKontenerow} ton.");
        Console.WriteLine("Lista kontenerów na statku:");
        foreach (var kontener in Kontenery)
        {
            kontener.WypiszInformacje();
        }
    }
    
    //Obliczanie wagi w Kontenerach do kontenerowca
    public double ObliczLacznaWage()
    {
        double lacznaWaga = 0;
        foreach (var kontener in Kontenery)
        {
            lacznaWaga += kontener.LoadMass;
        }
        return lacznaWaga;
    }
}
