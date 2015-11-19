namespace PetStore.Tests.Mocks
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Api;
    using Microsoft.AspNet.Identity;
    using Models;
    using Moq;

    public class ApplicationUserManagerMock
    {
        public static ApplicationUserManager Create()
        {
            // create our mocked user
            var user = new User { UserName = "TestAuthor", Email = "TestAuthor@test.com" };

            // mock the application user manager with mocked user store
            var mockedUserStore = new Mock<IUserStore<User>>();
            var applicationUserManager = new Mock<ApplicationUserManager>(mockedUserStore.Object);

            // mock the application user manager to always return our user object with any username and password
            applicationUserManager.Setup(x => x.FindAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            // mock the application user manager create identity in order to generate valid access token when requested
            applicationUserManager.Setup(x => x.CreateIdentityAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns<User, string>(
                    (u, password) =>
                        Task.FromResult(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, u.UserName) },
                            DefaultAuthenticationTypes.ApplicationCookie)));

            return applicationUserManager.Object;
        }
    }
}
