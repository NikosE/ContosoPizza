using Application.Interfaces;
using Domain.Dto;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Context;

namespace WebApi.Controllers;

public class SauceController : BaseController
{
   private readonly ISauceServices _sauceServices;
   public SauceController(ISauceServices sauceServices)
   {
      _sauceServices = sauceServices;   
   }

   [HttpGet]
   public async Task<IEnumerable<Sauce>> GetAll()
   {
      return await _sauceServices.GetAll();
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(int sauceId)
    => Ok(await _sauceServices.GetById(sauceId));


   [HttpPost]
   public async Task<IActionResult> CreateSauce([FromBody]CreateSauceDto dto, CancellationToken token)
      => Ok(await _sauceServices.CreateSauce(dto, token));


   [HttpPut("{id}")]
   public async Task<IActionResult> UpdateSauce(int sauceId, [FromBody] UpdateSauceDto dto, CancellationToken token)
      => Ok(await _sauceServices.UpdateSauce(sauceId, dto, token));

   
   [HttpDelete("{id}")]
   public async Task<IActionResult> DeleteSauce(int sauceId, CancellationToken token)
      => Ok(await _sauceServices.DeleteSauce(sauceId, token));
}