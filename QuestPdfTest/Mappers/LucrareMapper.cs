using QuestPdf.Domain.Models;
using QuestPdfTest.Entities.DataTransferObjects;

namespace QuestPdfTest.Mappers;

public static class LucrareMapper
{
    public static LucrareDto Map(Lucrare lucrare) 
    {
        return new LucrareDto 
        {
            Pret = lucrare.Pret,
            TipLucrare = lucrare.TipLucrare
        };
    }
}
