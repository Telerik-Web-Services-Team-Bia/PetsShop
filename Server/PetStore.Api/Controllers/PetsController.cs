namespace PetStore.Api.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Data.Repositories;
    using Models;
    using PetStore.Models;
    using System.Linq;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using System.Web.Http.Cors;

    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/pets")]
    public class PetsController : ApiController
    {
        private IRepository<Pet> pets;
        private IRepository<Species> species;
        private IRepository<Category> categories;
        private IRepository<Color> colors;

        public PetsController(
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

        [EnableCors("*", "*", "*")]
        public IHttpActionResult GetAllPets(string category = null, string sortBy = "ratingDesc")
        {
            var result = this.pets.All();

            if (category != null)
            {
                result = result.Where(p => p.Species.Category.Name == category);
            }

            switch (sortBy)
            {
                case "priceDesc":
                    return this.Ok(result.OrderByDescending(p => p.Price).ProjectTo<PetResponseModel>());
                case "priceAsc":
                    return this.Ok(result.OrderBy(p => p.Price).ProjectTo<PetResponseModel>());
                case "ratingDesc":
                    return this.Ok(result.ProjectTo<PetResponseModel>().OrderByDescending(p => p.Rating));
                case "ratingAsc":
                    return this.Ok(result.ProjectTo<PetResponseModel>().OrderBy(p => p.Rating));
                case "birthdateDesc":
                    return this.Ok(result.OrderByDescending(p => p.BirthDate).ProjectTo<PetResponseModel>());
                case "birthdateAsc":
                    return this.Ok(result.OrderBy(p => p.BirthDate).ProjectTo<PetResponseModel>());
                default:
                    return this.Ok(result.ProjectTo<PetResponseModel>());
            }
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult GetPet(int id)
        {
            var result = this.pets.All().Where(p => p.Id == id).ProjectTo<PetResponseModel>().First();

            return this.Ok(result);
        }

        [Authorize]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post(PetResponseModel pet)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentPetSpecies = new Species();
            var species = this.species.All().Where(s => s.Name == pet.Species).ToList();

            if (species.Count == 0)
            {
                var currentPetCategory = new Category();
                var categories = this.categories.All().Where(c => c.Name == pet.Category).ToList();

                if (categories.Count == 0)
                {
                    currentPetCategory.Name = pet.Category;
                }
                else
                {
                    currentPetCategory = categories[0];
                }

                currentPetSpecies.Category = currentPetCategory;
                currentPetSpecies.Name = pet.Species;
            }
            else
            {
                currentPetSpecies = species[0];
            }

            var currentPetColor = new Color();
            var colors = this.colors.All().Where(c => c.Name == pet.Color).ToList();

            if (colors.Count == 0)
            {
                currentPetColor.Name = pet.Color;
            }
            else
            {
                currentPetColor = colors[0];
            }

            var currentPet = new Pet()
            {
                Name = pet.Name,
                BirthDate = pet.BirthDate,
                Species = currentPetSpecies,
                Description = pet.Description,
                IsVaccinated = pet.IsVaccinated,
                Price = pet.Price,
                Color = currentPetColor,
                UserId = User.Identity.GetUserId()
            };

            if (pet.Image != null)
            {
                currentPet.Image = new PetImage() { Image = pet.Image };
            }

            this.pets.Add(currentPet);
            this.pets.SaveChanges();

            return this.Created(this.Url.ToString(), currentPet.Id);
        }

        [Authorize]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Delete(int id)
        {
            var pet = this.pets.All().Where(p => p.Id == id).FirstOrDefault();

            if (pet == null)
            {
                return this.NotFound();
            }

            if (this.User.Identity.Name == pet.User.UserName)
            {
                this.pets.Delete(pet);
                this.pets.SaveChanges();

                return this.Ok(pet);
            }

            return this.Unauthorized();
        }
    }
}
