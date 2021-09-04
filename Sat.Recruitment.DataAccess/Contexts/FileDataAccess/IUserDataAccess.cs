using Sat.Recruitment.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.DataAccess.Contexts
{
    public interface IUserDataAccess
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
