namespace PetStore.Services.Data
{
    using System;
    using PetStore.Services.Data.Contracts;
    using Models;
    using PetStore.Data.Repositories;
    using System.Linq;

    public class RatingsService : IRatingsService
    {
        private IRepository<Rating> ratings;
        private IRepository<User> users;

        public RatingsService(IRepository<Rating> ratings, IRepository<User> users)
        {
            this.ratings = ratings;
            this.users = users;
        }

        public IQueryable<Rating> ByPet(int id)
        {
            return this.ratings.All().Where(r => r.PetId == id);
        }

        public int Rate(int petId, string userId, int value)
        {
            var allRatings = this.ratings.All().Where(r => r.PetId == petId && r.UserId == userId).ToList();
            var currentRating = new Rating();

            if (allRatings.Count == 0)
            {
                currentRating = new Rating()
                {
                    UserId = userId,
                    PetId = petId,
                    Value = value
                };

                this.ratings.Add(currentRating);
            }
            else
            {
                currentRating = allRatings[0];
                currentRating.Value = value;

                this.ratings.Update(currentRating);
            }

            this.ratings.SaveChanges();

            return currentRating.Value;
        }

        public void Delete(Rating rating)
        {
            this.ratings.Delete(rating);
            this.ratings.SaveChanges();
        }
    }
}
