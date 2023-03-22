using AluraChallenges2.Data;
using AluraChallenges2.Models;
using AluraChallenges2.Services.IServices;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace AluraChallenges2.Services;

public class ResumoService : IResumoService
{
    private readonly AppDbContext _appDbContext;

    public ResumoService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<ResumoMes>> GetResumoByMonthAsync(int ano, int mes)
    {
        ResumoMes resumoMes = new ResumoMes();

        List<Receita> receitas = await _appDbContext.Receitas
            .Where(r => r.Data.Year == ano && r.Data.Month == mes).ToListAsync();
        foreach(Receita receita in receitas)
        {
            resumoMes.TotalReceitas += receita.Valor;
        }

        List<Despesa> despesas = await _appDbContext.Despesas
            .Where(r => r.Data.Year == ano && r.Data.Month == mes).Include(r => r.Categoria).ToListAsync();
        foreach(Despesa despesa in despesas)
        {
            resumoMes.TotalDespesas += despesa.Valor;
            if (resumoMes.ResumoCategorias.Any(c => c.Name == despesa.Categoria.Name))
            {
                resumoMes.ResumoCategorias
                    .FirstOrDefault(c => c.Name == despesa.Categoria.Name).TotalGasto += despesa.Valor;
            }
            else
            {
                ResumoCategoria resumoCategoria = new ResumoCategoria();
                resumoCategoria.Name = despesa.Categoria.Name;
                resumoCategoria.TotalGasto += despesa.Valor;
                resumoMes.ResumoCategorias.Add(resumoCategoria);
            }
        }

        resumoMes.SaldoMes = resumoMes.TotalReceitas - resumoMes.TotalDespesas;

        return Result.Ok(resumoMes);

    }
}
