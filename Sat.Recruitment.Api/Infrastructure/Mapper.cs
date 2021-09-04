using AutoMapper;
using Sat.Recruitment.DataAccess.Models;
using Sat.Recruitment.Domain.Models;

namespace Sat.Recruitment.Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserResponseModel>();
            CreateMap<UserResponseModel, User>();

        }
    }
}
