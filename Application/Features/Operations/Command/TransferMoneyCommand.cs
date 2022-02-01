using System;
using System.Linq;

namespace Application.Features.Operations.Command
{
    public class TransferMoneyCommand : IRequest<BaseResponse>
    {
        public string From_UserId { get; set; }
        public string To_UserId { get; set; }
        public double Amount { get; set; }

    }
}
