namespace PetStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;

    public class Species
    {
        private ICollection<Pet> pets;

        public Species()
        {
            this.Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [MaxLength(ModelsConstants.SpeciesNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Pet> Pets
        {
            get
            {
                return this.pets;
            }

            set
            {
                this.pets = value;
            }
        }
    }
}
