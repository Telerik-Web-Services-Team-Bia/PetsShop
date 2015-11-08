namespace PetStore.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public class PetStoreDbContext : IdentityDbContext<User>
    {
        public PetStoreDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static PetStoreDbContext Create()
        {
            return new PetStoreDbContext();
        }
    }
}
