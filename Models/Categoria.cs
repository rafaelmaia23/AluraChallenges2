using System.ComponentModel.DataAnnotations;

namespace AluraChallenges2.Models;

public class Categoria
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<Despesa> Despesas { get; set; }

}
