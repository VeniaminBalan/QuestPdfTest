namespace QuestPdf.Domain.Models;

public class CerereStatus : IModel
{
    public Guid Id { get; set; }
    public Cerere Cerere { get; set; }
    public Guid CerereId { get; set; }
    public Status Starea { get; set; }
    public DateOnly Created { get; set; }

    public static CerereStatus CreateDefault()
    {
        return new CerereStatus
        {
            Starea = Status.Inlucru
        };
    }
}