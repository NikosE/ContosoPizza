using Persistence.Context;
using Persistence.Data;

public static class Extensions
{
   public static void CreateDbIfNotExists(this IHost host)
   {
      // This method is an extension method for IHost, so it's called on an IHost instance.
      
      // Creating a new scope for dependency injection.
      using (var scope = host.Services.CreateScope())
      {
         // Obtaining the service provider from the scope.
         var services = scope.ServiceProvider;
         // Getting the required instance of the PizzaContext from the service provider.
         var context = services.GetRequiredService<PizzaContext>();
         // Ensuring that the database is created if it doesn't already exist.
         context.Database.EnsureCreated();
         // Calling a method (DbInitializer.Initialize) to initialize the context with data.
         DbInitializer.Initialize(context);
      }      
   }
}