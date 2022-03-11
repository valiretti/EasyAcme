using Microsoft.Extensions.DependencyInjection;

namespace EasyAcme.Logic;

public static class LogicExtensions
{
    public static void AddLogicServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAcmeAccountService, AcmeAccountService>();
        serviceCollection.AddScoped<IAcmeOrderService, AcmeOrderService>();
    }
}