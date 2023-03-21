namespace AluraChallenges2.Models;

public class ResumoMes
{
    public double TotalReceitas { get; set; }
    public double TotalDespesas { get; set; }
    public double SaldoMes { get; set; }
    public List<ResumoCategoria> ResumoCategorias { get; set; }
}
