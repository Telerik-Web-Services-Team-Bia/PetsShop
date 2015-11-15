namespace PetStore.Tests.Services
{
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

        }
    }
}
