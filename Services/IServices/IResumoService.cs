using AluraChallenges2.Models;
using FluentResults;

namespace AluraChallenges2.Services.IServices;

public interface IResumoService
{
    Task<Result<ResumoMes>> GetResumoByMonthAsync(int ano, int mes);
}
