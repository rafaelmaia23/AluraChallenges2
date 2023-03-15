using System.ComponentModel.DataAnnotations;

namespace AluraChallenges2.Models
{
    public class Receita
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
    }
}
