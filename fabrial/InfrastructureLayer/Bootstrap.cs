using fabrial.Config;

namespace fabrial.InfrastructureLayer
{
    public static class Bootstrap
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SqlServerConfig>(configuration.GetSection("SqlServerConfig"));
            services.AddSingleton<SqlConnectionManager>();
        }
    }
}
