using System;
using System.Linq;

namespace Application.Features.Operations.Command
{
    public class LogInCommandHandler : IRequestHandler<LogInCommand, UserResponse>
    {
        readonly IAsyncRepository<BankUser> repository;
        public LogInCommandHandler(IAsyncRepository<BankUser> repository)
        {
            this.repository = repository;
        }
        public async Task<UserResponse> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var user = await repository.GetAsync(request.UserId);
            if (user == null)
            {
                return new UserResponse(null, false, "Not found");
            }

            if (user.Pin != request.Pin)
            {
                return new UserResponse(null, false, "Invald pin");
            }

            return new UserResponse(user, true, "Success");
        }
    }
}
