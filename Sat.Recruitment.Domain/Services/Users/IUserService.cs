using Sat.Recruitment.DataAccess.Models;
using Sat.Recruitment.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sat.Recruitment.Domain.Services
{
    public interface IUserService
    {
        void AddOrUpdate(UserResponseModel entry);
        Task<IEnumerable<UserResponseModel>> GetDBUsersAsync();
        Task<IEnumerable<UserResponseModel>> GetFileUsersAsync();
        Task<UserResponseModel> GetById(int id);
        void Remove(int id);
        IEnumerable<UserResponseModel> Where(Expression<Func<User, bool>> exp);
        string NormalizeEmail(string email);

        bool IsUserDuplicated(User newUser, List<User> userList);

        public Result ValidateNullOrEmpty(User user);
        

    }
}