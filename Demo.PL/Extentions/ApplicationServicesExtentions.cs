using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.PL.Mappers;

namespace Demo.PL.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(M => M.AddProfile<EmployeeProfile>());
            services.AddAutoMapper(M => M.AddProfile<DepartmentProfile>());
            services.AddAutoMapper(M => M.AddProfile<AccountProfile>());
            services.AddAutoMapper(M => M.AddProfile<CreateUserProfile>());

            return services;
        }
    }
}
