namespace PetStore.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<PetStoreDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PetStoreDbContext context)
        {
            //context.Database.Delete();
            //context.Database.CreateIfNotExists();

            //context.Colors.AddOrUpdate(x => x.Name,
            //    new Color() { Name = "White" },
            //    new Color() { Name = "Black" },
            //    new Color() { Name = "Brown" },
            //    new Color() { Name = "Gray" },
            //    new Color() { Name = "Orange" },
            //    new Color() { Name = "Mixed" }
            //    );

            //context.Categories.AddOrUpdate(x => x.Name,
            //    new Category() { Name = "Cat" },
            //    new Category() { Name = "Dog" },
            //    new Category() { Name = "Mouse" },
            //    new Category() { Name = "Hamster"},
            //    new Category() { Name = "Rabbit"},
            //    new Category() { Name = "Parrot"}
            //    );

            //context.SaveChanges();

            //var catsCategory = context.Categories.Where(c => c.Name == "Cat").First();

            //context.Species.AddOrUpdate(x => x.Name,
            //    new Species() { Name = "Siamese", Category = catsCategory },
            //    new Species() { Name = "Persian", Category = catsCategory },
            //    new Species() { Name = "Himalayan", Category = catsCategory }
            //    );

            //context.SaveChanges();

            //var catSpecies = context.Species.Where(c => c.Name == "Siamese").First();
            //var catColor = context.Colors.Where(c => c.Name == "Brown").First();

            //context.Pets.AddOrUpdate(x => x.Name,
            //    new Pet()
            //    {
            //        Name = "Toshko",
            //        Color = catColor,
            //        BirthDate = new DateTime(2013, 5, 18),
            //        Description = "Very lovely cat",
            //        IsVaccinated = true,
            //        Species = catSpecies,
            //        Price = 80
            //    });
        }
    }
}
