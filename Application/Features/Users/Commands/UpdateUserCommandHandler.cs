using System;
using System.Linq;

namespace Application.Features.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse>
{
    readonly IAsyncRepository<BankUser> repository;
    public UpdateUserCommandHandler(IAsyncRepository<BankUser> repository)
    {
        this.repository = repository;
    }
    public async Task<BaseResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetAsync(request.UserId);
        if (entity == null)
        {
            return new BaseResponse(false, "Not found");
        }

        await repository.UpdateAsync(entity);
        return new BaseResponse(true, "Updated");
    }
}
