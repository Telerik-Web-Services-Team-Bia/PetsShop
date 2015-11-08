namespace PetStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        private ICollection<Species> species;

        public Category()
        {
            this.Species = new HashSet<Species>();
        }

        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Species> Species
        {
            get
            {
                return this.species;
            }

            set
            {
                this.species = value;
            }
        }
    }
}
