using AluraChallenges2.Models.Dtos;
using AluraChallenges2.Services.IServices;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetDespesasAsync()
    {
        Result<List<ReadDespesaDto>> result = await _despesaService.GetDespesasAsync();
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
