using KoiParadise.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiParadise.Data;

public class DataContext : DbContext
{
	public DbSet<UserInfo> UserInfos { get; set; } = default!;
	
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}
}
