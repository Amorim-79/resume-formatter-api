using Microsoft.Extensions.DependencyInjection;
using ResumeFormatter.Domain.Interfaces.Repository;
using ResumeFormatter.Domain.Interfaces.Service;
using ResumeFormatter.Infra.Data.Repository;
using ResumeFormatter.Service.Services;

namespace ResumeFormatter.Infra.CrossCutting.IOC
{
    public class DependenciesContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IResumeService, ResumeService>();
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}