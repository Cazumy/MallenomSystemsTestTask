using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Globalization;
using System.IO;

namespace DesktopImagesController.Services
{
    /// <summary>
    /// Конвертер для преобразования массива байтов в Bitmap
    /// </summary>
    public class ImageConverterService : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not byte[] bytes || bytes.Length == 0)
                return null;

            return new Bitmap(new MemoryStream(bytes));
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
