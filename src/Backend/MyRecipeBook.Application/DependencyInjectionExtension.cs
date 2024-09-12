using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        { 
            AddUseCases(services);
            AddAutoMapper(services);
            AddPasswordEncripter(services);
        }

        public static void AddUseCases(IServiceCollection services) 
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        private static void AddAutoMapper(IServiceCollection services) 
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        public static void AddPasswordEncripter(IServiceCollection services)
        {
            services.AddScoped(option => new PasswordEncripter());
        }
    }
}
