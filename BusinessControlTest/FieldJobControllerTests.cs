using BusinessControlService;
using BusinessControlService.Controllers;
using BusinessControlService.Models;
using BusinessControlService.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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

        private async Task SetWorkers(AppDbContext context)
        {
            context.Workers.AddRange(
                   new Worker { FirstName = "John", LastName = "Brown", IsHired = true, HiringDateStart = new DateTime(2015, 1, 1), HiringDateEnd = new DateTime(2017, 2, 2) },
                   new Worker { FirstName = "Chris", LastName = "Grease", IsHired = true, HiringDateStart = new DateTime(2015, 1, 1), HiringDateEnd = new DateTime(2017, 2, 2) }
               );
            await context.SaveChangesAsync();
        }

        private async Task SetCustomers(AppDbContext context)
        {
            context.Customers.AddRange(
                new Customer { FirstName = "Cus", LastName = "Tomer", Address = "Road 1/2", City = "City1" },
                new Customer { FirstName = "Leom", LastName = "Essi", Address = "Roadski", City = "London" }
            );

            await context.SaveChangesAsync();
        }

        private async Task SetMachines(AppDbContext context)
        {
            context.Machines.AddRange(
                new Machine { AcquisitionDate = new DateTime(2021, 12, 12, 8, 1, 1), MachineName = "Tractor1", MachineType = MachineTypeEnum.Tractor }
            );

            await context.SaveChangesAsync();
        }

        //todo seed data using api calls
        private async Task SeedData(AppDbContext context)
        {
            await SetWorkers(context);
            await SetCustomers(context);
            await SetMachines(context);
            Assert.NotEmpty(context.Workers);
            Assert.NotEmpty(context.Customers);
            Assert.NotEmpty(context.Machines);
            return;
        }

        //todo have to add customers, machines and workers first

        [Fact]
        public async Task SetFieldJob_AddsNew_WhenIdIsZero()
        {
            var context = GetDbContext();
            await SeedData(context);

            var controller = new FieldJobController(context);

            var newFieldJob = new FieldJob
            {
                Id = 0,
                FieldArea = 1.1f,
                Machine = context.Machines.First(),
                Worker = context.Workers.First(),
                Customer = context.Customers.First()
            };

            var result = await controller.SetFieldJob(newFieldJob);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Single(context.FieldJobs);
        }

        [Fact]
        public async Task SetFieldJob_Updates_WhenExists()
        {
            var context = GetDbContext();

            await SeedData(context);

            Assert.Null(await context.FieldJobs.SingleOrDefaultAsync(z=>z.Id == 1));

            var newFieldJob = new FieldJob
            {
                Id = 1,
                FieldArea = 1.1f,
                Machine = context.Machines.First(),
                Worker = context.Workers.First(),
                Customer = context.Customers.First()
            };

            var controller = new FieldJobController(context);
            var result = await controller.SetFieldJob(newFieldJob);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Single(context.FieldJobs);
            var fieldJobToUpdate = await context.FieldJobs.FindAsync(1);
            Assert.NotNull(fieldJobToUpdate);

            fieldJobToUpdate.FieldArea = 2.2f;

            result = await controller.SetFieldJob(fieldJobToUpdate);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Single(context.FieldJobs);

            var dbEntity = await context.FieldJobs.FindAsync(1);
            Assert.NotNull(dbEntity);
            Assert.Equal(2.2f, dbEntity.FieldArea);
        }

        [Fact]
        public async Task SetFieldJob_ReturnsBadRequest_WhenNull()
        {
            var context = GetDbContext();
            await SeedData(context);
            var controller = new FieldJobController(context);

            var result = await controller.SetFieldJob(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}