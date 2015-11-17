namespace PetStore.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using Api.Controllers;
    using Mocks;
    using Models;
    using Api.Models;
    using System.Linq;
    using System.Collections.Generic;

    [TestClass]
    public class PetsControllerTests
    {
        private IControllerBuilder<PetsController> petsController;

        [TestInitialize]
        public void Initialize()
        {
            this.petsController = MyWebApi
                .Controller<PetsController>()
                .WithResolvedDependencyFor(MocksFactory.GetPetsService());

            AutoMapper.Mapper.CreateMap<Pet, PetResponseModel>();
            AutoMapper.Mapper.CreateMap<Pet, PetResponseModel>()
                .ForMember(dest => dest.Color,
                           opts => opts.MapFrom(src => src.Color.Name))
                .ForMember(dest => dest.Species,
                           opts => opts.MapFrom(src => src.Species.Name))
                .ForMember(dest => dest.Category,
                           opts => opts.MapFrom(src => src.Species.Category.Name))
                .ForMember(dest => dest.Rating,
                           opts => opts.MapFrom(src => (src.Ratings.Count == 0) ? 0 : (double)src.Ratings.Sum(r => r.Value) / src.Ratings.Count))
                .ForMember(dest => dest.Image,
                           opts => opts.MapFrom(src => src.Image.Image));
        }

        [TestMethod]
        public void GetAllShouldReturnOkWithData()
        {
            this.petsController
                .Calling(p => p.GetAllPets(With.Any<string>(), With.Any<string>()))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<PetResponseModel>>()
                .Passing(p => p.Count == 1);
        }

        [TestMethod]
        public void GetPetShouldReturnOkWithDataWhenIdExists()
        {
            this.petsController
                .Calling(p => p.GetPet(1))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<PetResponseModel>();
        }

        [TestMethod]
        public void GetPetShouldReturnNotFoundWhenIdIsNonexistent()
        {
            this.petsController
                .Calling(p => p.GetPet(-1))
                .ShouldReturn()
                .NotFound();
        }

        [TestMethod]
        public void PostShouldHaveAuthorizeAttribute()
        {
            this.petsController
                .Calling(p => p.Post(null))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests());
        }

        [TestMethod]
        public void PostShouldReturnBadRequestWhenModelIsNull()
        {
            this.petsController
                .Calling(p => p.Post(null))
                .ShouldReturn()
                .BadRequest();
        }

        [TestMethod]
        public void PostShouldReturnBadRequestWhenModelIsInvalid()
        {
            this.petsController
                .Calling(p => p.Post(MocksFactory.GetInvalidPetRequestModel()))
                .ShouldReturn()
                .BadRequest();
        }

        [TestMethod]
        public void PostShouldReturnCreatedWithDataWhenModelIsValid()
        {
            this.petsController
                .Calling(p => p.Post(MocksFactory.GetValidPetRequestModel()))
                .ShouldReturn()
                .Created()
                .WithResponseModelOfType<int>()
                .Passing(x => x == 1);
        }
    }
}
