using CommunityToolkit.Mvvm.ComponentModel;

namespace DesktopImagesController.Models
{
    public partial class ImageInfo : ObservableObject
    {
        /// <summary>
        /// Класс для создания объектов с полной информацией об изображении
        /// </summary>
        /// <param name="id">Идентификатор для БД</param>
        /// <param name="name">Имя файла</param>
        /// <param name="format">Формат изображения ("jpg", "png")</param>
        /// <param name="data">Содержимое изображения (массив байтов)</param>
        public ImageInfo(int id, string name, string format, byte[] data)
        {
            Id = id;
            Name = name;
            Format = format;
            Data = data;
        }

        public int Id { get; set; }

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string format;

        [ObservableProperty]
        private byte[] data;

        /// <summary>
        /// Полное имя изображения: имя + точка + формат
        /// </summary>
        public string ImageFullName => $"{Name}.{Format}";
        partial void OnNameChanged(string oldValue, string newValue)
            => OnPropertyChanged(nameof(ImageFullName));
        partial void OnFormatChanged(string oldValue, string newValue)
            => OnPropertyChanged(nameof(ImageFullName));
    }
}
