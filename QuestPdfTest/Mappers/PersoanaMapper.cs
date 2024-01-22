using QuestPdf.Domain.Models;
using QuestPdfTest.Entities.DataTransferObjects;

namespace QuestPdfTest.Mappers;

public static class PersoanaMapper
{
    public static PersoanaDto Map(Persoana persoana) 
    {
        return new PersoanaDto
        {
            Id= persoana.Id,
            Nume= persoana.Nume,
            Prenume= persoana.Prenume,
            IDNP= persoana.IDNP,
            DataNasterii= persoana.DataNasterii,
            Domiciliu= persoana.Domiciliu,
            Email= persoana.Email,
            Telefon= persoana.Telefon,
        };
    }
}
