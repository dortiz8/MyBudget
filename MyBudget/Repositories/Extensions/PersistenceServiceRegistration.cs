public static class PersistenceServiceRegistration
{

    public static void AddPersistenceInfrastructure(
         this IServiceCollection services)
    {
        services.AddScoped<IMyBudgetDataAccess, MyBudgetDataAccess>();
    }
}