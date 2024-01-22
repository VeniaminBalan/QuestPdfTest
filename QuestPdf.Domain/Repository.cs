using QuestPdf.Domain.Models;

namespace QuestPdf.Domain;

public class Repository
{
    public static Cerere GetCerere()
    {
        var client = Persoana.Create(Guid.NewGuid(),
            "Balan",
            "Veniamin",
            "2002500081628",
            DateOnly.Parse("13.08.2002"),
            "or. Drochia",
            "veniamin.balan02@gmail.com",
            "079900846",
            Role.Client);
        
        var responsabil = Persoana.Create(Guid.NewGuid(),
            "Balan",
            "Octavian",
            "19772500081628",
            DateOnly.Parse("16.07.1977"),
            "sat. Gribova",
            "geoproiectgrup@gmail.com",
            "079900218",
            Role.Responsabil);

        var lucrari = new List<Lucrare>
        {
            new Lucrare
            {
                Pret = 1200,
                TipLucrare = "Topografie"
            },
            new Lucrare
            {
                Pret = 3600,
                TipLucrare = "Delimitare de teren"
            }
        };

        var documente = new List<Document>
        {
            new Document
            {
                Denumirea = "Document",
                Nr = "123",
                Data = DateOnly.Parse("12.12.2002"),
                Mentiuni = "Copie",
                Exemplare = 1
            },
            new Document
            {
                Denumirea = "Document2",
                Nr = "1",
                Data = DateOnly.Parse("12.12.2022"),
                Mentiuni = "",
                Exemplare = 2
            },
            new Document
            {
                Denumirea = "Document12454354321313",
                Nr = "1242",
                Data = DateOnly.Parse("12.12.2005"),
                Mentiuni = "",
                Exemplare = 4
            }
        };
        
        var portofoliu = new Portofoliu(documente, lucrari);
        
        return Cerere.Create(
            Guid.NewGuid(), 
            client,
            responsabil,
            responsabil,
            DateOnly.Parse("01.11.2023"),
            DateOnly.Parse("22.11.2023"),
            "12-43-233-2343",
            0,
            "Nr:23/0003",
            "",
            portofoliu,
            new());
    }
}