using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Domain;
using Moq;
using Xunit;

namespace TestProject.Users
{
    public class UserTest
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
    }
}
