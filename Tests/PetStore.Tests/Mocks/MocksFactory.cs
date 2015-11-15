﻿namespace PetStore.Tests.Mocks
{
    using System;

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
    }
}
