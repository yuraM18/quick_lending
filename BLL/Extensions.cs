using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class Extensions
    {
        public static void AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayer(configuration);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IStatementTypeService, StatementTypeService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
        }
    }
}
