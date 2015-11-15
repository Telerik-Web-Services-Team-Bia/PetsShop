namespace PetStore.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mocks;
    using PetStore.Services.Data.Contracts;
    using PetStore.Models;
    using PetStore.Services.Data;

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
    }
}
