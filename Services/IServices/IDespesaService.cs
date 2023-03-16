using AluraChallenges2.Models.Dtos;
using FluentResults;

namespace AluraChallenges2.Services.IServices;

public interface IDespesaService
{
    Task<Result> DeleteDespesaAsync(string id);
    Task<Result<ReadDespesaDto>> GetDespesaByIdAsync(string id);
    Task<Result<List<ReadDespesaDto>>> GetDespesasAsync();
    Task<Result<ReadDespesaDto>> PostDespesaAsync(UpsertDespesaDto upsertDespesaDto);
    Task<Result> PutDespesaAsync(UpsertDespesaDto upsertDespesaDto, string id);
}
