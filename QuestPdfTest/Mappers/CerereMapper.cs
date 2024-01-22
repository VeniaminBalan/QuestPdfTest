using QuestPdf.Domain.Models;
using QuestPdfTest.Entities.DataTransferObjects;

namespace QuestPdfTest.Mappers
{
    public static class CerereMapper
    {
        public static CerereDto Map(Cerere cerere) 
        {
            int costTotal = cerere.Adaos;

            foreach (var l in cerere.Portofoliu.Lucrari) 
            {
                costTotal += l.Pret;
            }

            return new CerereDto
            {
                Id = cerere.Id,
                Responsabil = string.Join(' ', cerere.Responsabil.Nume, cerere.Responsabil.Prenume),
                Executant = string.Join(' ', cerere.Executant.Nume, cerere.Executant.Prenume),
                Client = string.Join(' ', cerere.Client.Nume, cerere.Client.Prenume),

                NrCadastral = cerere.NrCadastral,
                ValabilDeLa = cerere.ValabilDeLa,
                ValabilPanaLa = cerere.ValabilPanaLa,
                Comment = cerere.Comment,
                CostTotal = costTotal,
                Nr = cerere.Nr,               

                StareaCererii = cerere.Starea,
                LaReceptie = GetDate(cerere.StatusList.ToList(), Status.LaReceptie),
                Eliberat = GetDate(cerere.StatusList.ToList(), Status.Eliberat),
                Respins = GetDate(cerere.StatusList.ToList(), Status.Respins),
                Prelungit = GetDate(cerere.StatusList.ToList(), Status.Prelungit)
            };
        }

        private static DateOnly? GetDate(List<CerereStatus> stari, Status status)
        {
            var state = stari.OrderByDescending(x => x.Created).FirstOrDefault(d => d.Starea == status);

            if (state is null)
                return null;

            return state.Created;
        }
    }
}
