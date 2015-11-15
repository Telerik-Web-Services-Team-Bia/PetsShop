namespace PetStore.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mocks;
    using Models;
    using Services.Data;
    using Services.Data.Contracts;

    [TestClass]
    public class CategoriesServiceTest
    {
        private ICategoriesService categoriesService;

        private RepositoryMock<Category> categoriesRepo;

        [TestInitialize]
        public void Initialize()
        {
            this.categoriesRepo = MocksFactory.GetCategoriesRepository();
            this.categoriesService = new CategoriesService(this.categoriesRepo);
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var result = this.categoriesService.Add("Test name");
            Assert.AreEqual(1, this.categoriesRepo.SavesCount);
        }

        [TestMethod]
        public void AddShouldCreateCategoryInDatabase()
        {
            string categoryName = "New category";
            this.categoriesService.Add(categoryName);
            var category = this.categoriesRepo
                            .All()
                            .FirstOrDefault(x => x.Name == categoryName);

            Assert.IsNotNull(category);
            Assert.AreEqual(categoryName, category.Name);
        }

        [TestMethod]
        public void AllShouldReturnAllCategoriesFromDatabase()
        {
            var result = this.categoriesService.All().ToList();
            var categoriesDbCount = this.categoriesRepo.All().ToList().Count;

            Assert.AreEqual(categoriesDbCount, result.Count);
        }

        [TestMethod]
        public void ByNameShouldReturnCorrectResultWhenNameExists()
        {
            string categoryName = "Test category 1";
            var result = this.categoriesService.ByName(categoryName).FirstOrDefault();

            Assert.AreEqual(categoryName, result.Name);
        }

        [TestMethod]
        public void ByNameShouldReturnCorrectResultWhenNameDoesNotExist()
        {
            string categoryName = "Nonexistent name";
            var result = this.categoriesService.ByName(categoryName).ToList();

            Assert.AreEqual(0, result.Count);
        }

        // TODO ByName -> what if string is null, empty or contains white spaces only
    }
}
