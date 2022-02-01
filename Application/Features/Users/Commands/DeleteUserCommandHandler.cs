using System;
using System.Linq;

namespace Application.Features.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse>
{
    readonly IAsyncRepository<BankUser> repository;
    public DeleteUserCommandHandler(IAsyncRepository<BankUser> repository)
    {
        this.repository = repository;
    }

    public async Task<BaseResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetAsync(request.UserId);
        if (entity == null)
        {
            return new BaseResponse(false, "Not found");
        }

        await repository.DeleteAsync(entity);
        return new BaseResponse(true, "Deleted");
    }
}
