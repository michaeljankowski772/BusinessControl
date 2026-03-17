using BusinessControlService;
using BusinessControlService.Controllers;
using BusinessControlService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;

namespace BusinessControlTest
{



    public class FieldJobControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("FieldJobTestDb")
                .Options;

            return new AppDbContext(options);
        }

        //todo have to add customers, machines and workers first

        /*[Fact]
        public async Task SetFieldJob_AddsNew_WhenIdIsZero()
        {
            var context = GetDbContext();
            var controller = new FieldJobController(context);

            var fieldJob = new FieldJob
            {
                Id = 0,
                Machine = new Machine { MachineName },

            };

            var result = await controller.SetFieldJob(fieldJob);

            Assert.IsType<OkObjectResult>(result);
            Assert.Single(context.FieldJobs);
        }

        [Fact]
        public async Task SetFieldJob_Updates_WhenExists()
        {
            var context = GetDbContext();

            var existing = new FieldJob
            {
                Id = 1,
                Name = "Old"
            };

            context.FieldJobs.Add(existing);
            await context.SaveChangesAsync();

            var controller = new FieldJobController(context);

            var updated = new FieldJob
            {
                Id = 1,
                Name = "Updated"
            };

            await controller.SetFieldJob(updated);

            var fromDb = await context.FieldJobs.FindAsync(1);
            Assert.Equal("Updated", fromDb.Name);
        }

        [Fact]
        public async Task SetFieldJob_ReturnsBadRequest_WhenNull()
        {
            var context = GetDbContext();
            var controller = new FieldJobController(context);

            var result = await controller.SetFieldJob(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }*/
    }
}