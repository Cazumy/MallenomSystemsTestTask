using AvaloniaApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AvaloniaApp.Services
{
    public class ImageApiService(HttpClient http)
    {
        private readonly HttpClient _httpClient = http;

        public async Task<IEnumerable<ImageInfo>?> GetAllAsync()
        {
            try     // на случай проблем с сервером API
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<ImageInfo>>("api/images/all");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<ImageInfo?> CreateAsync(ImageInfo image)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/images/add", image);
                if (!response.IsSuccessStatusCode) return null;
                // В идеале добавить обработку других статусов
                return await response.Content.ReadFromJsonAsync<ImageInfo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<ImageInfo?> UpdateAsync(ImageInfo image)
        {
            if (image.Id <= 0)
            {
                Console.WriteLine($"Идентификатор 0 только для создания объектов: {image.Id}");
                return null;
            }
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/images/update/{image.Id}", image);
                if (!response.IsSuccessStatusCode) return null;
                return await response.Content.ReadFromJsonAsync<ImageInfo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/images/delete/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode);
                    return false;
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
