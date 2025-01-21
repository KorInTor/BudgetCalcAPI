using BudgetCalcAPI.Model;
using BudgetCalcAPI.Model.Transactions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

public class ApplicationContext : DbContext
{
	public DbSet<TransactionType> TransactionTypes => Set<TransactionType>();

	public DbSet<TransactionCategory> TransactionCategories => Set<TransactionCategory>();

	public DbSet<Transaction> Transactions => Set<Transaction>();

	public DbSet<User> Users => Set<User>();

	public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		foreach (var entity in modelBuilder.Model.GetEntityTypes())
		{
			foreach (var property in entity.GetProperties())
			{
				var columnAttribute = property.PropertyInfo?.GetCustomAttribute<ColumnAttribute>();
				if (columnAttribute == null)
				{
					property.SetColumnName(property.Name.ToLower());
				}
			}
		}

		base.OnModelCreating(modelBuilder);
	}
}