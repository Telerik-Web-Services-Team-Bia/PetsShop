namespace PetStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Color
    {
        private ICollection<Pet> pets;
        
        public Color()
        {
            this.Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }

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
