using QuestPdf.Domain.Models;

namespace QuestPdfTest.Entities.DataTransferObjects
{
    public class CerereStatusDto
    {
        public Guid Id { get; set; }
        public Status Starea { get; set; }
        public DateOnly Created { get; set; }
    }
}
