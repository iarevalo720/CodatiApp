namespace Core.DTOs
{
    public class OrdenResumenDTO
    {
        public string? NroOrden { get; set; }
        public string? Estado { get; set; }
        public string? ResumenVehiculo { get; set; }
        public List<string>? SubCategoria { get; set; }
        public string? Observacion { get; set; }
        public bool BtnAceptarEnabled
        {
            get
            {
                return Estado == "A_VERIFICAR" ? true : false;
            }
        }

        public bool BtnRechazarEnabled
        {
            get
            {
                return Estado == "A_VERIFICAR" ? true : false;
            }
        }

        public bool BtnDetallesEnabled
        {
            get
            {
                return Estado != "A_VERIFICAR" ? true : false;
            }
        }
    }
}
