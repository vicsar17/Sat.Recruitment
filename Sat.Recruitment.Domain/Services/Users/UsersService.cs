using Sat.Recruitment.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Sat.Recruitment.Domain.Models;
using System;
using static Sat.Recruitment.Domain.Services.IUserService;
using Sat.Recruitment.DataAccess.Contexts;
using Sat.Recruitment.Domain.Infrastructure.ErrorHandler;

namespace Sat.Recruitment.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseService<User> _service;
        private readonly IMapper _mapper;
        private readonly IUserDataAccess _userDataAccess;
        private readonly IErrorHandler _errorHandler;

        public UserService(IBaseService<User> service, IMapper mapper, IErrorHandler errorHandler)
        {
            _service = service;
            _mapper = mapper;
            _errorHandler = errorHandler;
            _userDataAccess = new UserDataAccess();
        }

        public async Task<IEnumerable<UserResponseModel>> GetDBUsersAsync()
        {
            var result = await _service.GetAsync();
            return _mapper.Map<IEnumerable<User>, List<UserResponseModel>>(result);
        }

        public async Task<UserResponseModel> GetById(int id)
        {
            return _mapper.Map<User, UserResponseModel>(await _service.GetById(id));
        }

        public IEnumerable<UserResponseModel> Where(Expression<Func<User, bool>> exp)
        {
            var whereResult = _service.Where(exp).ToList();
            return _mapper.Map<List<User>, List<UserResponseModel>>(whereResult).AsEnumerable();
        }

        public void AddOrUpdate(UserResponseModel entry)
        {
            _service.AddOrUpdate(_mapper.Map<UserResponseModel, User>(entry));
        }

        public void Remove(int id)
        {
            _service.Remove(id);
        }


        public async Task<IEnumerable<UserResponseModel>> GetFileUsersAsync()
        {
            return _mapper.Map<IEnumerable<User>, List<UserResponseModel>>(await _userDataAccess.GetUsersAsync());
        }

        public string NormalizeEmail(string emailAddress)
        {
            //Normalize email
            var aux = emailAddress.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        public bool IsUserDuplicated(User newUser, List<User> userList)
        {
            if (userList.Any(z => z.Email.ToLower() == newUser.Email.ToLower() | z.Phone == newUser.Phone | (z.Name.ToLower() == newUser.Name.ToLower() & z.Address.ToLower() == newUser.Address.ToLower())))
                return true;

            return false;
        }

        public Result ValidateNullOrEmpty(User user)
        {
            {
                
                if (string.IsNullOrEmpty(user.Name))
                    return new Result
                    {
                        IsSuccess = false,
                        Errors = _errorHandler.GetMessage(ErrorMessagesEnum.UserNameNull)
                    };

                if (string.IsNullOrEmpty(user.Email))
                    return new Result
                    {
                        IsSuccess = false,
                        Errors = _errorHandler.GetMessage(ErrorMessagesEnum.UserEmailNull)
                    };

                if (string.IsNullOrEmpty(user.Phone))
                    return new Result
                    {
                        IsSuccess = false,
                        Errors = _errorHandler.GetMessage(ErrorMessagesEnum.UserPhoneNull)
                    };

                if (string.IsNullOrEmpty(user.Address))
                    return new Result
                    {
                        IsSuccess = false,
                        Errors = _errorHandler.GetMessage(ErrorMessagesEnum.UserAddressNull)
                    };

                if (string.IsNullOrEmpty(user.Money.ToString()))
                    return new Result
                    {
                        IsSuccess = false,
                        Errors = _errorHandler.GetMessage(ErrorMessagesEnum.UserMoneyNull)
                    };

                return new Result { IsSuccess = true };
            }
        }
    }
}
