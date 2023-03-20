using AluraChallenges2.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraChallenges2.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Despesa>()
			.HasOne(d => d.Categoria)
			.WithMany(c => c.Despesas)
			.HasForeignKey(d => d.CategoriaId);

		modelBuilder.Entity<Receita>()
			.Property(x => x.Id)
			.HasDefaultValueSql("NEWID()");

		modelBuilder.Entity<Despesa>()
			.Property(x => x.Id)
			.HasDefaultValueSql("NEWID()");

		List<string> categorias = new List<string>
		{
			"Outras", "Alimentação", "Saúde", "Moradia", "Transporte", "Educação", "Lazer", "Imprevistos"
		};
		for (int i = 1; i <= categorias.Count; i++)
		{
			modelBuilder.Entity<Categoria>()
				.HasData(new Categoria
				{
					Id = i,
					Name = categorias[i - 1]
				});
		}
	}

    public DbSet<Receita> Receitas { get; set; }
	public DbSet<Despesa> Despesas { get; set; }
	public DbSet<Categoria> Categorias { get; set; }
}
