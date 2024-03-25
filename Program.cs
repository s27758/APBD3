namespace Containers;

public class Program
{
    public static void Main(string[] args)
    {
        var kontenerowiec1 = new Kontenerowiec(30, 10, 20);
        var kontenerowiec2 = new Kontenerowiec(25, 8, 80);
        
        
        var kontenerNaPlyny = Kontenerowiec.StworzKontener("L", 5, 2, 2, 2.5, 10, true);
        var kontenerNaGaz = Kontenerowiec.StworzKontener("G", 4, 2.5, 2.1, 3, 12, cisnienie: 100);
        var kontenerChlodniczy = Kontenerowiec.StworzKontener("C", 3, 3, 2.2, 3.5, 15, produkt: "Fish", temperatura: 5);
        
        kontenerowiec1.DodajKontener(kontenerNaPlyny);
        kontenerowiec1.DodajKontener(kontenerNaGaz);
        kontenerowiec1.ZaladujLadunekDoKontenera(kontenerNaPlyny.SerialNumber, 4.5);
        
        kontenerowiec2.DodajKontener(kontenerChlodniczy);
        
        var dodatkoweKontenery = new List<Kontener>
        {
            Kontenerowiec.StworzKontener("L", 2, 2.5, 2, 2, 5, isHazardous: false),
            Kontenerowiec.StworzKontener("G", 3, 3, 2.1, 3.2, 8, cisnienie: 150)
        };
        kontenerowiec1.DodajKontenery(dodatkoweKontenery);
        
        kontenerowiec1.UsunKontener(kontenerNaPlyny);
        
        kontenerowiec1.RozladujKontener(kontenerNaGaz);
        
        var nowyKontenerNaPlyny = Kontenerowiec.StworzKontener("L", 1, 2, 1.5, 2.5, 9, true);
        kontenerowiec1.ZastapKontener(0, nowyKontenerNaPlyny);
        
        Kontenerowiec.PrzeniesKontener(kontenerowiec2, kontenerowiec1, kontenerChlodniczy);
        
        kontenerowiec1.WypiszInformacjeOKontenerze(kontenerNaGaz.SerialNumber);
        
        kontenerowiec1.WypiszInformacjeOStatku();
        kontenerowiec2.WypiszInformacjeOStatku();
    }
}