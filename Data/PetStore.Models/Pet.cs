namespace PetStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Pet
    {
        private ICollection<Rating> ratings;

        public Pet()
        {
            this.Ratings = new HashSet<Rating>();
        }

        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public int SpeciesId { get; set; }

        public virtual Species Species { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsVaccinated { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual PetImage Image { get; set; }

        public ICollection<Rating> Ratings
        {
            get
            {
                return this.ratings;
            }

            set
            {
                this.ratings = value;
            }
        }
    }
}
