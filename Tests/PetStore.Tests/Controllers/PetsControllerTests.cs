namespace PetStore.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Api.Controllers;
    using Api.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;
    using MyTested.WebApi;

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
                .ForMember(
                    dest => dest.Color,
                           opts => opts.MapFrom(src => src.Color.Name))
                .ForMember(
                    dest => dest.Species,
                           opts => opts.MapFrom(src => src.Species.Name))
                .ForMember(
                    dest => dest.Category,
                           opts => opts.MapFrom(src => src.Species.Category.Name))
                .ForMember(
                    dest => dest.Rating,
                           opts => opts.MapFrom(src => (src.Ratings.Count == 0) ? 0 : (double)src.Ratings.Sum(r => r.Value) / src.Ratings.Count))
                .ForMember(
                    dest => dest.Image,
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

        [TestMethod]
        public void PutShouldHaveAuthorizeAttribute()
        {
            this.petsController
                .Calling(p => p.Put(null))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests());
        }

        [TestMethod]
        public void PutShouldReturnBadRequestWhenModelIsNull()
        {
            this.petsController
                .Calling(p => p.Put(null))
                .ShouldReturn()
                .BadRequest();
        }

        [TestMethod]
        public void PutShouldReturnBadRequestWhenModelIsInvalid()
        {
            this.petsController
                .Calling(p => p.Put(MocksFactory.GetInvalidPetRequestModel()))
                .ShouldReturn()
                .BadRequest();
        }

        [TestMethod]
        public void PutShouldReturnNotFoundWhenPetIdIsNonexistent()
        {
            var petRequest = MocksFactory.GetValidPetRequestModel();
            petRequest.Id = -1;

            this.petsController
                .Calling(p => p.Put(petRequest))
                .ShouldReturn()
                .NotFound();
        }

        [TestMethod]
        public void PutShouldReturnUnauthorizedWhenRequestedPetHasAnotherOwner()
        {
            var petRequest = MocksFactory.GetValidPetRequestModel();
            petRequest.Id = 1;

            this.petsController
                .WithAuthenticatedUser()
                .Calling(p => p.Put(petRequest))
                .ShouldReturn()
                .Unauthorized();
        }

        [TestMethod]
        public void PutShouldReturnCreatedWhenModelIsValidAndUserIsPetOwner()
        {
            var petRequest = MocksFactory.GetValidPetRequestModel();
            petRequest.Id = 5;

            this.petsController
                .WithAuthenticatedUser()
                .Calling(p => p.Put(petRequest))
                .ShouldReturn()
                .Created();
        }

        [TestMethod]
        public void DeleteShouldHaveAuthorizeAttribute()
        {
            this.petsController
                .Calling(p => p.Delete(0))
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests());
        }

        [TestMethod]
        public void DeleteShouldReturnNotFoundWhenPetIdIsNonexistent()
        {
            this.petsController
                .Calling(p => p.Delete(-1))
                .ShouldReturn()
                .NotFound();
        }

        [TestMethod]
        public void DeleteShouldReturnOkWhenPetIdIsExistent()
        {
            this.petsController
                .Calling(p => p.Delete(1))
                .ShouldReturn()
                .Ok();
        }
    }
}
