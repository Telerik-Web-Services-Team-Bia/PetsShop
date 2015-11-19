namespace PetStore.Services.Data.Contracts
{
    using System.Linq;
    using Models;
    using System;

    public interface IPetsService
    {
        IQueryable<Pet> All(string category = null, string sortBy = "ratingDesc");

        IQueryable<Pet> ById(int id);

        int Add(string name, DateTime birthDate, string speciesName, string categoryName, string description, bool isVaccinated, decimal price, string color, string userId, string image, Pet pet = null);

        void Delete(Pet pet);
    }
}
