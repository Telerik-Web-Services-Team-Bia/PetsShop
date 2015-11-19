namespace PetStore.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Api;
    using Api.Models;
    using Common;
    using Models;
    using Moq;
    using PetStore.Services.Data.Contracts;

    public static class MocksFactory
    {
        private static IQueryable<Category> categories = new List<Category>
        {
            new Category
            {
                Name = "Test category 0"
            },
            new Category
            {
                Name = "Test category 1"
            }
        }.AsQueryable();

        private static IQueryable<Pet> pets = new List<Pet>
        {
            new Pet
            {
                Id = 1,
                Name = "Test pet",
                Price = 10,
                ColorId = 1,
                Color = new Color { Id = 1, Name = "test" },
                SpeciesId = 1,
                Species = new Species { Id = 1, Name = "test", CategoryId = 1, Category = new Category { Id = 1, Name = "test" } },
                UserId = "test",
                User = new User { Age = 1, FirstName = "John", LastName = "Doe" },
                BirthDate = new DateTime(2015, 11, 17),
                IsVaccinated = true,
                Description = "Test",
                Image = new PetImage { Image = string.Empty, PetId = 1 },
                Ratings = new[] { new Rating { Value = 1 } }
            }
        }.AsQueryable();

        public static ApplicationUserManager ApplicationUserManager
        {
            get { return ApplicationUserManagerMock.Create(); }
        }

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

        public static RepositoryMock<Color> GetColorsRepository(int colorsCount = 5)
        {
            var repo = new RepositoryMock<Color>();

            for (int i = 0; i < colorsCount; i++)
            {
                repo.Add(new Color
                {
                    Id = i,
                    Name = "Test color " + i
                });
            }

            return repo;
        }

        public static RepositoryMock<Species> GetSpeciesRepository(int speciesCount = 5)
        {
            var repo = new RepositoryMock<Species>();

            for (int i = 0; i < speciesCount; i++)
            {
                repo.Add(new Species
                {
                    Id = i,
                    Name = "Test species " + i,
                    CategoryId = i
                });
            }

            return repo;
        }

        public static RepositoryMock<Pet> GetPetsRepository(int petsCount = 5)
        {
            var repo = new RepositoryMock<Pet>();

            for (int i = 0; i < petsCount; i++)
            {
                var date = new DateTime(2015, 11, 15);
                var pet = new Pet
                {
                    Id = i,
                    Name = "Test pet " + i,
                    ColorId = i,
                    SpeciesId = i,
                    Price = 10 + i,
                    BirthDate = date.AddDays(i)
                };

                repo.Add(pet);
            }

            return repo;
        }

        public static ICategoriesService GetCategoriesService()
        {
            var categoriesService = new Mock<ICategoriesService>();

            categoriesService.Setup(x => x.All()).Returns(categories);
            categoriesService.Setup(x => x.ByName(It.Is<string>(n => n == "Valid"))).Returns(categories);
            categoriesService.Setup(x => x.ByName(It.Is<string>(n => n == "Invalid"))).Returns(new List<Category>().AsQueryable());
            categoriesService.Setup(x => x.Add(It.IsAny<string>())).Returns(1);

            return categoriesService.Object;
        }

        public static IPetsService GetPetsService()
        {
            var petsService = new Mock<IPetsService>();

            petsService.Setup(x => x.All(It.IsAny<string>(), It.IsAny<string>())).Returns(pets);
            petsService.Setup(x => x.ById(It.Is<int>(i => i == 1))).Returns(pets);
            petsService.Setup(x => x.ById(It.Is<int>(i => i == -1))).Returns(new List<Pet>().AsQueryable());

            var myUser = new User { UserName = "TestUser", Email = "TestUser@mail.com" };
            var myPet = new Pet { User = myUser };
            petsService.Setup(x => x.ById(It.Is<int>(i => i == 5))).Returns(new List<Pet> { myPet }.AsQueryable());

            petsService.Setup(x => x.Add(
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<decimal>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<Pet>()))
                .Returns(1);

            petsService.Setup(x => x.Delete(It.IsAny<Pet>()));

            return petsService.Object;
        }

        public static CategoryDataTransferModel GetInvalidCategoryModel()
        {
            return new CategoryDataTransferModel
            {
                Name = new string('*', ModelsConstants.CategoryNameMaxLength + 5)
            };
        }

        public static CategoryDataTransferModel GetValidCategoryModel()
        {
            return new CategoryDataTransferModel
            {
                Name = new string('*', ModelsConstants.CategoryNameMaxLength - 1)
            };
        }

        public static PetRequestModel GetInvalidPetRequestModel()
        {
            return new PetRequestModel
            {
                Name = "Test pet",
                Price = ModelsConstants.PetMinPrice - 1
            };
        }

        public static PetRequestModel GetValidPetRequestModel()
        {
            return new PetRequestModel
            {
                Name = "Test pet",
                Color = "purple",
                Species = "test",
                Category = "test",
                Description = "test",
                Price = ModelsConstants.PetMinPrice + 1
            };
        }
    }
}
