using AluraChallenges2.Models;
using AluraChallenges2.Services.IServices;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AluraChallenges2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResumoController : ControllerBase
{
    private readonly IResumoService _resumoService;

    public ResumoController(IResumoService resumoService)
    {
        _resumoService = resumoService;
    }

    [HttpGet("{ano}/{mes}")]
    public async Task<IActionResult> GetResumoByMonthAsync(int ano, [Range(1, 12)] int mes)
    {
        if (ano < 1990 || ano > DateTime.Now.Year) return BadRequest("Ano inválido");
        Result<ResumoMes> result = await _resumoService.GetResumoByMonthAsync(ano, mes);
        return Ok(result.Value);
    }
}
