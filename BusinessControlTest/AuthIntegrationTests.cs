using BusinessControlService;
using BusinessControlService.Models;
using BusinessControlService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessControlTest
{
    public class AuthIntegrationTests
    {
        [Fact]
        public async Task LoginFlow_CreatesJwtAndRefreshToken()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            await using var context = new AppDbContext(options);

            var userStore = new UserStore<AppUser>(context);
            var userManager = new UserManager<AppUser>(
                userStore, null, new PasswordHasher<AppUser>(),
                null, null, null, null, null, null
            );

            var newUser = new AppUser { UserName = "testUser" };
            await userManager.CreateAsync(newUser, "Test@123");

            var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Key", "LovJ20jljjuE9t2wlPS7Os1eXb9BpcrXmv7O8SnFFes=,"}
        };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var jwtService = new JwtService(configuration);

            var user = await userManager.FindByNameAsync("testUser");
            Assert.NotNull(user);

            var appUser = user as AppUser;
            Assert.NotNull(appUser);

            var jwt = jwtService.GenerateJwt(appUser.Id, appUser.UserName);
            var refreshToken = jwtService.GenerateRefreshToken();

            appUser.RefreshToken = refreshToken;
            appUser.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await userManager.UpdateAsync(appUser);


            Assert.NotNull(jwt);
            Assert.NotNull(appUser.RefreshToken);
            Assert.True(appUser.RefreshTokenExpiry > DateTime.UtcNow);

            var fromDb = await userManager.FindByNameAsync("testUser");
            Assert.Equal(refreshToken, fromDb.RefreshToken);

        }
    }
}
