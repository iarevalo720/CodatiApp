namespace Core.DTOs
{
    public class HistorialOrdenDTO
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string FechaCompleta => $"{Fecha} {Hora}";
        public string Hora { get; set; }
        public string Descripcion { get; set; }
        public int OrdenId { get; set; }
        public string IdUsuario { get; set; }
    }
}
