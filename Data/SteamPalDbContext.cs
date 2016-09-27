using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using netcoretest.Models;

namespace netcoretest.Data
{
    public class SteamPalDbContext : DbContext
    {
		public DbSet<Account> Accounts { get; set; }

		public SteamPalDbContext(DbContextOptions<SteamPalDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Account>().ToTable("Account");

			base.OnModelCreating(modelBuilder);
		}
	}
}
