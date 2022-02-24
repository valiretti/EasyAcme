using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAcme.DataAccess
{
    public static class DataAccessExtensions
    {
        public static void AddDbContext(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ApplicationContext>(options => options.UseSqlite(GetSqLiteConnectionString()));
        }

        public static void AddDefaultIdentity(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDefaultIdentity<IdentityUser>(options => 
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<ApplicationContext>();
        }

        private static string GetSqLiteConnectionString()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Path.Join(Environment.GetFolderPath(folder), "EasyAcme");
            Directory.CreateDirectory(path);
            path = Path.Join(path, "EasyAcme.db");
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = path
            };

            return connectionStringBuilder.ToString();
        }
    }
}