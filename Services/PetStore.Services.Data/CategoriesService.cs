namespace PetStore.Services.Data
{
    using System.Linq;
    using Contracts;
    using Models;
    using PetStore.Data.Repositories;

    public class CategoriesService : ICategoriesService
    {
        private IRepository<Category> categories;

        public CategoriesService(IRepository<Category> categories)
        {
            this.categories = categories;
        }

        public int Add(string name)
        {
            var currentCategory = new Category()
            {
                Name = name
            };

            this.categories.Add(currentCategory);
            this.categories.SaveChanges();

            return currentCategory.Id;
        }

        public IQueryable<Category> All()
        {
            return this.categories.All();
        }

        public IQueryable<Category> ByName(string name)
        {
            return this.categories.All().Where(c => c.Name == name);
        }
    }
}
