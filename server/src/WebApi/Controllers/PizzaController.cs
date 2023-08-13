using Application.Interfaces;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class PizzaController : BaseController
{
   private readonly IPizzaService _pizzaService;
   
   public PizzaController(IPizzaService pizzaService)
   {
      _pizzaService = pizzaService;
   }

   [HttpGet]
   public async Task<IEnumerable<Pizza>> GetAll()
   {
      return await _pizzaService.GetAll();
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(int id)
    => Ok(await _pizzaService.GetById(id));


   [HttpPost]
   public async Task<IActionResult> CreatePizza([FromBody]CreatePizzaDto dto, CancellationToken token)
      => Ok(await _pizzaService.CreatePizza(dto, token));


   [HttpPut("{id}/addtopping")]
   public async Task<IActionResult> AddTopping([FromBody]PizzaDto dto, int toppingId, CancellationToken token)
      => Ok(await _pizzaService.AddTopping(dto, toppingId, token));
      

   [HttpPut("{id}/updatesauce")]
   public async Task<IActionResult> UpdateSauce(int id, [FromBody] PizzaSauceDto dto, CancellationToken token)
      => Ok(await _pizzaService.UpdateSauce(id, dto, token));

   
   [HttpDelete("{id}")]
   public async Task<IActionResult> DeletePizza(int id, CancellationToken token)
      => Ok(await _pizzaService.DeletePizza(id, token));
}