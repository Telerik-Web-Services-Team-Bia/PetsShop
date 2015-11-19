namespace PetStore.Api.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Models;
    using System.Linq;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using System.Web.Http.Cors;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/pets")]
    public class PetsController : ApiController
    {
        private IPetsService pets;

        public PetsController(IPetsService pets)
        {
            this.pets = pets;
        }

        public IHttpActionResult GetAllPets(string category = null, string sortBy = "ratingDesc")
        {
            var result = this.pets.All(category, sortBy).ProjectTo<PetResponseModel>().ToList();
            return this.Ok(result);
        }

        public IHttpActionResult GetPet(int id)
        {
            var result = this.pets.ById(id).ProjectTo<PetResponseModel>().FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(PetRequestModel pet)
        {
            if (pet == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = this.pets.Add(pet.Name, pet.BirthDate, pet.Species, pet.Category, pet.Description, pet.IsVaccinated, pet.Price, pet.Color, User.Identity.GetUserId(), pet.Image);

            return this.Created(this.Url.ToString(), result);
        }

        [Authorize]
        public IHttpActionResult Put(PetRequestModel pet)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var petToUpdate = this.pets.ById(pet.Id).FirstOrDefault();

            if (petToUpdate == null)
            {
                return this.NotFound();
            }

            if (this.User.Identity.Name == petToUpdate.User.UserName)
            {
                var result = this.pets.Add(pet.Name, pet.BirthDate, pet.Species, pet.Category, pet.Description, pet.IsVaccinated, pet.Price, pet.Color, User.Identity.GetUserId(), pet.Image, petToUpdate);

                return this.Created(this.Url.ToString(), result);
            }

            return this.Unauthorized();
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var pet = this.pets.ById(id).FirstOrDefault();

            if (pet == null)
            {
                return this.NotFound();
            }

            this.pets.Delete(pet);

            return this.Ok();
        }
    }
}
