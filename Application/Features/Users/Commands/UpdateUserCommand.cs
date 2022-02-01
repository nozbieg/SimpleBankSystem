using System;
using System.Linq;

namespace Application.Features.Users.Commands;

public class UpdateUserCommand : IRequest<BaseResponse>
{
    public string UserId { get; set; }
    public string Pin { get; set; }
}
