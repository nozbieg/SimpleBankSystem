
using System;
using System.Linq;

namespace Application.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseResponse>
{
    readonly IGeneratorService generatorService;
    readonly IAsyncRepository<BankUser> repository;
    public CreateUserCommandHandler(IGeneratorService generatorService, IAsyncRepository<BankUser> repository)
    {
        this.generatorService = generatorService;
        this.repository = repository;
    }
    public async Task<BaseResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var generatedAccountNumber = generatorService.GenerateCardNumber();
        var randomPin = new Random().Next(0, 9999).ToString("D4");
        var newUser = new BankUser(generatedAccountNumber, randomPin);
        await repository.CreateAsync(newUser);

        return new BaseResponse(true, $"User created \n number: {newUser.Number} \n Pin: {newUser.Pin}");
    }
}
