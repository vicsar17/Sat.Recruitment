namespace Sat.Recruitment.Domain.Services.Users.Promos
{
    public class NormalUserPromo : IPromosBase
    {
        public decimal GetPercentage(decimal userMoney)
        => userMoney > 100 ? 0.12M :
                (userMoney < 100 & userMoney > 10) ? 0.8M : 0.0M;

    }
}