// En UI/Converters/EstadoColorConverter.cs
using System.Globalization;

namespace UI.Converters
{
    public class EstadoColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string estado)
            {
                return estado switch
                {
                    "A_VERIFICAR" => Color.FromArgb("#FF9800"), // Naranja
                    "ESPERANDO_VEHICULO" => Color.FromArgb("#2196F3"), // Azul
                    "EN_PROCESO" => Color.FromArgb("#CCC"), // Gris
                    "TERMINADO" => Color.FromArgb("#009688"), // Verde azulado
                    "FINALIZADO" => Color.FromArgb("#95eb30"), // Verde claro
                    "RECHAZADO" => Color.FromArgb("#F44336"), // Rojo
                    _ => Color.FromArgb("#000000") // Negro por defecto
                };
            }
            return Color.FromArgb("#000000");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}