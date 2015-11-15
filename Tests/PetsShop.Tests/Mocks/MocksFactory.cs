namespace PetsShop.Tests.Mocks
{
    using PetStore.Models;

    public static class MocksFactory
    {
        public static RepositoryMock<Category> GetCategoriesRepository(int categoriesCount = 5)
        {
            var repo = new RepositoryMock<Category>();

            for (int i = 0; i < categoriesCount; i++)
            {
                repo.Add(new Category
                {
                    Name = "Test category " + i
                });
            }

            return repo;
        }
    }
}
