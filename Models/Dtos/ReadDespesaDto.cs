using System.ComponentModel.DataAnnotations;

namespace AluraChallenges2.Models.Dtos;

public class ReadDespesaDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public int CategoriaId { get; set; }
    public ReadCategoriaDto Categoria { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }
}
