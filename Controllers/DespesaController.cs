using AluraChallenges2.Models.Dtos;
using AluraChallenges2.Services;
using AluraChallenges2.Services.IServices;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AluraChallenges2.Controllers;

[ApiController]
[Route("/despesas")]
public class DespesaController : ControllerBase
{
    private readonly IDespesaService _despesaService;

    public DespesaController(IDespesaService despesaService)
    {
        _despesaService = despesaService;
    }

    [HttpPost]
    public async Task<IActionResult> PostDespesaAsync(UpsertDespesaDto upsertDespesaDto)
    {
        Result<ReadDespesaDto> result = await _despesaService.PostDespesaAsync(upsertDespesaDto);
        if (result.IsFailed) return BadRequest(result.Reasons);
        return CreatedAtAction(nameof(GetDespesaByIdAsync), new { id = result.Value.Id }, result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetDespesasAsync([FromQuery] string? descricao = null)
     {
        Result<List<ReadDespesaDto>> result = await _despesaService.GetDespesasAsync(descricao);
        if (result.IsFailed) return NotFound();
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    [ActionName("GetDespesaByIdAsync")]
    public async Task<IActionResult> GetDespesaByIdAsync(string id)
    {
        Result<ReadDespesaDto> result = await _despesaService.GetDespesaByIdAsync(id);
        if (result.IsFailed) return NotFound();
        return Ok(result.Value);
    }

    [HttpGet("{ano}/{mes}")]
    public async Task<IActionResult> GetDespesasByMonthAsync(int ano, [Range(1, 12)] int mes)
    {
        if (ano < 1990 || ano > DateTime.Now.Year) return BadRequest("Ano inválido");
        Result<List<ReadDespesaDto>> result = await _despesaService.GetDespesasByMonthAsync(ano, mes);
        if (result.IsFailed) return NotFound();
        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDespesaAsync(UpsertDespesaDto upsertDespesaDto, string id)
    {
        Result result = await _despesaService.PutDespesaAsync(upsertDespesaDto, id);
        if (result.IsFailed) return BadRequest(result.Reasons);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDespesaAsync(string id)
    {
        Result result = await _despesaService.DeleteDespesaAsync(id);
        if (result.IsFailed) return NotFound();
        return Ok();
    }
}
