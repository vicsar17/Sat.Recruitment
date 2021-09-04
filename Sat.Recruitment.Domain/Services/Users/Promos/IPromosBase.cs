using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Services.Users.Promos
{
    public interface IPromosBase
    {
        decimal GetPercentage(decimal moneyToCalculate);
    }
}
