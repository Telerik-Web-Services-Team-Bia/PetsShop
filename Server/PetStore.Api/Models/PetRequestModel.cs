﻿namespace PetStore.Api.Models
{
    using System;

    public class PetRequestModel
    {
        public string Name { get; set; }

        public string Color { get; set; }

        public string Species { get; set; }

        public string Category { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsVaccinated { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }
    }
}