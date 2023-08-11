using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using WebApi;

namespace Persistence.Context;

public static class DependencyInjection
{
   public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
   {
      services.AddDbContext<PizzaContext>(opt =>
      {
         opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddDbContext<PromotionsContext>(opt =>
      {
         opt.UseSqlite(configuration.GetConnectionString("PromotionsConnection"));
      });

      return services;
   }
}