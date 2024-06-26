

using MyAPISecuringAndTrackingDataChange.Data;

namespace MyAPISecuringAndTrackingDataChange.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
         WebApplicationBuilder builder)
        {
            //add db context
            var connection = builder.Configuration.GetConnectionString("DefultConnection");
            services.AddDbContext<ApplicationDBContext>(
                 op => op.UseSqlServer(connection));

            // add services
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
