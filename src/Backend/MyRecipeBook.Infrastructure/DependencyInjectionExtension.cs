using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            AddRepositories(services);
            AddDbContext(services);
        }

        private static void AddDbContext(IServiceCollection services)
        {
            var connectionString = "Data Source=DESKTOP;Initial Catalog=LIVRO_DE_RECEITAS;User ID=sa;Password=!Q2w3e4r;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
