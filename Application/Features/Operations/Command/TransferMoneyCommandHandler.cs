using System;
using System.Linq;
using Infrastructure.Extensions;

namespace Application.Features.Operations.Command
{
    public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, BaseResponse>
    {
        readonly IAsyncRepository<BankUser> repository;
        public TransferMoneyCommandHandler(IAsyncRepository<BankUser> repository)
        {
            this.repository = repository;
        }

        public async Task<BaseResponse> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
        {
            var userFrom = repository.GetAsync(request.From_UserId);
            var userTo = repository.GetAsync(request.To_UserId);
            await Task.WhenAll(userFrom, userTo);
            if (userFrom == null)
            {
                return new BaseResponse(false, "User from not found");
            }

            if (request.From_UserId == request.To_UserId)
            {
                return new BaseResponse(false, "You cant transfer money to your account.");
            }

            if (userFrom.Result.AccountBalance < request.Amount)
            {
                return new BaseResponse(false, "You dont have enaught money.");
            }
            if (!request.To_UserId.HasValidCheckDigit())
            {
                return new BaseResponse(false, "Luhna checksum invald.");
            }

            userFrom.Result.AccountBalance -= request.Amount;
            userTo.Result.AccountBalance += request.Amount;

            var updateUserFrom = repository.UpdateAsync(userFrom.Result);
            var updateUserTo = repository.UpdateAsync(userTo.Result);

            await Task.WhenAll(updateUserFrom, updateUserTo);

            return new BaseResponse(true, "Money transfer successfull");
        }
    }
}
