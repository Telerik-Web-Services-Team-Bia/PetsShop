namespace PetStore.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PetImage
    {
        [Key, ForeignKey("Pet")]
        public int PetId { get; set; }

        public virtual Pet Pet { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
