public static class BusinessServiceRegistration
{

    public static void AddBusinessInfrastructure(
         this IServiceCollection services)
    {
        services.AddScoped<IBillService, BillService>();
    }
}