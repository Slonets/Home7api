using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using Infrastructure.Models;
using Core.Interfaces;

namespace home7api
{
    public static class SeederDb
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;
                //Отримую посилання на наш контекст
                var context = service.GetRequiredService<ShopDbContext>();
                var imageWoker = service.GetRequiredService<IFilesService>();

                context.Database.Migrate();

                if (!context.Units.Any())
                {
                    Unit[] array = new Unit[]
                    {
                        new Unit() {Name="Ноутбук Acer Aspire 7 A715-76G-560W" },
                        new Unit() {Name="Ноутбук Lenovo IdeaPad Gaming 3 15ACH6 (82K20297RA)"},
                        new Unit() {Name="Ноутбук HP Laptop 15-fd0023ua (825G7EA) Natural Silver"},
                        new Unit() {Name="Ноутбук Lenovo IdeaPad 1 15AMN7 (82VG00HHRA)"},
                        new Unit() {Name="Ноутбук HP Victus Gaming 16-r0014ua (9G9J2EA) Performance Blue" }
                    };

                    // Додаємо всі об'єкти з масиву до бази даних
                    context.Units.AddRange(array);

                    // Зберігаємо зміни в базі даних
                    context.SaveChanges();
                }

                if (!context.Images.Any())
                {
                    ImagesUnit[] array = new ImagesUnit[]
                    {
                        new ImagesUnit() {Path=imageWoker.ImageSave("https://content2.rozetka.com.ua/goods/images/big/362592851.jpg"), UnitId=1},
                        new ImagesUnit() {Path=imageWoker.ImageSave("https://content1.rozetka.com.ua/goods/images/big/382257301.jpg"), UnitId=2},
                        new ImagesUnit() {Path=imageWoker.ImageSave("https://content1.rozetka.com.ua/goods/images/big/392186314.jpg"), UnitId=3},
                        new ImagesUnit() {Path=imageWoker.ImageSave("https://content1.rozetka.com.ua/goods/images/big/334484472.jpg"), UnitId=4},
                        new ImagesUnit() {Path=imageWoker.ImageSave("https://content2.rozetka.com.ua/goods/images/big/380709372.jpg"), UnitId=5}
                    };

                    // Додаємо всі об'єкти з масиву до бази даних
                    context.Images.AddRange(array);

                    // Зберігаємо зміни в базі даних
                    context.SaveChanges();
                }            
            }
        }
    }
}
