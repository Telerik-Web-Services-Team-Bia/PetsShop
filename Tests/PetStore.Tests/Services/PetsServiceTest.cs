namespace PetStore.Tests.Services
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Mocks;
    using Models;
    using PetStore.Services.Data;
    using PetStore.Services.Data.Contracts;

    [TestClass]
    public class PetsServiceTest
    {
        private IPetsService petsService;

        private RepositoryMock<Category> categoriesRepo;

        private RepositoryMock<Species> speciesRepo;

        private RepositoryMock<Color> colorsRepo;

        private RepositoryMock<Pet> petsRepo;

        [TestInitialize]
        public void Initialize()
        {
            this.categoriesRepo = MocksFactory.GetCategoriesRepository();
            this.speciesRepo = MocksFactory.GetSpeciesRepository();
            this.colorsRepo = MocksFactory.GetColorsRepository();
            this.petsRepo = MocksFactory.GetPetsRepository();

            this.petsService = new PetsService(this.petsRepo, this.speciesRepo, this.categoriesRepo, this.colorsRepo);
        }

        [TestMethod]
        public void AllShouldReturnAllPetsFromDatabase()
        {
            var result = this.petsService.All().ToList();
            var categoriesDbCount = this.petsRepo.All().ToList().Count;

            Assert.AreEqual(categoriesDbCount, result.Count);
        }

        [TestMethod]
        public void AllShouldReturnPetsInCorrectOrderWhenSortByPriceDescending()
        {
            var result = this.petsService.All(category: null, sortBy: "priceDesc").ToList();
            var maxPrice = result.Max(x => x.Price);
            var minPrice = result.Min(x => x.Price);

            bool highestAndLowestPriceArePlacedCorrectly = (result[0].Price == maxPrice) 
                && (result[result.Count - 1].Price == minPrice);

            Assert.IsTrue(highestAndLowestPriceArePlacedCorrectly);
        }

        [TestMethod]
        public void AllShouldReturnPetsInCorrectOrderWhenSortByPriceAscending()
        {
            var result = this.petsService.All(category: null, sortBy: "priceAsc").ToList();
            var maxPrice = result.Max(x => x.Price);
            var minPrice = result.Min(x => x.Price);

            bool highestAndLowestPriceArePlacedCorrectly = (result[0].Price == minPrice)
                && (result[result.Count - 1].Price == maxPrice);

            Assert.IsTrue(highestAndLowestPriceArePlacedCorrectly);
        }

        [TestMethod]
        public void ByIdShouldReturnPetWhenIdExists()
        {
            var result = this.petsService.ById(0).FirstOrDefault();
            Assert.AreEqual(0, result.Id);
        }

        [TestMethod]
        public void ByIdShouldReturnNoPetsWhenIdIsNonexistent()
        {
            var result = this.petsService.ById(-1).ToList();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void AddShouldCreatePetInDatabase()
        {
            string petName = "New pet name";
            var birthDate = new DateTime(2015, 11, 15);
            this.petsService.Add(petName, birthDate, "Species name 0", "Category name 0", string.Empty, false, 100, "Color name 0", "1", string.Empty);

            var pet = this.petsRepo.All().Where(x => x.Name == petName).FirstOrDefault();

            Assert.IsNotNull(pet);
            Assert.AreEqual(petName, pet.Name);
        }

        [TestMethod]
        public void DeleteShouldRemovePetFromDatabase()
        {
            int repoInitialCount = this.petsRepo.All().ToList().Count;
            this.petsService.Delete(new Pet());

            int repoAfterDeleteCount = this.petsRepo.All().ToList().Count;
            Assert.AreEqual(repoInitialCount - 1, repoAfterDeleteCount);
        }
    }
}
