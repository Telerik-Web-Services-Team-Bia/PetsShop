namespace PetStore.Api.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class PetRequestModel
    {
        public int Id { get; set; }

        [MaxLength(ModelsConstants.PetNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ModelsConstants.ColorNameMaxLength)]
        public string Color { get; set; }

        [Required]
        [MaxLength(ModelsConstants.SpeciesNameMaxLength)]
        public string Species { get; set; }

        [Required]
        [MaxLength(ModelsConstants.CategoryNameMaxLength)]
        public string Category { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public bool IsVaccinated { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(ModelsConstants.PetMinPrice, ModelsConstants.PetMaxPrice)]
        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}