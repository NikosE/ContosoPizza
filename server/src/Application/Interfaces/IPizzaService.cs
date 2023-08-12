using Domain.Models;
using Domain.Dto;
using Domain.Dto.Common;

namespace Application.Interfaces;

public interface IPizzaService
   {
      Task<IEnumerable<Pizza>> GetAll();
      Task<PizzaDto> GetById(int id);
      Task<CommandResponse<string>> CreatePizza(CreatePizzaDto dto, CancellationToken token);
      Task<CommandResponse<string>> AddTopping(PizzaDto dto, int ToppingId, CancellationToken token);
      Task<CommandResponse<string>> UpdateSauce(int pizzaId, PizzaSauceDto dto, CancellationToken token);
      Task<CommandResponse<string>> DeletePizza(int pizzaId, CancellationToken token);
   }