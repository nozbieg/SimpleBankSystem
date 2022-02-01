using System;
using System.Linq;

namespace Application.Features.Users.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse>
    {

        readonly IAsyncRepository<BankUser> repository;
        public GetUserQueryHandler(IAsyncRepository<BankUser> repository)
        {
            this.repository = repository;
        }

        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(request.UserId);
            if (user == null)
            {
                return new UserResponse(null, false);
            }

            return new UserResponse(user, true);
        }
    }
}
