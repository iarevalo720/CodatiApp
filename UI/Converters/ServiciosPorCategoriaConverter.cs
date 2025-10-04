using Core.DTOs;
using System.Collections.ObjectModel;
using System.Globalization;

namespace UI.Converters
{
    public class ServiciosPorCategoriaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Dictionary<int, ObservableCollection<SubCategoriaDTO>> serviciosPorCategoria && parameter is int categoriaId)
            {
                if (serviciosPorCategoria.TryGetValue(categoriaId, out var servicios))
                {
                    return servicios;
                }
            }
            return new ObservableCollection<SubCategoriaDTO>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}