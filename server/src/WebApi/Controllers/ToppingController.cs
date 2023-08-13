using Application.Interfaces;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class ToppingController : BaseController
{
private readonly IToppingServces _toppingServces;
   public ToppingController(IToppingServces toppingServces)
   {
      _toppingServces = toppingServces;   
   }

   [HttpGet]
   public async Task<IEnumerable<Topping>> GetAll()
   {
      return await _toppingServces.GetAll();
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(int toppingId)
    => Ok(await _toppingServces.GetById(toppingId));


   [HttpPost]
   public async Task<IActionResult> CreateTopping([FromBody]CreateToppingDto dto, CancellationToken token)
      => Ok(await _toppingServces.CreateTopping(dto, token));


   [HttpPut("{id}")]
   public async Task<IActionResult> UpdateTopping(int toppingId, [FromBody] UpdateToppingDto dto, CancellationToken token)
      => Ok(await _toppingServces.UpdateTopping(toppingId, dto, token));

   
   [HttpDelete("{id}")]
   public async Task<IActionResult> DeleteTopping(int toppingId, CancellationToken token)
      => Ok(await _toppingServces.DeleteTopping(toppingId, token));
}