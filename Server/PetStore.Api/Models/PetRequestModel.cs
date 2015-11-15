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

        [MaxLength(ModelsConstants.SpeciesNameMaxLength)]
        public string Species { get; set; }

        [MaxLength(ModelsConstants.CategoryNameMaxLength)]
        public string Category { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsVaccinated { get; set; }

        public string Description { get; set; }

        [Range(ModelsConstants.PetMinPrice, ModelsConstants.PetMaxPrice)]
        public decimal Price { get; set; }

        public byte[] Image { get; set; }
    }
}