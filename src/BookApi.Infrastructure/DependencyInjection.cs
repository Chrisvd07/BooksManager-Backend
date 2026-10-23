using BookApi.Domain.Interfaces;
using BookApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient();
            
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            return services;
        }
    }
}
