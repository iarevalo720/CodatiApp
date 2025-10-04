using Core.DTOs;
using Core.Entities;
using System.Collections.ObjectModel;
using System.Globalization;

namespace UI.Converters
{
    public class ModelosPorMarcaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Dictionary<int, ObservableCollection<ModeloVehiculo>> modelosPorMarca && parameter is int marcaId)
            {
                if (modelosPorMarca.TryGetValue(marcaId, out var modelos))
                {
                    return modelos;
                }
            }
            return new ObservableCollection<ModeloVehiculo>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}