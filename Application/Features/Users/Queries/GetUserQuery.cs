using System;
using System.Linq;

namespace Application.Features.Users.Queries;

public class GetUserQuery : IRequest<UserResponse>
{
    public string UserId { get; set; }
}
