namespace PetStore.Tests.Controllers
{
    using System.Linq;
    using System.Web.Http.Cors;

    using Api.Controllers;
    using Api.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;
    using MyTested.WebApi;

    [TestClass]
    public class CategoriesControllerTests
    {
        private IControllerBuilder<CategoriesController> categoriesController;

        [TestInitialize]
        public void Initialize()
        {
            this.categoriesController = MyWebApi
                .Controller<CategoriesController>()
                .WithResolvedDependencyFor(MocksFactory.GetCategoriesService());

            AutoMapper.Mapper.CreateMap<Category, CategoryDataTransferModel>();
        }

        [TestMethod]
        public void GetAllShouldHaveCorsEnabled()
        {
            this.categoriesController
                .Calling(c => c.GetAllCategories())
                .ShouldHave()
                .ActionAttributes(attr => attr.ContainingAttributeOfType<EnableCorsAttribute>());
        }

        [TestMethod]
        public void GetCategoryShouldHaveCorsEnabled()
        {
            this.categoriesController
                .Calling(c => c.GetCategory("some name"))
                .ShouldHave()
                .ActionAttributes(attr => attr.ContainingAttributeOfType<EnableCorsAttribute>());
        }

        [TestMethod]
        public void GetAllShouldReturnOkWithData()
        {
            this.categoriesController
                .Calling(c => c.GetAllCategories())
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<IQueryable<CategoryDataTransferModel>>()
                .Passing(c => c.Count() == 2);
        }

        [TestMethod]
        public void GetCategoryShouldReturnOkWithDataWhenNameExists()
        {
            this.categoriesController
                .Calling(c => c.GetCategory("Valid"))
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<IQueryable<CategoryDataTransferModel>>()
                .Passing(c => c.Count() != 0);
        }

        [TestMethod]
        public void GetCategoryShouldReturnNotFoundWhenNameIsNonexistent()
        {
            this.categoriesController
                .Calling(c => c.GetCategory("Invalid"))
                .ShouldReturn()
                .NotFound();
        }

        [TestMethod]
        public void GetCategoryShouldReturnBadRequestWhenNameIsNull()
        {
            this.categoriesController
                .Calling(c => c.GetCategory(null))
                .ShouldReturn()
                .BadRequest();
        }

        [TestMethod]
        public void GetCategoryShouldReturnBadRequestWhenNameIsEmpty()
        {
            this.categoriesController
                .Calling(c => c.GetCategory(string.Empty))
                .ShouldReturn()
                .BadRequest();
        }

        [TestMethod]
        public void PostShouldReturnBadRequestWhenModelIsNull()
        {
            this.categoriesController
                .Calling(c => c.Post(null))
                .ShouldReturn()
                .BadRequest();
        }

        [TestMethod]
        public void PostShouldReturnBadRequestWithInvalidModelWhenModelIsInvalid()
        {
            this.categoriesController
                .Calling(c => c.Post(MocksFactory.GetInvalidCategoryModel()))
                .ShouldReturn()
                .BadRequest()
                .WithModelStateFor<CategoryDataTransferModel>()
                .ContainingModelStateErrorFor(x => x.Name);
        }

        [TestMethod]
        public void PostShouldReturnCreatedWithDataWhenModelIsValid()
        {
            this.categoriesController
                .Calling(c => c.Post(MocksFactory.GetValidCategoryModel()))
                .ShouldReturn()
                .Created()
                .WithResponseModelOfType<int>()
                .Passing(x => x == 1);
        }
    }
}
