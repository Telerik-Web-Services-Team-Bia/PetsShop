namespace PetStore.Api.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Data.Repositories;
    using Models;
    using PetStore.Models;
    using System.Web.Http;

    public class PetsController : ApiController
    {
        private IRepository<Pet> pets;

        public PetsController(IRepository<Pet> pets)
        {
            this.pets = pets;
        }

        public IHttpActionResult GetAllCategories()
        {
            var result = pets.All().ProjectTo<PetResponseModel>();

            return this.Ok(result);
        }
    }
}
