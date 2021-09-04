using Sat.Recruitment.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Services.Users.Promos
{
    public class PromosPerUserTypeAndMoney
    {
        private readonly Dictionary<string, IPromosBase> users;

        public PromosPerUserTypeAndMoney()
        {
            users = new Dictionary<string, IPromosBase>
            {
                { "NORMAL", new NormalUserPromo() },
                { "SUPERUSER", new SuperUserPromo() },
                { "PREMIUM", new PremiumUserPromo() }
            };
        }

        public decimal PromoGiftPerUserType(User user)
        {
            var percentage = users[user.UserType.ToUpper()].GetPercentage(user.Money);
            var gif = user.Money * percentage;
            user.Money += gif;
            return user.Money;
        }

    }
}
