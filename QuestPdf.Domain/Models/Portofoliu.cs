namespace QuestPdf.Domain.Models;

public class Portofoliu
{
    private readonly List<Lucrare> _lucrari = new();
    private List<Document> _documente = new();

    public Portofoliu()
    {
    }

    public Portofoliu(List<Document> documente, List<Lucrare> lucrari)
    {
        _documente = documente;
        _lucrari = lucrari;
    }

    public IReadOnlyList<Document> Documente => _documente;
    public IReadOnlyList<Lucrare> Lucrari => _lucrari;

    public void AddLucrare(Lucrare lucrare)
    {
        _lucrari.Add(lucrare);
    }

    public void AddDocument(Document doc)
    {
        _documente.Add(doc);
    }

    public void AddDocumentsSource(List<Document> documente)
    {
        _documente = documente;
    }
}