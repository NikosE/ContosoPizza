using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services)
   {
      // Application services
      services.AddScoped<IPizzaService, PizzaService>();        

      return services;
   }
}