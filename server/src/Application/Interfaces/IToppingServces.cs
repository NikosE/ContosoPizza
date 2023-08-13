using Domain.Dto;
using Domain.Dto.Common;
using Domain.Models;

namespace Application.Interfaces;

public interface IToppingServces
{
   Task<IEnumerable<Topping>> GetAll();
   Task<ToppingDto> GetById(int toppingId);
   Task<CommandResponse<string>> CreateTopping(CreateToppingDto dto, CancellationToken token);
   Task<CommandResponse<string>> UpdateTopping(int toppingId, UpdateToppingDto dto, CancellationToken token);
   Task<CommandResponse<string>> DeleteTopping(int toppingId, CancellationToken token);
}