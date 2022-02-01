using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain;
using Moq;
using Xunit;

namespace TestProject.Users
{
    public class CRUD_Test
    {
        readonly Mock<IAsyncRepository<BankUser>> repositoryMock = new();
        readonly Mock<IGeneratorService> generatorService = new();

        [Fact]
        public async void CreateUser_Test()
        {
            //arange

            var numberId = generatorService.Object.GenerateCardNumber();
            var pin = "0000";
            var user = new BankUser(numberId, pin);

            generatorService.Setup(x => x.GenerateCardNumber()).Returns("4000001684365752");
            repositoryMock.Setup(x => x.CreateAsync(user)).ReturnsAsync(user);

            //act
            var createdUser = await repositoryMock.Object.CreateAsync(user);

            //assert
            Assert.Equal(user, createdUser);
        }

        [Fact]
        public async void DeleteUser_Test()
        {
            var numberId = generatorService.Object.GenerateCardNumber();
            var pin = "0000";
            var user = new BankUser(numberId, pin);
            repositoryMock.Setup(x => x.CreateAsync(user)).ReturnsAsync(user);
            repositoryMock.Setup(x => x.DeleteAsync(user)).Returns(Task.CompletedTask);


            var createdUser = await repositoryMock.Object.CreateAsync(user);
            Assert.Equal(user, createdUser);

            var task = Task.Run(async () =>
            {
                await repositoryMock.Object.DeleteAsync(user);
            });
            await task;
            Assert.True(task.IsCompleted);


        }
        [Fact]
        public async void UpdateUser_Test()
        {
            var numberId = generatorService.Object.GenerateCardNumber();
            var pin = "0000";
            var user = new BankUser(numberId, pin);
            repositoryMock.Setup(x => x.CreateAsync(user)).ReturnsAsync(user);
            user.Pin = "1234";
            repositoryMock.Setup(x => x.UpdateAsync(user)).Returns(Task.CompletedTask);


            var createdUser = await repositoryMock.Object.CreateAsync(user);
            Assert.Equal(user, createdUser);

            var task = Task.Run(async () =>
            {
                await repositoryMock.Object.UpdateAsync(user);
            });
            await task;
            Assert.True(task.IsCompleted);
        }

        [Fact]
        public async void GetUser_Test()
        {
            var numberId = "123";
            var pin = "0000";
            var user = new BankUser(numberId, pin);
            repositoryMock.Setup(x => x.GetAsync("123")).ReturnsAsync(user);

            var getUser = await repositoryMock.Object.GetAsync("123");

            Assert.Equal(user, getUser);
        }
    }
}
