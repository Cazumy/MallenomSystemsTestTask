#pragma warning disable CS0618 // Тип или член устарел

using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AvaloniaApp.Models;
using AvaloniaApp.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace AvaloniaApp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ImageApiService _imageApiService;

        [ObservableProperty]
        private ObservableCollection<ImageInfo> images = [];

        /// <summary>
        /// Выбранный элемент в таблице
        /// </summary>
        [ObservableProperty]
        private ImageInfo? selectedImage;
        partial void OnSelectedImageChanged(ImageInfo? value)
        {
            UpdateImageCommand.NotifyCanExecuteChanged();
            DeleteImageCommand.NotifyCanExecuteChanged();
        }
        private bool CanUpdateOrDelete() => SelectedImage != null; // Кликабельные кнопки редиктирования и удаления только при выбранном элементе таблицы

        public MainWindowViewModel(ImageApiService apiService)
        {
            _imageApiService = apiService;
            LoadImages();
        }
        private async void LoadImages()
        {
            var allImages = await _imageApiService.GetAllAsync();
            if (allImages != null)
            {
                foreach (var img in allImages)
                    Images.Add(img);
            }
        }

        [RelayCommand]
        private async Task AddImage(Window window)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Добавить изображение",
                Filters = { new FileDialogFilter { Name = "Изображение", Extensions = { "jpg", "png", "bmp" } } }
            };
            var result = await dlg.ShowAsync(window);

            if (result?.Length > 0)
            {
                var filePath = result[0];

                var name = Path.GetFileNameWithoutExtension(filePath);
                var format = Path.GetExtension(filePath).TrimStart('.');
                var data = await File.ReadAllBytesAsync(filePath);
                var newImage = new ImageInfo(0, name, format, data);

                var createdImage = await _imageApiService.CreateAsync(newImage);
                Images.Add(createdImage);
            }
        }

        [RelayCommand(CanExecute = nameof(CanUpdateOrDelete))]
        private async Task UpdateImage(Window window)
        {
            if (SelectedImage == null)
                return;

            var dlg = new OpenFileDialog
            {
                Title = "Изменить изображение",
                Filters = { new FileDialogFilter { Name = "Изображение", Extensions = { "jpg", "png", "bmp" } } }
            };

            var result = await dlg.ShowAsync(window);
            if (result?.Length > 0)
            {
                var filePath = result[0];
                SelectedImage.Name = Path.GetFileNameWithoutExtension(filePath);
                SelectedImage.Format = Path.GetExtension(filePath).TrimStart('.');
                SelectedImage.Data = await File.ReadAllBytesAsync(filePath);
                await _imageApiService.UpdateAsync(SelectedImage);
                SelectedImage = null;
            }
        }

        [RelayCommand(CanExecute = nameof(CanUpdateOrDelete))]
        private async Task DeleteImage()
        {
            if (SelectedImage == null)
                return;

            await _imageApiService.DeleteAsync(SelectedImage.Id);
            Images.Remove(SelectedImage);
            SelectedImage = null;
        }
        [RelayCommand]
        private void UpdateConnection()
        {
            LoadImages();
        }
    }
}
