using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.DataAccess.Models;
using Sat.Recruitment.Domain.Infrastructure.ErrorHandler;
using Sat.Recruitment.Domain.Models;
using Sat.Recruitment.Domain.Services;
using Sat.Recruitment.Domain.Services.Users.Promos;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IErrorHandler _errorHandler;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, IErrorHandler errorHandler, IMapper mapper)
        {
            _service = service;
            _errorHandler = errorHandler;
            _mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<UserResponseModel>> Get()
        {
            //Change to GetDBUsersAsync() if you want to validate the users database
            return await _service.GetFileUsersAsync();
        }

        [HttpPost("createuser")]
        public async Task<Result> Post([FromBody] UserResponseModel entity)
        {
            if (!ModelState.IsValid)
            {
                return new Result
                {
                    IsSuccess = false,
                    Errors = $"{_errorHandler.GetMessage(ErrorMessagesEnum.ModelValidation)} {ModelState.Values.First().Errors.First().ErrorMessage}."
                };

            }
            try
            {
                var resultValidation = _service.ValidateNullOrEmpty(_mapper.Map<UserResponseModel,User>(entity));
                if (!resultValidation.IsSuccess)
                    return resultValidation;

                //Change to GetDBUsersAsync() if you want to validate the users database
                var userList = await _service.GetFileUsersAsync();
                var promo = new PromosPerUserTypeAndMoney();

                entity.Money = promo.PromoGiftPerUserType(_mapper.Map<UserResponseModel, User>(entity));
                entity.Email = _service.NormalizeEmail(entity.Email);

                if (_service.IsUserDuplicated(_mapper.Map<UserResponseModel, User>(entity), _mapper.Map<List<UserResponseModel>, List<User>>((List<UserResponseModel>)userList)))
                {
                    return new Result()
                    {
                        IsSuccess = false,
                        ResultValue = string.Empty,
                        Errors = "The user is duplicated"
                    };
                }

                //Uncomment if you want to save in a database
                //_service.AddOrUpdate(entity);

                Debug.WriteLine("User Created");

                return new Result()
                {
                    IsSuccess = true,
                    ResultValue = entity,
                    Message = "User created."
                };
            }
            catch (Exception ex)
            {

                return new Result
                {
                    IsSuccess = false,
                    ResultValue = string.Empty,
                    Errors = $"{ex.Message} - {ex.StackTrace}."
                };
            }
        }

    }

}
