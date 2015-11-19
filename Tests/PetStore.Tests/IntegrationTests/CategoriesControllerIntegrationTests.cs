namespace PetStore.Tests.IntegrationTests
{
    using System.Net.Http;

    using Api;
    using Api.App_Start;
    using Api.Models;
    using Data.Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using Models;
    using MyTested.WebApi;
    using Newtonsoft.Json.Linq;
    using PetStore.Services.Data.Contracts;

    [TestClass]
    public class CategoriesControllerIntegrationTests
    {
        private IServerBuilder server;
        private string accessToken;

        [ClassCleanup]
        public static void CleanUp()
        {
            MyWebApi.Server().Stops();
        }

        [TestInitialize]
        public void Initialize()
        {
            NinjectConfig.RebindAction = kernel =>
            {
                kernel.Rebind<IRepository<Category>>().ToConstant(MocksFactory.GetCategoriesRepository());
                kernel.Rebind<ICategoriesService>().ToConstant(MocksFactory.GetCategoriesService());
                kernel.Rebind<ApplicationUserManager>().ToConstant(MocksFactory.ApplicationUserManager);
            };

            this.server = MyWebApi.Server().Starts<Startup>();

           //// this.GetAccessToken();

            AutoMapper.Mapper.CreateMap<Category, CategoryDataTransferModel>();
        }

        [TestMethod]
        public void CategoriesControllerShouldReturnResponseMessage()
        {
            this.server
                .WithHttpRequestMessage(r => r
                    .WithRequestUri("/api/Categories")
                    .WithMethod(HttpMethod.Get))
                .ShouldReturnHttpResponseMessage();
        }

        private void GetAccessToken()
        {
            var message = this.server
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("/token")
                    .WithMethod(HttpMethod.Post)
                    .WithFormUrlEncodedContent("username=TestAuthor@test.com&password=testpass&grant_type=password"))
                .ShouldReturnHttpResponseMessage()
                .AndProvideTheHttpResponseMessage();

            var result = JObject.Parse(message.Content.ReadAsStringAsync().Result);
            this.accessToken = (string)result["access_token"];
        }
    }
}
