namespace PetStore.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using Models;
    using Services.Data.Contracts;

    [EnableCors("*", "*", "*")]
    public class RatingsController : ApiController
    {
        private IRatingsService ratings;

        public RatingsController(IRatingsService ratings)
        {
            this.ratings = ratings;
        }

        public IHttpActionResult Get(int petId)
        {
            var result = this.ratings.ByPet(petId).ProjectTo<RatingResponseModel>();

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(RatingRequestModel rating)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var value = this.ratings.Rate(rating.PetId, User.Identity.GetUserId(), rating.Value);

            return this.Created(this.Url.ToString(), value);
        }

        [Authorize]
        public IHttpActionResult Put(RatingRequestModel rating)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var value = this.ratings.Rate(rating.PetId, User.Identity.GetUserId(), rating.Value);

            return this.Created(this.Url.ToString(), value);
        }

        [Authorize]
        public IHttpActionResult Delete(string userId, int petId)
        {
            var rating = this.ratings.ByPet(petId).Where(r => r.UserId == userId).FirstOrDefault();

            if (rating == null)
            {
                return this.NotFound();
            }

            if (this.User.Identity.Name == rating.User.UserName)
            {
                this.ratings.Delete(rating);

                return this.Ok();
            }

            return this.Unauthorized();
        }
    }
}
