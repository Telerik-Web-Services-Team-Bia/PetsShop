namespace PetStore.Api.Controllers
{
    using Microsoft.AspNet.Identity;
    using Models;
    using Services.Data.Contracts;
    using System.Web.Http;
    using System.Web.Http.Cors;

    [EnableCors("*", "*", "*")]
    public class RatingsController : ApiController
    {
        private IRatingsService ratings;

        public RatingsController(IRatingsService ratings)
        {
            this.ratings = ratings;
        }

        [Authorize]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post(RatingRequestModel rating)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var value = this.ratings.Rate(rating.PetId, User.Identity.GetUserId(), rating.Value);

            return this.Created(this.Url.ToString(), value);
        }
    }
}
