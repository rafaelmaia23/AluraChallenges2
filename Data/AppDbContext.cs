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
		modelBuilder.Entity<Receita>()
			.Property(x => x.Id)
			.HasDefaultValueSql("NEWID()");

		modelBuilder.Entity<Despesa>()
			.Property(x => x.Id)
			.HasDefaultValueSql("NEWID()");
	}

	public DbSet<Receita> Receitas { get; set; }
	public DbSet<Despesa> Despesas { get; set; }
	public DbSet<Categoria> Categorias { get; set; }
}
