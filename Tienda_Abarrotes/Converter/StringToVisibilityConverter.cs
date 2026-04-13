using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Tienda_Abarrotes.Converters
{
    // Debe ser public para que pueda ser utilizado en XAML
    public class StringToVisibilityConverter : IValueConverter
    {
        // Convierte un string a Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value as string)
                ? Visibility.Visible   // Muestra el placeholder cuando está vacío
                : Visibility.Collapsed; // Lo oculta cuando hay texto
        }

        // No se utiliza en este escenario, pero es obligatorio implementarlo
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}