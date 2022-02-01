using System;
using System.Linq;

namespace Application.Features.Users.Commands;

public class DeleteUserCommand : IRequest<BaseResponse>
{
    public string UserId { get; set; }
}
