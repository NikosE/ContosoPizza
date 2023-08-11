using Application.Interfaces;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
   private readonly IPizzaService _pizzaService;
   
   public PizzaController(IPizzaService pizzaService)
   {
      _pizzaService = pizzaService;
   }

   [HttpGet]
   public async Task<IEnumerable<Pizza>> GetAll()
   {
      return await _pizzaService.GetAll(); //GetAll();
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(int id)
    => Ok(await _pizzaService.GetById(id));


   [HttpPost]
   public async Task<IActionResult> Create(Pizza newPizza)
   {
      var pizza = await _pizzaService.Create(newPizza);
      return CreatedAtAction(nameof(GetById), new { id = pizza!.Id }, pizza);
   }

   [HttpPut("{id}/addtopping")]
   public IActionResult AddTopping(int id, int toppingId)
   {
      var pizzaToUpdate = _pizzaService.GetById(id);

      if(pizzaToUpdate is not null)
      {
         _pizzaService.AddTopping(id, toppingId);
         return NoContent();    
      }
      else
      {
         return NotFound();
      }
   }

   [HttpPut("{id}/updatesauce")]
   public IActionResult UpdateSauce(int id, int sauceId)
   {
      var pizzaToUpdate = _pizzaService.GetById(id);

      if(pizzaToUpdate is not null)
      {
         _pizzaService.UpdateSauce(id, sauceId);
         return NoContent();    
      }
      else
      {
         return NotFound();
      }
   }

   [HttpDelete("{id}")]
   public IActionResult Delete(int id)
   {
      var pizza = _pizzaService.GetById(id);

      if(pizza is not null)
      {
         _pizzaService.DeleteById(id);
         return Ok();
      }
      else
      {
         return NotFound();
      }
   }
}