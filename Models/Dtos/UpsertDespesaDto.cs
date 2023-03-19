using System.ComponentModel.DataAnnotations;

namespace AluraChallenges2.Models.Dtos;

public class UpsertDespesaDto
{
    [Required]
    public string Descricao { get; set; }
    public int CategoriaId { get; set; }
    [Required]
    public double Valor { get; set; }
    [Required]
    public DateTime Data { get; set; }
}
