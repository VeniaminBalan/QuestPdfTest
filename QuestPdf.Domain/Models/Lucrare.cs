namespace QuestPdf.Domain.Models;

public class Lucrare
{
    public string TipLucrare { get; set; } = "";
    public int Pret { get; set; }

    public static Lucrare Create(string tipLucrare, int pret)
    {
        if (string.IsNullOrWhiteSpace(tipLucrare))
            throw new Exception("Selectati lucrarea");

        if (pret < 0)
            throw new Exception("Pretul nu poate fi negativ");

        return new Lucrare
        {
            TipLucrare = tipLucrare,
            Pret = pret
        };
    }
}