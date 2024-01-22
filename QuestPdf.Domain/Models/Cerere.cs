namespace QuestPdf.Domain.Models;

public enum Status
{
    Inlucru,
    LaReceptie,
    Eliberat,
    Respins,
    Prelungit
}

public class Cerere : IModel
{
    private readonly List<CerereStatus> _stateList = new();


    private Cerere(Guid id, Persoana client, Persoana executant, Persoana responsabil, string nr, DateOnly valabilDeLa,
        DateOnly valabilPanaLa, string nrCadastral, int adaos, string comment, List<CerereStatus> stateList,
        Portofoliu portofoliu)
    {
        Id = id;
        ClientId = client.Id;
        Client = client;
        ExecutantId = executant.Id;
        Executant = executant;
        ResponsabilId = responsabil.Id;
        Responsabil = responsabil;
        ValabilDeLa = valabilDeLa;
        ValabilPanaLa = valabilPanaLa;
        NrCadastral = nrCadastral;
        Adaos = adaos;
        Comment = comment;
        Portofoliu = portofoliu;
        _stateList = stateList;
        Nr = nr;
        Starea = Status.Inlucru;
    }

    private Cerere()
    {
    }

    public Guid Id { get; private set; }
    public Guid ClientId { get; private set; }
    public Persoana Client { get; private set; }

    public Guid ExecutantId { get; private set; }
    public Persoana Executant { get; private set; }

    public Guid ResponsabilId { get; private set; }
    public Persoana Responsabil { get; private set; }

    public string Nr { get; private set; }
    public DateOnly ValabilDeLa { get; }
    public DateOnly ValabilPanaLa { get; }

    public string NrCadastral { get; private set; }
    public int Adaos { get; private set; }
    public string Comment { get; private set; }
    public Status Starea { get; private set; }
    public Portofoliu Portofoliu { get; private set; }
    public IReadOnlyList<CerereStatus> StatusList => _stateList;


    public static Cerere Create(
        Guid id,
        Persoana client,
        Persoana executant,
        Persoana responsabil,
        DateOnly valabilDeLa,
        DateOnly valabilPanaLa,
        string nrCadastral,
        int adaos,
        string nr,
        string comment,
        Portofoliu portofoliu,
        List<CerereStatus> stateList)
    {
        if (stateList.Count() == 0)
            stateList.Add(new CerereStatus
            {
                Id = Guid.NewGuid(),
                Created = DateOnly.FromDateTime(DateTime.Now),
                CerereId = id,
                Starea = Status.Inlucru
            });

        if (string.IsNullOrEmpty(nrCadastral))
            throw new Exception("Nr cadastral gresit");

        var costTotal = adaos;

        foreach (var item in portofoliu.Lucrari) costTotal += item.Pret;

        if (costTotal < 0)
            throw new Exception("Costul total nu poate fi mai mic de 0");

        return new Cerere(id, client, executant, responsabil, nr, valabilDeLa, valabilPanaLa, nrCadastral, adaos,
            comment, stateList, portofoliu);
    }

    public void AddStatus(CerereStatus cerereStatus)
    {
        if (cerereStatus.Created < ValabilDeLa)
            throw new Exception(
                $"data starii ({cerereStatus.Created}) nu poate fi mai devreme de data de cand e valabila cererea");

        if (cerereStatus.Starea == Status.Eliberat && StatusList.Any(c => c.Starea == Status.Eliberat))
            throw new Exception("Cererea poate fi eliberata doar o singura data");

        if (cerereStatus.Starea == Status.Prelungit && cerereStatus.Created < ValabilPanaLa)
            throw new Exception($"Starea \"Prelungit\" poate fi setata dupa data {ValabilPanaLa}");

        _stateList.Add(cerereStatus);
        Starea = SetStatus(_stateList);
    }

    public void SetComment(string _comment)
    {
        Comment = _comment;
    }

    private static Status SetStatus(List<CerereStatus> stari)
    {
        if (stari.Any(c => c.Starea == Status.Eliberat))
            return Status.Eliberat;

        var state = stari
            .Where(s => s.Starea != Status.Prelungit)
            .OrderByDescending(x => x.Created).FirstOrDefault();

        if (state is null)
            return Status.Inlucru;

        return state.Starea;
    }
}