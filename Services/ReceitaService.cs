using AluraChallenges2.Data;
using AluraChallenges2.Models;
using AluraChallenges2.Models.Dtos;
using AluraChallenges2.Services.IServices;
using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace AluraChallenges2.Services;

public class ReceitaService : IReceitaService
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public ReceitaService(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<Result<List<ReadReceitaDto>>> GetReceitasAsync()
    {
        List<Receita> receitas = await _appDbContext.Receitas.ToListAsync();
        if (receitas.Count == 0) return Result.Fail("Not Found");
        List<ReadReceitaDto> readReceitaDtos = _mapper.Map<List<ReadReceitaDto>>(receitas);
        return Result.Ok(readReceitaDtos);
    }

    public async Task<Result<ReadReceitaDto>> PostReceitaAsync(UpsertReceitaDto upsertReceitaDto)
    {        
        if (await IsDuplicated(upsertReceitaDto))
        {
            return Result.Fail("Entrada duplicada");
        }
        Receita receita = _mapper.Map<Receita>(upsertReceitaDto);
        await _appDbContext.Receitas.AddAsync(receita);
        await _appDbContext.SaveChangesAsync();
        ReadReceitaDto readReceitaDto = _mapper.Map<ReadReceitaDto>(receita);
        return Result.Ok(readReceitaDto);
    }

    private async Task<bool> IsDuplicated(UpsertReceitaDto upsertReceitaDto)
    {
        return await _appDbContext.Receitas.AnyAsync(x =>
            x.Data.Year == upsertReceitaDto.Data.Year
            & x.Data.Month == upsertReceitaDto.Data.Month
            & x.Descricao == upsertReceitaDto.Descricao);
    }
}
