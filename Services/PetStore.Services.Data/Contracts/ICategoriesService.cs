namespace PetStore.Services.Data.Contracts
{
    using System.Linq;

    using Models;

    public interface ICategoriesService
    {
        IQueryable<Category> All();

        int Add(string name);

        int Update(Category category);

        IQueryable<Category> ByName(string name);

        void Delete(Category category);
    }
}
