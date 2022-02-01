// See https://aka.ms/new-console-template for more information
using App;
using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

var serviceProvider = new ServiceCollection()
    .AddApplicationServices()
    .AddInfrastructureServices()
    .AddPersistanceServices()
    .AddSingleton<IBankSystem, BankSystem>()
    .BuildServiceProvider();

var db = serviceProvider.GetRequiredService<BankDbContext>();
db.Database.EnsureCreated();

var bankSystem = serviceProvider.GetRequiredService<IBankSystem>();

bankSystem.PrintWelocmeScreen();
bankSystem.PrintBaseMenu();
bankSystem.TakeBaseInput();




