namespace QuestPdfTest.Entities.DataTransferObjects.ForTable;

public class CerereDtoForTable
{
    public Guid Id { get; set; }
    public string Client { get; set; }
    public string Executant { get; set; }
    public string Responsabil { get; set; }
    public string NrCadastral { get; set; }
    public DateOnly ValabilDeLa { get; set; }
    public DateOnly ValabilPanaLa { get; set; }
    public DateOnly Prelungit { get; set; }
    public string StareaCererii { get; set; }
    public DateOnly LaReceptie { get; set; }
    public DateOnly Eliberat { get; set; }
    public DateOnly Respins { get; set; }
}
