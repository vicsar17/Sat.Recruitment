using System;


namespace Sat.Recruitment.Domain.Infrastructure.ErrorHandler
{
    public class ErrorHandler : IErrorHandler
    {
        public string GetMessage(ErrorMessagesEnum message)
        {
            return message switch
            {
                ErrorMessagesEnum.EntityNull => "The entity passed is null {0}. Additional information: {1}",
                ErrorMessagesEnum.ModelValidation => "The request data is not correct. Additional information: {0}",
                ErrorMessagesEnum.UserNameNull => "The name is required.",
                ErrorMessagesEnum.UserEmailNull => "The email is required.",
                ErrorMessagesEnum.UserAddressNull => "The address is required.",
                ErrorMessagesEnum.UserPhoneNull => "The phone is required.",
                ErrorMessagesEnum.UserMoneyNull => "The money is required.",
                ErrorMessagesEnum.UserTypeNull => "The usertype is required.",
                _ => throw new ArgumentOutOfRangeException(nameof(message), message, null),
            };
        }

    }
}
