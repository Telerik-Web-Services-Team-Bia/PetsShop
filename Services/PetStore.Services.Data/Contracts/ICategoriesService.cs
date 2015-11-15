namespace PetStore.Services.Data.Contracts
{
    using Models;
    using System.Linq;

    public interface ICategoriesService
    {
        IQueryable<Category> All();

        int Add(string name);

        IQueryable<Category> ByName(string name);
    }
}
