namespace PetStore.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using AutoMapper.QueryableExtensions;
    using Data.Repositories;
    using Models;
    using PetStore.Models;

    [EnableCors("*", "*", "*")]
    public class CategoriesController : ApiController
    {
        private IRepository<Category> categories;

        public CategoriesController(IRepository<Category> categories)
        {
            this.categories = categories;
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult GetAllCategories()
        {
            var result = this.categories.All().ProjectTo<CategoryResponseModel>();

            return this.Ok(result);
        }

        // TODO: Check if we need this!
        public IHttpActionResult GetCategory(string name)
        {
            var result = this.categories.All().Where(c => c.Name == name).ProjectTo<CategoryResponseModel>();

            return this.Ok(result);
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post(CategoryResponseModel category)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var currentCategory = new Category()
            {
                Name = category.Name
            };

            this.categories.Add(currentCategory);
            this.categories.SaveChanges();

            return this.Created(this.Url.ToString(), currentCategory.Id);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var category = this.categories.All().Where(c => c.Id == id).FirstOrDefault();

            if (category == null)
            {
                return this.NotFound();
            }

            // TODO: Add user role - only admin can delete category 
            //if (this.User.IsInRole("Admin")) // this may not work now
            //{
            //    this.categories.Delete(category);
            //    this.categories.SaveChanges();

            //    return this.Ok(category);
            //}

            return this.Unauthorized();
        }
    }
}
