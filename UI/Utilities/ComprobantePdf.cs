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
    public ComprobantePdf()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Content()
                .Column(col =>
                {
                    col.Spacing(10);

                    col.Item().Text("CODATI S.R.L.").Bold().FontSize(30).FontColor("#4F3F9B");
                    col.Item().Text("Tu comprobante").Bold().FontSize(20).FontColor("#4F3F9B");
                    col.Item().LineHorizontal(1).LineColor("#4F3F9B");

                    col.Item().Text($"Fecha de emisión: {Fecha}").FontColor("#4F3F9B");
                    col.Item().Text($"Cliente: {Cliente}").FontColor("#4F3F9B");
                    col.Item().Text($"CI: {CI}").FontColor("#4F3F9B");
                    col.Item().Text($"Nro. Comprobante: {NumeroComprobante}").FontColor("#4F3F9B");
                    col.Item().Text($"Timbrado: {Timbrado} - Vencimiento del timbrado: {Vencimiento}").FontColor("#4F3F9B");

                    col.Item().LineHorizontal(1).LineColor("#4F3F9B");

                    col.Item().Text($"Trabajo por orden #{OrdenId}").Bold().FontColor("#4F3F9B").FontSize(16);

                    foreach (var Detalles in ListaOrdenDetalles)
                    {
                        col.Item().Text($"{Detalles.descripcion}...........{Detalles.monto:N0} Gs.").FontColor("#4F3F9B");
                    }

                    int total = ListaOrdenDetalles.Sum(t => t.monto);
                    col.Item().Text($"TOTAL: {total:N0} Gs.").Bold().FontColor("#4F3F9B").FontSize(20);
                });
        });
    }
}
