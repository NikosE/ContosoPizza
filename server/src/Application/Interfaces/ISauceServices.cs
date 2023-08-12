using Domain.Dto;
using Domain.Dto.Common;
using Domain.Models;

namespace Application.Interfaces;

public interface ISauceServices
{
   Task<IEnumerable<Sauce>> GetAll();
   Task<SauceDto> GetById(int sauceId);
   Task<CommandResponse<string>> CreateSauce(CreateSauceDto dto, CancellationToken token);
   Task<CommandResponse<string>> UpdateSauce(int sauceId, UpdateSauceDto dto, CancellationToken token);
   Task<CommandResponse<string>> DeleteSauce(int sauceId, CancellationToken token);
}