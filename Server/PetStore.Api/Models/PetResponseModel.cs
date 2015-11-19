using System;

namespace PetStore.Api.Models
{
    public class PetResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Species { get; set; }

        public string Category { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsVaccinated { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public double Rating { get; set; }

        public byte[] Image { get; set; }

        public string Seller { get; set; }
    }
}