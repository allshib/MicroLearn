using Microsoft.EntityFrameworkCore;

namespace Micro.PlaformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var servicScope = app.ApplicationServices.CreateScope())
            {
                SeedData(servicScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }

        }   

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("Миграция начата");
                try
                {
                    context.Database.Migrate();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Миграция не удалась: {ex.Message}");
                }
                
            }


            if(!context.Platforms.Any()) {
                Console.WriteLine("--> Seeding Date... ");

                context.Platforms.AddRange(
                new Models.Platform
                        {
                            Name ="Dot Net",
                            Publisher="Microsoft",
                            Cost="Free"
                        },
                new Models.Platform
                {
                    Name = "SQL Server Express",
                    Publisher = "Microsoft",
                    Cost = "Free"
                },
                new Models.Platform
                {
                    Name = "Cubernetes",
                    Publisher = "Cloud Native Computing Foundation",
                    Cost = "Free"
                }
                );
                context.SaveChanges(); 
            }
            else
            {
                Console.WriteLine("--> We alredy have data");
            }
        }
    }
}
