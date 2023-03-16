﻿using AluraChallenges2.Models.Dtos;
using AluraChallenges2.Services.IServices;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetReceitasAsync()
    {
        Result<List<ReadReceitaDto>> result = await _receitaService.GetReceitasAsync();
        if (result.IsFailed) return NotFound();
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReceitaByIdAsync(string id)
    {
        Result<ReadReceitaDto> result = await _receitaService.GetReceitaByIdAsync(id);
        if (result.IsFailed) return NotFound();
        return Ok(result.Value);
    }
}
