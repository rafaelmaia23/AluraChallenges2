using System.ComponentModel.DataAnnotations;

namespace AluraChallenges2.Models;

public class Despesa
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Descricao { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    [Required]
    public double Valor { get; set; }
    [Required]
    public DateTime Data { get; set; }
}
