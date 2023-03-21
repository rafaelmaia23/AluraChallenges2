using AluraChallenges2.Models.Dtos;
using FluentResults;

namespace AluraChallenges2.Services.IServices;

public interface IReceitaService
{
    Task<Result> DeleteReceitaAsync(string id);
    Task<Result<ReadReceitaDto>> GetReceitaByIdAsync(string id);
    Task<Result<List<ReadReceitaDto>>> GetReceitasAsync(string? descricao);
    Task<Result<List<ReadReceitaDto>>> GetReceitasByMonthAsync(int ano, int mes);
    Task<Result<ReadReceitaDto>> PostReceitaAsync(UpsertReceitaDto upsertReceitaDto);
    Task<Result> PutReceitaAsync(UpsertReceitaDto upsertReceitaDto, string id);
}
