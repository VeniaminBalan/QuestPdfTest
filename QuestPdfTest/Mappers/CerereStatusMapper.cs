using QuestPdf.Domain.Models;
using QuestPdfTest.Entities.DataTransferObjects;

namespace QuestPdfTest.Mappers;

public static class CerereStatusMapper
{
    public static CerereStatusDto Map(CerereStatus cerereStatus) 
    {
        return new CerereStatusDto 
        {
            Created= cerereStatus.Created,
            Starea= cerereStatus.Starea,
            Id= cerereStatus.Id,
        };
    }
}
