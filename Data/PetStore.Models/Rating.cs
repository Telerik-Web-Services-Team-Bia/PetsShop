namespace PetStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rating
    {
        [Key, Column(Order = 0)]
        public int PetId { get; set; }

        public virtual Pet Pet { get; set; }

        [Key, Column(Order = 1)]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int Value { get; set; }
    }
}
