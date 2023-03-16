using AluraChallenges2.Models.Dtos;
using FluentResults;

namespace AluraChallenges2.Services.IServices;

public interface IReceitaService
{
    Task<Result<ReadReceitaDto>> GetReceitaByIdAsync(string id);
    Task<Result<List<ReadReceitaDto>>> GetReceitasAsync();
    Task<Result<ReadReceitaDto>> PostReceitaAsync(UpsertReceitaDto upsertReceitaDto);
}
