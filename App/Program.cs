// See https://aka.ms/new-console-template for more information
using App;
using Application;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

var serviceProvider = new ServiceCollection()
    .AddApplicationServices()
    .AddInfrastructureServices()
    .AddPersistanceServices()
    .BuildServiceProvider();

var db = serviceProvider.GetRequiredService<BankDbContext>();
db.Database.EnsureCreated();

var mediator = serviceProvider.GetRequiredService<IMediator>();

var bankSystem = new BankSystem(mediator);
bankSystem.PrintWelocmeScreen();
bankSystem.PrintBaseMenu();
bankSystem.TakeBaseInput();




