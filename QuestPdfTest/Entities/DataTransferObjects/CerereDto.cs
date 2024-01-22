using QuestPdf.Domain.Models;

namespace QuestPdfTest.Entities.DataTransferObjects
{
    public class CerereDto
    {
        public Guid Id { get; init; }
        public Guid ClientId { get; init; }
        public string Client { get; init; }
        public Guid ExecutantId { get; init; }
        public string Executant { get; init; }
        public Guid ResponsabilId { get; init; }
        public string Responsabil { get; init; }
        public string NrCadastral { get; init; }
        public DateOnly ValabilDeLa { get; init; }
        public DateOnly ValabilPanaLa { get; init; }
        public DateOnly? Prelungit { get; init; }
        public Status? StareaCererii { get; init; }
        public DateOnly? LaReceptie { get; init; }
        public DateOnly? Eliberat { get; init; }
        public DateOnly? Respins { get; init; }
        public int CostTotal { get; init; }
        public string Comment { get; init; }
        public string Nr { get; init; }

        public override string ToString()
        {
            return new string($"{Id} \n {Client} \n {Executant} \n {Responsabil} \n {NrCadastral}");
        }
    }
}
