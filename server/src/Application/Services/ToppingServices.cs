using System.Linq.Expressions;
using Application.Interfaces;
using Application.Mappings;
using Domain.Dto;
using Domain.Dto.Common;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Interfaces;

namespace Application.Services;

public class ToppingServices : IToppingServces
{
   private readonly PizzaContext _context;
   private readonly ICommonRepository _commonRepository;
   public ToppingServices(PizzaContext context, ICommonRepository commonRepository)
   {
      _commonRepository = commonRepository;
      _context = context;
   }

   public async Task<IEnumerable<Topping>> GetAll()
   {
      return await _context.Toppings.ToListAsync();
   }

   public async Task<ToppingDto> GetById(int toppingId)
   {
      // Searching Topping
      var topping = await _context.Toppings.FirstOrDefaultAsync(x => x.Id == toppingId);

      // Checking for Exception
      if (topping is null) throw new Exception("Topping does not exist!");

      // Mapping Dto
      var dto = topping.ToppingDtoMapping();

      return dto;
   }

   public async Task<CommandResponse<string>> CreateTopping(CreateToppingDto dto, CancellationToken token)
   {
       //Searching for Topping
      var filtersQuery = new Expression<Func<Topping, bool>>[] {x => x.Id == dto.Id || x.Name == dto.Name};
      var topping = await _commonRepository.GetResultByIdAsync(_context.Toppings, filtersQuery, null, token);

      // Checking for Exxeptions
      if (topping is not null) throw new Exception("Topping Id or Name exist!");

      // Saving new Topping
      var newTopping = dto.CreateToppingDtoMapping();
      await _context.Toppings.AddAsync(newTopping);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Responses
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The Topping {newTopping.Name} created successfully!");
   }

   public async Task<CommandResponse<string>> UpdateTopping(int toppingId, UpdateToppingDto dto, CancellationToken token)
   {
       //Searching for Topping
      var filtersQuery = new Expression<Func<Topping, bool>>[] {x => x.Id == toppingId};
      var topping = await _commonRepository.GetResultByIdAsync(_context.Toppings, filtersQuery, null, token);

      // Checking for Exxeptions
      if (topping is null) throw new Exception("Topping does not exist!");

      // Mapping and Saving
      dto.UpdateToppingDtoMapping(topping);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Responses
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The Topping {topping.Name} updated successfully!");
   }

   public async Task<CommandResponse<string>> DeleteTopping(int toppingId, CancellationToken token)
   {
      //Searching for Topping
      var filtersQuery = new Expression<Func<Topping, bool>>[] {x => x.Id == toppingId};
      var topping = await _commonRepository.GetResultByIdAsync(_context.Toppings, filtersQuery, null, token);

      // Checking for Exxeptions
      if (topping is null) throw new Exception("Topping does not exist!");

      // Deleting Sauce
      _context.Toppings.Remove(topping);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Responses
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The Topping {topping.Name} deleted successfully!");
   }
}