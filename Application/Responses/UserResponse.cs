using System;
using System.Linq;

namespace Application.Responses
{
    public class UserResponse : BaseResponse
    {
        public BankUser? BankUser { get; set; }

        public UserResponse(BankUser? bankUser, bool success = true) : base(success)
        {
            BankUser = bankUser;
        }
        public UserResponse(BankUser? bankUser, bool success = true, string message = "") : base(success)
        {
            BankUser = bankUser;
            Message = message;
        }
    }
}
