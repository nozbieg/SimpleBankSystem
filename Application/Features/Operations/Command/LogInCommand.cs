using System;
using System.Linq;

namespace Application.Features.Operations.Command
{
    public class LogInCommand : IRequest<UserResponse>
    {
        public string UserId { get; set; }
        public string Pin { get; set; }
    }
}
