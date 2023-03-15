namespace AluraChallenges2.Models;

public class Despesa
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime Data { get; set; }
}
