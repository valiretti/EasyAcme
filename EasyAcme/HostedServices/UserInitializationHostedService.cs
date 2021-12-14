using Microsoft.AspNetCore.Identity;

namespace EasyAcme.HostedServices
{
    public class UserInitializationHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public UserInitializationHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;           
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await userManager.FindByNameAsync("admin");
            if (user == null)
            {
                user = new IdentityUser { UserName = "admin" };
                var result = await userManager.CreateAsync(user, "Admin123");
            }            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
