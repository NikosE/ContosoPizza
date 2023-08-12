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

public class SauceServices : ISauceServices
{
   private readonly PizzaContext _context;
   private readonly ICommonRepository _commonRepository;
   public SauceServices(PizzaContext context, ICommonRepository commonRepository)
   {
      _commonRepository = commonRepository;
      _context = context;
   }
   
   public async Task<IEnumerable<Sauce>> GetAll()
   {
      return await _context.Sauces.ToListAsync();
   }

   public async Task<SauceDto> GetById(int sauceId)
   {
      // Searching Sauce
      var sauce = await _context.Sauces.FirstOrDefaultAsync(x => x.Id == sauceId);

      // Checking for Exception
      if (sauce is null) throw new Exception("Sauce does not exist!");

      // Mapping Dto
      var dto = sauce.SauceDtoMapping();

      return dto;
   }

   public async Task<CommandResponse<string>> CreateSauce(CreateSauceDto dto, CancellationToken token)
   {
      //Searching for Sauce
      var filtersQuery = new Expression<Func<Sauce, bool>>[] {x => x.Id == dto.Id || x.Name == dto.Name};
      var sauce = await _commonRepository.GetResultByIdAsync(_context.Sauces, filtersQuery, null, token);

      // Checking for Exxeptions
      if (sauce is not null) throw new Exception("Sauce Id or Name exist!");

      // Saving new Sauce
      var newSauce = dto.CreateSauceDtoMapping();
      await _context.Sauces.AddAsync(newSauce);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Responses
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The Sauce {newSauce.Name} created successfully!");
      
   }

   public async Task<CommandResponse<string>> UpdateSauce(int sauceId, UpdateSauceDto dto, CancellationToken token)
   {
      //Searching for Sauce
      var filtersQuery = new Expression<Func<Sauce, bool>>[] {x => x.Id == sauceId};
      var sauce = await _commonRepository.GetResultByIdAsync(_context.Sauces, filtersQuery, null, token);

      // Checking for Exxeptions
      if (sauce is null) throw new Exception("Sauce does not exist!");

      // Mapping and Saving
      dto.UpdateSauceDtoMapping(sauce);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Responses
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The Sauce {sauce.Name} updated successfully!");
   }

   public async Task<CommandResponse<string>> DeleteSauce(int sauceId, CancellationToken token)
   {
      //Searching for Sauce
      var filtersQuery = new Expression<Func<Sauce, bool>>[] {x => x.Id == sauceId};
      var sauce = await _commonRepository.GetResultByIdAsync(_context.Sauces, filtersQuery, null, token);

      // Checking for Exxeptions
      if (sauce is null) throw new Exception("Sauce does not exist!");

      // Deleting Sauce
      _context.Sauces.Remove(sauce);
      var result = await _context.SaveChangesAsync(token) > 0;

      // Responses
      return new CommandResponse<string>()
         .WithSuccess(result)
         .WithData($"The Sauce {sauce.Name} deleted successfully!");
   }
}