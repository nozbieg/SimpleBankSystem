using System;
using System.Linq;

namespace Application.Features.Operations.Command
{
    public class WithdrawMoneyCommand : IRequest<BaseResponse>
    {
        public string UserId { get; set; }
        public double Amount { get; set; }
    }
}
