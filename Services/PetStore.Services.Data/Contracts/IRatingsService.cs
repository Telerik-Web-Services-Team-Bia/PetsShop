namespace PetStore.Services.Data.Contracts
{
    public interface IRatingsService
    {
        int Rate(int PetId, string UserId, int value);
    }
}
