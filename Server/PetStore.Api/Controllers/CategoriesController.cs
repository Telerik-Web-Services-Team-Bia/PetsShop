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
        
        public IHttpActionResult GetAllCategories()
        {
            var result = this.categories.All().ProjectTo<CategoryDataTransferModel>();

            return this.Ok(result);
        }
        
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

        [Authorize]
        public IHttpActionResult Delete(string name)
        {
            var category = this.categories.ByName(name).FirstOrDefault();

            if (category == null)
            {
                return this.NotFound();
            }

            this.categories.Delete(category);

            return this.Ok();
        }

        [Authorize]
        public IHttpActionResult Update(string name, [FromBody] CategoryDataTransferModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var categoryToUpdate = this.categories.ByName(name).FirstOrDefault();

            if (categoryToUpdate == null)
            {
                return this.NotFound();
            }

            categoryToUpdate.Name = category.Name;

            this.categories.Update(categoryToUpdate);

            return this.Ok(categoryToUpdate.Id);
        }
    }
}
