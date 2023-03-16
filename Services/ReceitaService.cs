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

    public async Task<Result<ReadReceitaDto>> GetReceitaByIdAsync(string id)
    {
        Receita? receita = await _appDbContext.Receitas.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (receita == null) return Result.Fail("Not Found");
        ReadReceitaDto readReceitaDto = _mapper.Map<ReadReceitaDto>(receita);
        return Result.Ok(readReceitaDto);
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
        Receita receita = _mapper.Map<Receita>(upsertReceitaDto);
        if (await IsDuplicated(receita))
        {
            return Result.Fail("Entrada duplicada");
        }
        await _appDbContext.Receitas.AddAsync(receita);
        await _appDbContext.SaveChangesAsync();
        ReadReceitaDto readReceitaDto = _mapper.Map<ReadReceitaDto>(receita);
        return Result.Ok(readReceitaDto);
    }

    public async Task<Result> PutReceitaAsync(UpsertReceitaDto upsertReceitaDto, string id)
    {
        Receita? receita = await _appDbContext.Receitas.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        if (receita == null) return Result.Fail($"Receita de id {id} Not Found");
        _mapper.Map(upsertReceitaDto, receita);
        if (await IsDuplicated(receita))
        {
            return Result.Fail("Entrada duplicada");
        }
        _appDbContext.Receitas.Update(receita);
        await _appDbContext.SaveChangesAsync();
        return Result.Ok();
    }

    private async Task<bool> IsDuplicated(Receita receita)
    {
        List<Receita> receitas = await _appDbContext.Receitas.ToListAsync();
        receitas.Remove(receita);
        return receitas.Any(x =>
            x.Data.Year == receita.Data.Year
            & x.Data.Month == receita.Data.Month
            & x.Descricao == receita.Descricao);
    }
}
