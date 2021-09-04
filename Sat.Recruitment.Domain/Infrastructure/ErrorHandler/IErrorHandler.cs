namespace Sat.Recruitment.Domain.Infrastructure.ErrorHandler
{
    public interface IErrorHandler
    {
        string GetMessage(ErrorMessagesEnum message);
    }


    public enum ErrorMessagesEnum
    {
        EntityNull = 1,
        ModelValidation = 2,
        UserNameNull = 3,
        UserEmailNull = 4,
        UserAddressNull = 5,
        UserPhoneNull = 6,
        UserMoneyNull = 7,
        UserTypeNull = 8
    }

}