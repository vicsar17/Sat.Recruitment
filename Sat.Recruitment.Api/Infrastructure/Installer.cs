using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Contexts;
using Sat.Recruitment.DataAccess.Models;
using Sat.Recruitment.Domain.Infrastructure.ErrorHandler;
using Sat.Recruitment.Domain.Repositories;
using Sat.Recruitment.Domain.Services;

namespace Sat.Recruitment.Api.Infrastructure
{
    internal static class Installer
    {

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>();

            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
            services.AddTransient<IBaseService<User>, BaseService<User>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IErrorHandler, ErrorHandler>();
        }

    }
}
