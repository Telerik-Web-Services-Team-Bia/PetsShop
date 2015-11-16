namespace PetStore.Tests.Controllers
{
    using System.Linq;
    using System.Web.Http.Cors;

    using Api.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using MyTested.WebApi;
    using Api.Models;
    using Models;

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

            AutoMapper.Mapper.CreateMap<Category, CategoryResponseModel>();
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
                .WithResponseModelOfType<IQueryable<CategoryResponseModel>>()
                .Passing(c => c.Count() == 2);
        }
    }
}
