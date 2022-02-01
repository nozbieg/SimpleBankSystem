using System;
using System.Linq;

namespace Application.Features.Operations.Command
{
    public class PutIncomeCommandHandler : IRequestHandler<PutIncomeCommand, BaseResponse>
    {
        readonly IAsyncRepository<BankUser> repository;
        public PutIncomeCommandHandler(IAsyncRepository<BankUser> repository)
        {
            this.repository = repository;
        }
        public async Task<BaseResponse> Handle(PutIncomeCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(request.UserId);
            if (user == null)
            {
                return new BaseResponse(false, "Not found");
            }

            user.AccountBalance += request.Income;

            await repository.UpdateAsync(user);

            return new BaseResponse(true, $"Balance updated. Actual balance: {user.AccountBalance}");

        }
    }
}
