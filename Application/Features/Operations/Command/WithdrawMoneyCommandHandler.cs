using System;
using System.Linq;

namespace Application.Features.Operations.Command
{
    public class WithdrawMoneyCommandHandler : IRequestHandler<WithdrawMoneyCommand, BaseResponse>
    {
        readonly IAsyncRepository<BankUser> repository;
        public WithdrawMoneyCommandHandler(IAsyncRepository<BankUser> repository)
        {
            this.repository = repository;
        }
        public async Task<BaseResponse> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(request.UserId);
            if (user == null)
            {
                return new BaseResponse(false, "Not found");
            }
            if (user.AccountBalance - request.Amount < 0)
            {
                return new BaseResponse(false, "Not enaught money to withdraw.");
            }

            user.AccountBalance -= request.Amount;

            await repository.UpdateAsync(user);

            return new BaseResponse(true, $"Money withdrawed. Actual balance: {user.AccountBalance}");
        }
    }
}
