namespace PetStore.Services.Data.Contracts
{
    using Models;
    using System.Linq;

    public interface IRatingsService
    {
        int Rate(int PetId, string UserId, int value);

        IQueryable<Rating> ByPet(int id);

        void Delete(Rating rating);
    }
}
