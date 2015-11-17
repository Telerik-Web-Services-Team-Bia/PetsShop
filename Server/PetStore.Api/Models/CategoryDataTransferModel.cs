namespace PetStore.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class CategoryDataTransferModel
    {
        [Required]
        [MaxLength(ModelsConstants.CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}