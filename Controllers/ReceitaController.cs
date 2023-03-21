using AluraChallenges2.Models.Dtos;
using AluraChallenges2.Services.IServices;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AluraChallenges2.Controllers;

[ApiController]
[Route("/receitas")]
public class ReceitaController : ControllerBase
{
    private readonly IReceitaService _receitaService;

    public ReceitaController(IReceitaService receitaService)
    {
        _receitaService = receitaService;
    }

    [HttpPost]
    public async Task<IActionResult> PostReceitaAsync(UpsertReceitaDto upsertReceitaDto)
    {
        Result<ReadReceitaDto> result = await _receitaService.PostReceitaAsync(upsertReceitaDto);
        if (result.IsFailed) return BadRequest(result.Reasons);
        return CreatedAtAction(nameof(GetReceitaByIdAsync), new { id = result.Value.Id }, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetReceitasAsync([FromQuery] string? descricao = null)
    {
        Result<List<ReadReceitaDto>> result = await _receitaService.GetReceitasAsync(descricao);
        if (result.IsFailed) return NotFound();
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [ActionName("GetReceitaByIdAsync")]
    public async Task<IActionResult> GetReceitaByIdAsync(string id)
    {
        Result<ReadReceitaDto> result = await _receitaService.GetReceitaByIdAsync(id);
        if (result.IsFailed) return NotFound();
        return Ok(result.Value);
    }

    [HttpGet("{ano}/{mes}")]
    public async Task<IActionResult> GetReceitasByMonthAsync(int ano, [Range(1,12)]int mes)
    {
        if (ano < 1990 || ano > DateTime.Now.Year) return BadRequest("Ano inválido");
        Result<List<ReadReceitaDto>> result = await _receitaService.GetReceitasByMonthAsync(ano, mes);
        if (result.IsFailed) return NotFound();
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutReceitaAsync(UpsertReceitaDto upsertReceitaDto, string id)
    {
        Result result = await _receitaService.PutReceitaAsync(upsertReceitaDto, id);
        if (result.IsFailed) return BadRequest(result.Reasons);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReceitaAsync(string id)
    {
        Result result = await _receitaService.DeleteReceitaAsync(id);
        if (result.IsFailed) return NotFound();
        return Ok();
    }
}
