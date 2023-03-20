using AluraChallenges2.Data;
using AluraChallenges2.Models.Dtos;
using AluraChallenges2.Models;
using AluraChallenges2.Services.IServices;
using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace AluraChallenges2.Services;

public class DespesaService : IDespesaService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public DespesaService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<Result> DeleteDespesaAsync(string id)
    {
        Despesa? despesa = await _appDbContext.Despesas.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (despesa == null) return Result.Fail("Not Found");
        _appDbContext.Despesas.Remove(despesa);
        await _appDbContext.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result<ReadDespesaDto>> GetDespesaByIdAsync(string id)
    {
        Despesa? despesa = await _appDbContext.Despesas.Include(d => d.Categoria).FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (despesa == null) return Result.Fail("Not Found");
        ReadDespesaDto readDespesaDto = _mapper.Map<ReadDespesaDto>(despesa);
        return Result.Ok(readDespesaDto);
    }

    public async Task<Result<List<ReadDespesaDto>>> GetDespesasAsync()
    {
        List<Despesa> despesas = await _appDbContext.Despesas.Include(d => d.Categoria).ToListAsync();
        if (despesas.Count == 0) return Result.Fail("Not Found");
        List<ReadDespesaDto> readDespesaDtos = _mapper.Map<List<ReadDespesaDto>>(despesas);
        return Result.Ok(readDespesaDtos);
    }

    public async Task<Result<ReadDespesaDto>> PostDespesaAsync(UpsertDespesaDto upsertDespesaDto)
    {
        Despesa despesa = _mapper.Map<Despesa>(upsertDespesaDto);
        if (despesa.CategoriaId > _appDbContext.Categorias.Count() || despesa.CategoriaId < 0) 
        {
            return Result.Fail("Categoria Not Found");
        }
        if(despesa.CategoriaId == 0) despesa.CategoriaId = 1;
        if (await IsDuplicated(despesa))
        {
            return Result.Fail("Entrada duplicada");
        }
        await _appDbContext.Despesas.AddAsync(despesa);
        await _appDbContext.SaveChangesAsync();
        ReadDespesaDto readDespesaDto = _mapper.Map<ReadDespesaDto>(despesa);
        return Result.Ok(readDespesaDto);
    }

    public async Task<Result> PutDespesaAsync(UpsertDespesaDto upsertDespesaDto, string id)
    {
        Despesa? despesa = await _appDbContext.Despesas.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (despesa == null) return Result.Fail($"Receita de id {id} Not Found");
        _mapper.Map(upsertDespesaDto, despesa);
        if (await IsDuplicated(despesa))
        {
            return Result.Fail("Entrada duplicada");
        }
        _appDbContext.Despesas.Update(despesa);
        await _appDbContext.SaveChangesAsync();
        return Result.Ok();
    }

    private async Task<bool> IsDuplicated(Despesa despesa)
    {
        List<Despesa> despesas = await _appDbContext.Despesas.ToListAsync();
        despesas.Remove(despesa);
        return despesas.Any(x =>
            x.Data.Year == despesa.Data.Year
            & x.Data.Month == despesa.Data.Month
            & x.Descricao == despesa.Descricao);
    }
}
