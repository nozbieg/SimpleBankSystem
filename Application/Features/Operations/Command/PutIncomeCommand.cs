using System;
using System.Linq;

namespace Application.Features.Operations.Command
{
    public class PutIncomeCommand : IRequest<BaseResponse>
    {
        public string UserId { get; set; }
        public double Income { get; set; }
    }
}
