namespace PetStore.Api.Models
{
    public class RatingResponseModel
    {
        public int PetId { get; set; }

        public string UserId { get; set; }

        public int Value { get; set; }
    }
}