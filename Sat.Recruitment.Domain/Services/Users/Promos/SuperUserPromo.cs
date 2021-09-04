namespace Sat.Recruitment.Domain.Services.Users.Promos
{
    public class SuperUserPromo : IPromosBase
    {
        public decimal GetPercentage(decimal userMoney)
        => userMoney > 100 ? 0.20M : 0.0M;

    }
}