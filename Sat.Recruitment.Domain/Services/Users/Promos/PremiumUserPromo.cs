namespace Sat.Recruitment.Domain.Services.Users.Promos
{
    public class PremiumUserPromo : IPromosBase
    {
        public decimal GetPercentage(decimal userMoney)
        => userMoney > 100 ? 2M : 0.0M;
    }
}