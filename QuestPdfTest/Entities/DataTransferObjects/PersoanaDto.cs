

namespace QuestPdfTest.Entities.DataTransferObjects
{
    public class PersoanaDto
    {
        public Guid Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string IDNP { get; set; }
        public DateOnly DataNasterii { get; set; }
        public string Domiciliu { get; set; }
        public string? Email { get; set; }
        public string? Telefon { get; set; }

        public override string ToString()
        {
            return new string($"{Id} {Nume} {Prenume}({IDNP})");
        }
    }

}
