using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Domain.Dto;
using Domain.Dto.Common;
using Domain.Models;
using Persistence.Context;
using Persistence.Interfaces;
using Application.Mappings;
using Application.Interfaces;

namespace Application.Services;

public class PizzaService : IPizzaService
{
   private readonly PizzaContext _context;
   private readonly ICommonRepository _commonRepository;
   public PizzaService(PizzaContext context, ICommonRepository commonRepository)
   {
      _commonRepository = commonRepository;
      _context = context;
      
   }

   public async Task<IEnumerable<Pizza>> GetAll()
   {
      return await _context.Pizzas.ToListAsync();
   }

   public async Task<PizzaDto> GetById(int id)
   {       
      var pizza = await _context.Pizzas
                     .Include(p => p.Toppings)
                     .Include(p => p.Sauce)
                     .AsNoTracking()
                     .FirstOrDefaultAsync(p => p.Id == id);

      if (pizza is null) throw new Exception("Pizza does not exist");

      var dto = pizza.PizzaDtoMapping();

      return dto;
   }

   public async Task<CommandResponse<string>> CreatePizza(CreatePizzaDto dto, CancellationToken token)
   {
      // Searching Pizza
      var filtersPizzaQuery = new Expression<Func<Pizza, bool>>[] {x => x.Id == dto.Id || x.Name == dto.Name};
      var pizzaExist = await _commonRepository.GetResultByIdAsync(_context.Pizzas, filtersPizzaQuery, null, token);

      // Checking for Exceptions
      if (pizzaExist is not null) throw new InvalidOperationException($"Pizza Name {dto.Name} or Id already exist!");

      // Searching Sauce
      var filtersQuery = new Expression<Func<Sauce, bool>>[] {x => x.Id == dto.Sauce.Id};
      var sauce = await _commonRepository.GetResultByIdAsync(_context.Sauces, filtersQuery, null, token);

      // Checking for Exceptions
      if (sauce is null) throw new Exception("sauce does not exist!");

      // Mapping and Saving
      var pizza = dto.CreatePizzaModelMapping(sauce);
      await _context.Pizzas.AddAsync(pizza);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Initializing object
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The Pizza {pizza.Name} created successfully!");
   }

   public async Task<CommandResponse<string>>AddTopping(PizzaDto dto, int ToppingId, CancellationToken token)
   {
      // Searching Pizza
      var filtersPizzaQuery = new Expression<Func<Pizza, bool>>[] {x => x.Id == dto.Id};
      var pizza = await _commonRepository.GetResultByIdAsync(_context.Pizzas, filtersPizzaQuery, null, token);
      
      // Checking for Exceptions
      if(pizza is null) throw new Exception("Pizza does not exist!");
      
      // Searching Sauce
      var filtersToppingQuery = new Expression<Func<Topping, bool>>[] {x => x.Id == ToppingId};
      var topping = await _commonRepository.GetResultByIdAsync(_context.Toppings, filtersToppingQuery, null, token);
      
      // Checking for Exceptions
      if(topping is null) throw new Exception("Topping does not exist!");

      
      if (pizza.Toppings is null)
      pizza.Toppings = new List<Topping>();

      pizza.Toppings.Add(topping);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Initializing object
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The topping{topping.Name} added in {pizza.Name} successfully!");
   }

   public async Task<CommandResponse<string>> UpdateSauce(int id, PizzaSauceDto dto, CancellationToken token)
   {
      // Searching Pizza
      var filtersPizzaQuery = new Expression<Func<Pizza, bool>>[] {x => x.Id == id};
      var pizzaToUpdated = await _commonRepository.GetResultByIdAsync(_context.Pizzas, filtersPizzaQuery, null, token);
      
      // Checking for Exceptions
      if(pizzaToUpdated is null) throw new Exception("Pizza does not exist!");

      // Searching Sauce
      var filtersSauceQuery = new Expression<Func<Sauce, bool>>[] {x => x.Id == dto.Sauce.Id};
      var sauceToUpdated = await _commonRepository.GetResultByIdAsync(_context.Sauces, filtersSauceQuery, null, token);
      
      // Checking for Exceptions
      if(sauceToUpdated is null) throw new Exception("Sauce does not exist!");

      // Mapping and Saving
      dto.UpdatePizzaModelMapping(pizzaToUpdated, sauceToUpdated);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Initializing object
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The pizza {pizzaToUpdated.Name} updated with sauce {sauceToUpdated.Name} successfully!");
   }

   public async Task<CommandResponse<string>> DeletePizza(int pizzaId, CancellationToken token)
   {
      // Searching Pizza
      var filteredQuery = new Expression<Func<Pizza, bool>>[] {x => x.Id == pizzaId};
      var pizzaToDelete = await _commonRepository.GetResultByIdAsync(_context.Pizzas, filteredQuery, null, token);
      
      // Checking for Exceptions
      if (pizzaToDelete is null) throw new Exception("Pizza does not exist!");
      
      // Deleting
      _context.Pizzas.Remove(pizzaToDelete);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Initializing object
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"Pizza with name {pizzaToDelete.Name} deleted successfully!");
   }
}