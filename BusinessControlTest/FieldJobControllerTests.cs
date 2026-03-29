using BusinessControlService;
using BusinessControlService.Controllers;
using BusinessControlService.Models;
using BusinessControlService.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
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
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
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

            var newFieldJob = new FieldJobDTO
            {
                FieldArea = 1.1f,
                CustomerId = context.Customers.First().Id,
                CustomerFirstName = context.Customers.First().FirstName,
                CustomerLastName= context.Customers.First().LastName,
                MachineId = context.Machines.First().Id,
                MachineName = context.Machines.First().MachineName,
                WorkerId= context.Workers.First().Id,
                WorkerFirstName = context.Workers.First().FirstName,
                WorkerLastName = context.Workers.First().LastName,
            };

            var result = await controller.CreateFieldJob(newFieldJob);

            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Single(context.FieldJobs);
        }

        [Fact]
        public async Task SetFieldJob_Updates_WhenExists()
        {
            var context = GetDbContext();

            await SeedData(context);

            Assert.Null(await context.FieldJobs.SingleOrDefaultAsync(z=>z.Id == 1));

            var newFieldJob = new FieldJobDTO
            {
                FieldArea = 1.1f,
                CustomerId = context.Customers.First().Id,
                CustomerFirstName = context.Customers.First().FirstName,
                CustomerLastName = context.Customers.First().LastName,
                MachineId = context.Machines.First().Id,
                MachineName = context.Machines.First().MachineName,
                WorkerId = context.Workers.First().Id,
                WorkerFirstName = context.Workers.First().FirstName,
                WorkerLastName = context.Workers.First().LastName,
            };

            var controller = new FieldJobController(context);
            var result = await controller.CreateFieldJob(newFieldJob);

            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Single(context.FieldJobs);

            var resultGetFieldJob = await controller.GetFieldJob(1);

            var fieldJobToUpdate = resultGetFieldJob.Value ?? (resultGetFieldJob.Result as OkObjectResult)?.Value as FieldJobDTO;

            Assert.NotNull(fieldJobToUpdate);

            Assert.Equal(1, fieldJobToUpdate.Id);

            fieldJobToUpdate.FieldArea = 2.2f;

            result = await controller.UpdateFieldJob(fieldJobToUpdate);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Single(context.FieldJobs);


            var resultGetFieldJobUpdated = await controller.GetFieldJob(1);

            var fieldJobUpdated = resultGetFieldJobUpdated.Value ?? (resultGetFieldJobUpdated.Result as OkObjectResult)?.Value as FieldJobDTO;

            Assert.NotNull(fieldJobUpdated);

            Assert.Equal(2.2f, fieldJobUpdated.FieldArea);
        }
    }
}