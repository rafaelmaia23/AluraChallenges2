using Microsoft.EntityFrameworkCore;

namespace AluraChallenges2.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
	{

	}
}
