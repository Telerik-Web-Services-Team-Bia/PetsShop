namespace PetStore.Api.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Models;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    public class CategoriesController : ApiController
    {
        private ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
        {
            this.categories = categories;
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult GetAllCategories()
        {
            var result = this.categories.All().ProjectTo<CategoryResponseModel>();

            return this.Ok(result);
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult GetCategory(string name)
        {
            var result = this.categories.ByName(name).ProjectTo<CategoryResponseModel>();

            return this.Ok(result);
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post(CategoryResponseModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = this.categories.Add(category.Name);

            return this.Created(this.Url.ToString(), result);
        }
    }
}
