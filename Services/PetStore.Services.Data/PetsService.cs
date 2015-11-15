namespace PetStore.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using Models;
    using PetStore.Data.Repositories;

    public class PetsService : IPetsService
    {
        private IRepository<Pet> pets;
        private IRepository<Species> species;
        private IRepository<Category> categories;
        private IRepository<Color> colors;

        public PetsService(
            IRepository<Pet> pets,
            IRepository<Species> species,
            IRepository<Category> categories,
            IRepository<Color> colors)
        {
            this.pets = pets;
            this.species = species;
            this.categories = categories;
            this.colors = colors;
        }

        public int Add(string name, DateTime birthDate, string speciesName, string categoryName, string description, bool isVaccinated, decimal price, string color, string userId, byte[] image, Pet pet = null)
        {
            var currentPetSpecies = new Species();
            var species = this.species.All().Where(s => s.Name == speciesName).ToList();

            if (species.Count == 0)
            {
                var currentPetCategory = new Category();
                var categories = this.categories.All().Where(c => c.Name == categoryName).ToList();

                if (categories.Count == 0)
                {
                    currentPetCategory.Name = categoryName;
                }
                else
                {
                    currentPetCategory = categories[0];
                }

                currentPetSpecies.Category = currentPetCategory;
                currentPetSpecies.Name = speciesName;
            }
            else
            {
                currentPetSpecies = species[0];
            }

            var currentPetColor = new Color();
            var colors = this.colors.All().Where(c => c.Name == color).ToList();

            if (colors.Count == 0)
            {
                currentPetColor.Name = color;
            }
            else
            {
                currentPetColor = colors[0];
            }

            var currentPet = new Pet();

            if (pet != null)
            {
                currentPet = pet;
            }

            currentPet.Name = name;
            currentPet.BirthDate = birthDate;
            currentPet.Species = currentPetSpecies;
            currentPet.Description = description;
            currentPet.IsVaccinated = isVaccinated;
            currentPet.Price = price;
            currentPet.Color = currentPetColor;
            currentPet.UserId = userId;
            currentPet.Name = name;

            if (image != null)
            {
                currentPet.Image = new PetImage() { Image = image };
            }

            if (pet != null)
            {
                this.pets.Update(currentPet);
            }
            else
            {
                this.pets.Add(currentPet);
            }

            this.pets.SaveChanges();

            return currentPet.Id;
        }

        public IQueryable<Pet> All(string category = null, string sortBy = "ratingDesc")
        {
            var result = this.pets.All();

            if (category != null)
            {
                result = result.Where(p => p.Species.Category.Name == category);
            }

            switch (sortBy)
            {
                case "priceDesc":
                    return result.OrderByDescending(p => p.Price);
                case "priceAsc":
                    return result.OrderBy(p => p.Price);
                case "ratingDesc":
                    return result.OrderByDescending(p => (p.Ratings.Count == 0) ? 0 : (double)p.Ratings.Sum(r => r.Value) / p.Ratings.Count);
                case "ratingAsc":
                    return result.OrderBy(p => (p.Ratings.Count == 0) ? 0 : (double)p.Ratings.Sum(r => r.Value) / p.Ratings.Count);
                case "birthdateDesc":
                    return result.OrderByDescending(p => p.BirthDate);
                case "birthdateAsc":
                    return result.OrderBy(p => p.BirthDate);
                default:
                    return result;
            }
        }

        public IQueryable<Pet> ById(int id)
        {
            return this.pets.All().Where(p => p.Id == id);
        }

        public void Delete(Pet pet)
        {
            this.pets.Delete(pet);
            this.pets.SaveChanges();
        }
    }
}
