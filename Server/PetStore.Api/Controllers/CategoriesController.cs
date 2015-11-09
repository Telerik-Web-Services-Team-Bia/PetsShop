namespace PetStore.Api.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Data.Repositories;
    using Models;
    using PetStore.Models;
    using System.Web.Http;

    public class CategoriesController : ApiController
    {
        private IRepository<Category> categories;

        public CategoriesController(IRepository<Category> categories)
        {
            this.categories = categories;
        }

        public IHttpActionResult GetAllCategories()
        {
            var result = categories.All().ProjectTo<CategoryResponseModel>();

            return this.Ok(result);
        }
    }
}
