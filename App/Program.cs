// See https://aka.ms/new-console-template for more information
using Application;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceCollection()
    .AddApplicationServices()
    .AddInfrastructureServices()
    .AddPersistanceServices()
    .BuildServiceProvider();
var db = serviceProvider.GetRequiredService<BankDbContext>();
db.Database.EnsureCreated();
var mediator = serviceProvider.GetRequiredService<IMediator>();

var response = await mediator.Send(new Application.Features.Users.Commands.CreateUserCommand());



