namespace PetStore.Api.Controllers
{
    using System.Linq;
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
            var result = this.categories.All().ProjectTo<CategoryDataTransferModel>();

            return this.Ok(result);
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult GetCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return this.BadRequest("Name is null, empty or consists only of white spaces.");
            }

            var result = this.categories.ByName(name).ProjectTo<CategoryDataTransferModel>();
            if (result.Count() == 0)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post(CategoryDataTransferModel category)
        {
            if (category == null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = this.categories.Add(category.Name);

            return this.Created(this.Url.ToString(), result);
        }
    }
}
