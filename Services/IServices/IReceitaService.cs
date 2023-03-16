using AluraChallenges2.Models.Dtos;
using FluentResults;

namespace AluraChallenges2.Services.IServices;

public interface IReceitaService
{
    Task<Result<List<ReadReceitaDto>>> GetReceitas();
    Task<Result<ReadReceitaDto>> PostReceitaAsync(UpsertReceitaDto upsertReceitaDto);
}
