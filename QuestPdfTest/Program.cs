using System.Diagnostics;
using System.Reflection;
using QuestPdf.Domain;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using QuestPdfTest.Mappers;
using SkiaSharp;

QuestPDF.Settings.License = LicenseType.Community;

var cerere = Repository.GetCerere();
var cerereDto = CerereMapper.Map(cerere);

var doc = Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.A5.Landscape());
        page.Margin(5, Unit.Millimetre);
        page.PageColor(Colors.White);
        page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

        page.Header().Element(ComposeHeader);
        page.Content().Element(ComposeContent);
        page.Footer().Element(ComposeFooter);
    });
}).GeneratePdf();

string path = Directory.GetCurrentDirectory()+"\\cerere.pdf";

using var writer = new BinaryWriter(File.OpenWrite(path));
writer.Write(doc);

try
{
    Process.Start($"{path}");
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}




void ComposeFooter(IContainer container)
{
    container.Column(column =>
    {
        column.Item().Row(row =>
        {
            row.Spacing(-280);
            row.RelativeItem().Text("Data și semnătura solicitantului:");
            row.RelativeItem()
                .Text("__________________________________________________________________________");
        });
        
        column.Spacing(10);
        
        column.Item().Row(row =>
        {
            row.ConstantItem(250).Row(r =>
            {
                r.RelativeItem().Text("Data eliberării:");
                r.RelativeItem()
                    .Text("________________________");
                r.Spacing(-100);
            });
            
            
            row.ConstantItem(250).Row(r =>
            {
                r.RelativeItem().Text("Semnătura solicitantului:");
                r.RelativeItem()
                    .Text("________________________");
                r.Spacing(-20);
            });
        });
    });
}
void ComposeHeader(IContainer container)
{
    var titleStyle = TextStyle.Default.FontSize(10).SemiBold().FontColor(Colors.Grey.Darken4);
    
    container.Column(column =>
    {
        column.Item().Text($"Nr Cadastral {cerereDto.NrCadastral}")
            .Style(titleStyle);
        
        column.Item()
            .AlignCenter()
            .Text($"Cerere {cerereDto.Nr}")
            .Style(TextStyle.Default.FontSize(15).Bold().FontColor(Colors.Black));
    });
}
void ComposeContent(IContainer container)
{
    container.Column(column =>
    {
        
        column.Item().Row(row =>
        {
            row.RelativeItem().Padding(10).Column(col =>
            {
                col.Spacing(5);
                col.Item().Row(r =>
                {
                    r.RelativeItem().Column(col =>
                    {
                        col.Item().Element(ComposeResponsabil);
                        col.Item().Element(ComposeCerereData);
                        col.Item().Element(ComposeLucrariData);
                    });
                    
                    r.RelativeItem().Column(col =>
                    {
                        col.Item().Element(ComposeClient);
                        col.Spacing(5);
                        col.Item().Element(ComposeReprezentant);
                    });
                });
            });
        });

        column.Item().Padding(10).Element(ComposeTable);
    });
}
void ComposeExecutant(IContainer container)
{
    container
        .Width(96, Unit.Millimetre)
        .Border(1)
        .Padding(5)
        .Column(column =>
        {
            column.Item().Row(row =>
            {
                row.Spacing(-70);
                row.RelativeItem().Text("Executant:").SemiBold();
                row.RelativeItem().Text(cerereDto.Executant);
            });
            column.Item().Row(row =>
            {
                row.Spacing(-70);
                row.RelativeItem().Text("Telefon:").SemiBold();
                row.RelativeItem().Text(cerere.Executant.Telefon);
            });
            column.Item().Row(row =>
            {
                row.Spacing(-70);
                row.RelativeItem().Text("Email:").SemiBold();
                row.RelativeItem().Text(cerere.Executant.Email);
            });
        
        });
}
void ComposeCerereData(IContainer container)
{
    container
        .PaddingVertical(8)
        .PaddingHorizontal(8)
        .Column(column =>
    {
        column.Item().Row(row =>
        {
            row.ConstantItem(100).Text("Data primirii cererii:").SemiBold();
            row.RelativeItem().Text(cerere.ValabilPanaLa);
        }); 
        
        column.Item().Row(row =>
        {
            row.ConstantItem(100).Text("Data eliberării:").SemiBold();
            row.RelativeItem().Text(cerereDto.Eliberat);
        });
    });
}
void ComposeLucrariData(IContainer container)
{
    container.Column(column =>
    {
        column.Item().Text("Rog să efectuaț:");

        foreach (var lucreare in cerere.Portofoliu.Lucrari)
        {
            column.Item().PaddingHorizontal(10).Text("- " +lucreare.TipLucrare).FontSize(9);
        }
        
        column.Item()
            .Row(row =>
        {
            row.ConstantItem(35).Text("Suma:").SemiBold();
            row.RelativeItem().Text(cerereDto.CostTotal + " MDL");
        });
    });
}
void ComposeReprezentant(IContainer container)
{
    container.Column(column =>
    {
        column.Item().Row(row =>
        {
            row.ConstantItem(65).Text("Reprezentant: ");
            row.RelativeItem().Text("_____________________________________");
        });
        
        column.Spacing(-12);
        
        column.Item().DefaultTextStyle(TextStyle.Default.FontSize(9)).Row(row =>
        {
            row.ConstantItem(135).Row(r =>
            {
                r.ConstantItem(28).Text("Email: ");
                r.RelativeItem().Text("____________________");
            });
            
            row.RelativeItem().Row(r =>
            {
                r.ConstantItem(35).Text("Telefon: ");
                r.RelativeItem().Text("____________________");
            });
        });

        column.Item()
            .DefaultTextStyle(TextStyle.Default.FontSize(8))
            .PaddingHorizontal(10)
            .PaddingVertical(15).
            Column(c =>
        {
            c.Item().Text("Temeiul:");
            c.Item().Text("Mentiuni:");
        });

        column.Item().Element(ComposeExecutant);

    });
}
void ComposeTable(IContainer container)
{
    container.Table(table =>
    {
        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White).ShowOnce();
        
        table.ColumnsDefinition(columns =>
        {
            columns.ConstantColumn(40);   // Nr/O
            columns.ConstantColumn(250);  // Denumirea
            columns.ConstantColumn(50);   // NR
            columns.ConstantColumn(70);   // Data
            columns.RelativeColumn();          // Mentiuni
            columns.RelativeColumn();          // Exemplare
        });

        table.Header(header =>
        {
            IContainer HeaderStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten1);
            header.Cell().Element(HeaderStyle).Text("Nr/O");
            header.Cell().Element(HeaderStyle).AlignCenter().Text("Denumirea documentului");
            header.Cell().Element(HeaderStyle).AlignCenter().Text("Nr");
            header.Cell().Element(HeaderStyle).Text("Data");
            header.Cell().Element(HeaderStyle).Text("Mentiuni");
            header.Cell().Element(HeaderStyle).Text("Exemplare");
        });

        var documente = cerere.Portofoliu.Documente;
        for (int i = 0; i < documente.Count; i++)
        {
            table.Cell().Element(CellStyle).Text(i);
            table.Cell().Element(CellStyle).Text(documente[i].Denumirea);
            table.Cell().Element(CellStyle).Text(documente[i].Nr);
            table.Cell().Element(CellStyle).Text(documente[i].Data);
            table.Cell().Element(CellStyle).Text(documente[i].Mentiuni);
            table.Cell().Element(CellStyle).Text(documente[i].Exemplare);

        } 
    });
    
}
void ComposeResponsabil(IContainer container)
{
    container
        .Width(8, Unit.Centimetre)
        .Border(1)
        .Padding(5)
        .Column(column =>
    {
        column.Item().Row(row =>
        {
            row.Spacing(-70);
            row.RelativeItem().Text("Responsabil").SemiBold();
            row.RelativeItem().Text(cerereDto.Responsabil);
        });
        column.Item().Row(row =>
        {
            row.Spacing(-70);
            row.RelativeItem().Text("Telefon").SemiBold();
            row.RelativeItem().Text(cerere.Responsabil.Telefon);
        });
        column.Item().Row(row =>
        {
            row.Spacing(-70);
            row.RelativeItem().Text("Email").SemiBold();
            row.RelativeItem().Text(cerere.Responsabil.Email);
        });
        
    });
}
void ComposeClient(IContainer container)
{
    container.Column(column =>
    {
        column
            .Item()
            .DefaultTextStyle(TextStyle.Default.FontSize(10).FontColor(Colors.Black))
            .Row(row =>
        {
            row.Spacing(-180);
            row.RelativeItem().Text("De la:");
            row.RelativeItem().Text($"{cerere.Client.Nume} {cerere.Client.Prenume}, {cerere.Client.DataNasterii}, {cerere.Client.IDNP}").SemiBold();
        });
        
        column.Item()
            .Row(c =>
        {
            c.RelativeItem()
                .DefaultTextStyle(TextStyle.Default.FontSize(9).FontColor(Colors.Black))
                .Row(row =>
            {
                row.Spacing(-68);
                row.RelativeItem().Text("Telefon:");
                row.RelativeItem().Text($"{cerere.Client.Telefon}").SemiBold();
            });
            
            c.Spacing(-50);
            
            c.RelativeItem()
                .AlignLeft()
                .DefaultTextStyle(TextStyle.Default.FontSize(9).FontColor(Colors.Black))
                .Row(row =>
            {
                row.Spacing(-100);
                row.RelativeItem().Text("Email:");
                row.RelativeItem().Text(cerere.Client.Email).SemiBold();
            });
        });
        
        column.Item()
            .DefaultTextStyle(TextStyle.Default.FontSize(9).FontColor(Colors.Black))
            .Row(row =>
        {
            row.Spacing(-180);
            row.RelativeItem().Text("Adresa");
            row.RelativeItem().Text(cerere.Client.Domiciliu).SemiBold();
        });
        
    });
}

IContainer DefaultCellStyle(IContainer container, string backgroundColor)
{
    return container
        .Border(1)
        .BorderColor(Colors.Grey.Darken2)
        .Background(backgroundColor)
        .PaddingVertical(1)
        .PaddingHorizontal(2)
        .AlignCenter()
        .AlignMiddle();
}