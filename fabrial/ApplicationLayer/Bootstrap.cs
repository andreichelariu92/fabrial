namespace fabrial.ApplicationLayer
{
    public static class Bootstrap
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<CreateSqlCommandUsecase>();
        }
    }
}
