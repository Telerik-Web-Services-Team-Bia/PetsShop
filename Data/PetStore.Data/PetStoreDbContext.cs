namespace PetStore.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;

    public class PetStoreDbContext : IdentityDbContext<User>
    {
        public PetStoreDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Color> Colors { get; set; }

        public virtual IDbSet<Pet> Pets { get; set; }

        public virtual IDbSet<PetImage> Images { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public virtual IDbSet<Species> Species { get; set; }

        public static PetStoreDbContext Create()
        {
            return new PetStoreDbContext();
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Category>().HasMany(i => i.Species).WithRequired().WillCascadeOnDelete(false);
        //}
    }
}
