﻿namespace PetStore.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Common;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        [MaxLength(ModelsConstants.UserNamesMaxLength)]
        public string FirstName { get; set; }

        [MaxLength(ModelsConstants.UserNamesMaxLength)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public virtual HashSet<Pet> Pets { get; set; }

        public virtual HashSet<Rating> Ratings { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            
            // Add custom user claims here
            return userIdentity;
        }
    }
}
