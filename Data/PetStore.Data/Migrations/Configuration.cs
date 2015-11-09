namespace PetStore.Data.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<PetStoreDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PetStoreDbContext context)
        {
            context.Categories.AddOrUpdate(x => x.Name,
                new Category() { Name = "Cat" },
                new Category() { Name = "Dog" },
                new Category() { Name = "Mouse" },
                new Category() { Name = "Hamster"},
                new Category() { Name = "Rabbit"},
                new Category() { Name = "Parrot"}
                );
        }
    }
}
