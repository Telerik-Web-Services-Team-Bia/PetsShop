namespace PetStore.Api.Models
{
    public class RatingRequestModel
    {
        public int PetId { get; set; }

        public int Value { get; set; }
    }
}