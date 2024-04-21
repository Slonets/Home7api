using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Webp;
using Size = SixLabors.ImageSharp.Size;
using Microsoft.Extensions.Configuration;
using Core.Helper;

namespace Core.ShopService
{
    public class FilesService : IFilesService
    {
        private readonly IConfiguration _configuration;
        public FilesService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Метод для збереження зображення, отриманого з форми.
        public string ImageSave(IFormFile image)
        {
            // Отримання рядка значень розмірів зображень із конфігурації.
            var imageSizes = _configuration.GetSection("ImageSizes").Value;           

            // Розділення рядка розмірів на масив строк за роздільником коми.
            var sizes = imageSizes.Split(",");            

            // Генерація унікального імені файлу для збереження зображення.
            string imageName = Guid.NewGuid().ToString() + ".webp";           

            // Цикл для обробки кожного розміру зображення.
            foreach (var size in sizes)
            {
                // Парсинг строкового розміру в ціле число.
                int width = int.Parse(size);             

                // Формування шляху до папки для збереження зображення.
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");                

                // Зменшення розміру зображення з використанням ImageProcessingHelper.
                var bytes = ImageProcessingHelper.ResizeImage(image, width, width);               

                // Збереження зменшеного зображення з вказаним розміром та унікальним іменем файлу.
                System.IO.File.WriteAllBytes(Path.Combine(dir, imageName), bytes);
                
            }
            // Повернення унікального імені файлу, в якому збережено зображення.
            return imageName;
            
        }

        // Метод для збереження зображення, отриманого з вказаного URL.
        public string ImageSave(string url)
        {
           // Генерація унікального імені файлу для збереження зображення.
            string imageName = Guid.NewGuid().ToString() + ".webp";
            
            try
            {
                // Створення об'єкта HttpClient для виконання HTTP-запитів.
                using (HttpClient client = new HttpClient())
                {
                    // Виконання асинхронного GET-запиту до вказаного URL та отримання відповіді.
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    // Перевірка, чи статус відповіді вказує на успішне завершення (наприклад, 200 OK).
                    if (response.IsSuccessStatusCode)
                    {
                        // Зчитування байтів зображення із вмісту відповіді.
                        byte[] imageBytes = response.Content.ReadAsByteArrayAsync().Result;

                        // Отримання рядка значень розмірів зображень із конфігурації.
                        var imageSizes = _configuration.GetSection("ImageSizes").Value;

                        // Розділення рядка розмірів на масив строк за роздільником коми.
                        var sizes = imageSizes.Split(",");

                        // Цикл для обробки кожного розміру зображення.
                        foreach (var size in sizes)
                        {
                            // Парсинг строкового розміру в ціле число.
                            int width = int.Parse(size);

                            // Формування шляху до папки для збереження зображення.
                            var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");

                            // Зменшення розміру зображення з використанням ImageProcessingHelper.
                            var bytes = ImageProcessingHelper.ResizeImage(imageBytes, width, width);

                            // Збереження зменшеного зображення з вказаним розміром та унікальним іменем файлу.
                            System.IO.File.WriteAllBytes(Path.Combine(dir, imageName), bytes);                           
                        }
                    }
                    else
                    {
                        // Виведення повідомлення про невдалу спробу отримати зображення, якщо статус відповіді не вказує на успіх.
                        Console.WriteLine($"Failed to retrieve image. Status code: {response.StatusCode}");                        
                    }
                }
            }
            catch (Exception ex)
            {
                // Виведення повідомлення про помилку, якщо виникає виняток під час обробки запиту.
                Console.WriteLine($"An error occurred: {ex.Message}");                
            }
            // Повернення унікального імені файлу, в якому збережено зображення.
            return imageName;
            
        }

        // Метод для видалення зображення за вказаним іменем файлу.
        public void RemoveImage(string name)
        {
            // Отримання рядка значень розмірів зображень із конфігурації.
            var imageSizes = _configuration.GetSection("ImageSizes").Value;

            // Розділення рядка розмірів на масив строк за роздільником коми.
            var sizes = imageSizes.Split(",");

            // Задання базового імені файлу для видалення.
            string baseImagePath = name;

            // Цикл для обробки кожного розміру зображення
            foreach (var size in sizes)
            {
                // Формування шляху до файлу, який потрібно видалити.
                string imagePathToDelete = Path.Combine(Directory.GetCurrentDirectory(), "images", baseImagePath);
               
                if (File.Exists(imagePathToDelete))
                {
                    // Перевірка і видалення файлу, якщо він існує.
                    File.Delete(imagePathToDelete);                    
                }
            }
        }
    }
}
