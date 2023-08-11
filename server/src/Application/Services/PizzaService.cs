using Application.Interfaces;
using Application.Mappings;
using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.Services;

public class PizzaService : IPizzaService
{
   private readonly PizzaContext _context;
   public PizzaService(PizzaContext context)
   {
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

   public async Task<Pizza> Create(Pizza newPizza)
   {
      await _context.Pizzas.AddAsync(newPizza);
      await _context.SaveChangesAsync();

      return newPizza;
   }

   public void AddTopping(int PizzaId, int ToppingId)
   {
      var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
      var toppingToAdd = _context.Toppings.Find(ToppingId);

      if (pizzaToUpdate is null || toppingToAdd is null)
         throw new InvalidOperationException("Pizza or Topping does not exist");

      if (pizzaToUpdate.Toppings is null)
         pizzaToUpdate.Toppings = new List<Topping>();

      pizzaToUpdate.Toppings.Add(toppingToAdd);
      _context.SaveChanges();
   }

   public void UpdateSauce(int PizzaId, int SauceId)
   {
      var pizzaToUpdate = _context.Pizzas.Find(PizzaId);
      var sauceToUpdate = _context.Sauces.Find(SauceId);

      if (pizzaToUpdate is null || sauceToUpdate is null)
         throw new InvalidOperationException("Pizza or Topping does not exist");

      pizzaToUpdate.Sauce = sauceToUpdate;
      _context.SaveChanges();
   }

   public void DeleteById(int id)
   {
      var pizzaToDelete = _context.Pizzas.Find(id);

      if (pizzaToDelete is not null)
      {
         _context.Pizzas.Remove(pizzaToDelete);
         _context.SaveChanges();
      }
   }
}