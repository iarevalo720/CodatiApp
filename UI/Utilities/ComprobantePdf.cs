using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Drawing;

namespace UI.Utilities;
public class ComprobantePdf : IDocument
{
    public string OrdenId { get; set; }
    public string Cliente { get; set; }
    public string CI { get; set; }
    public string Fecha { get; set; }
    public string NumeroComprobante { get; set; }
    public string Timbrado { get; set; }
    public string Vencimiento { get; set; }
    public List<(string descripcion, int monto)> ListaOrdenDetalles { get; set; }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Content()
                .Column(col =>
                {
                    col.Spacing(10);

                    col.Item().Text("Tu comprobante").Bold().FontSize(16);
                    col.Item().Text("");
                    col.Item().Text($"{Fecha}");
                    col.Item().Text($"Cliente: {Cliente}");
                    col.Item().Text($"CI: {CI}");
                    col.Item().Text("");
                    col.Item().Text($"Nro. Comprobante: {NumeroComprobante}");
                    col.Item().Text($"Timbrado: {Timbrado} - Vencimiento del timbrado: {Vencimiento}");
                    col.Item().Text("");

                    col.Item().LineHorizontal(1).LineColor(QuestPDF.Helpers.Colors.Grey.Lighten1);

                    col.Item().Text("");
                    col.Item().Text($"Trabajo por orden #{OrdenId}").Bold();

                    foreach (var Detalles in ListaOrdenDetalles)
                    {
                        col.Item().Text($"{Detalles.descripcion}...........{Detalles.monto:N0} Gs.");
                    }

                    int total = ListaOrdenDetalles.Sum(t => t.monto);
                    col.Item().Text($"TOTAL: {total:N0} Gs.").Bold();
                });
        });
    }
}
