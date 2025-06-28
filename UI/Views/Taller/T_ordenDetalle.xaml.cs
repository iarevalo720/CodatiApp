using QuestPDF.Fluent;
using UI.Utilities;
using UI.ViewModels.Taller;

namespace UI.Views.Taller;

[QueryProperty(nameof(nroOrden), nameof(nroOrden))]
public partial class T_ordenDetalle : ContentPage
{
    private readonly T_ordenDetalleViewModel _t_ordenDetalleViewModel;
    public int nroOrden { get; set; }
    public T_ordenDetalle(T_ordenDetalleViewModel t_OrdenDetalleViewModel)
    {
        InitializeComponent();
        _t_ordenDetalleViewModel = t_OrdenDetalleViewModel;
        BindingContext = _t_ordenDetalleViewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((T_ordenDetalleViewModel)BindingContext).CargarOrdenCompletoAsync(nroOrden);
    }

    private async void BtnGuardarEstadoOrdenCabecera(object sender, EventArgs e)
    {
        await ((T_ordenDetalleViewModel)BindingContext).ActualizarOrdenCabecera(nroOrden);
    }

    private async void btnIrGestionordenDetalle(object sender, EventArgs e)
    {
        int nroOrdenDetalle = ObtenerIdOrdenDetalle(sender);
        var ruta = $"{nameof(T_gestionOrdenDetalle)}?nroOrdenDetalle={nroOrdenDetalle}";
        await Shell.Current.GoToAsync(ruta);
    }

    private async void btnCrearComprobante(object sender, EventArgs e)
    {
        // 1. Crear el contenido del comprobante
        var comprobante = new ComprobantePdf
        {
            OrdenId = _t_ordenDetalleViewModel.OrdenCompleto.OrdenId.ToString(),
            Cliente = _t_ordenDetalleViewModel.OrdenCompleto.NombreUsuario,
            CI = _t_ordenDetalleViewModel.OrdenCompleto.NroDocumento,
            Fecha = DateTime.Now.ToString("dddd, dd 'de' MMMM 'del' yyyy"),
            NumeroComprobante = "001-001-0000001",
            Timbrado = "1234567890",
            Vencimiento = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy"),
            ListaOrdenDetalles = _t_ordenDetalleViewModel.OrdenCompleto.ListaOrdenDetalleResumenes
                .Select(t => (t.OrdenDetalleName, t.OrdenDetalleMonto))
                .ToList()
        };

        // 2. Generar el PDF en memoria (byte[])
        var documento = comprobante.GeneratePdf();

        // 3. Guardarlo temporalmente en la carpeta de caché
        var tempFileName = $"comprobante_temp_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        var tempPath = Path.Combine(FileSystem.CacheDirectory, tempFileName);
        await File.WriteAllBytesAsync(tempPath, documento);

        // 4. Abrirlo con el visor predeterminado del sistema
        await Launcher.OpenAsync(new OpenFileRequest
        {
            File = new ReadOnlyFile(tempPath)
        });
    }

    private async void btnFinalizarOrden(object sender, EventArgs e)
    {
        await _t_ordenDetalleViewModel.FinalizarOrden(nroOrden);
    }

    private int ObtenerIdOrdenDetalle(object sender)
    {
        Grid grid = (Grid)((Button)sender).Parent;
        return int.Parse(((Label)grid.Children.FirstOrDefault()).Text.Substring(1));
    }
}
