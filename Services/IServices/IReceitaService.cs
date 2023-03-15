using AluraChallenges2.Models.Dtos;
using FluentResults;

namespace AluraChallenges2.Services.IServices;

public interface IReceitaService
{
    Task<Result<ReadReceitaDto>> PostReceitaAsync(UpsertReceitaDto upsertReceitaDto);
}
