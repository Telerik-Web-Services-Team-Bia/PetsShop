namespace PetStore.Services.Data.Contracts
{
    using System.Linq;

    using Models;

    public interface IRatingsService
    {
        int Rate(int petId, string userId, int value);

        IQueryable<Rating> ByPet(int id);

        void Delete(Rating rating);
    }
}
