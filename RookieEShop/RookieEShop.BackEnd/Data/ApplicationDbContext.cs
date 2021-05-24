﻿using Microsoft.EntityFrameworkCore;
using RookieEShop.BackEnd.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RookieEShop.BackEnd.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public DbSet<Category> Categories { get; set; }

		public DbSet<Product> Products { get; set; }
		
		public DbSet<Rating> Ratings { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasColumnType("decimal");

			modelBuilder.Entity<Product>()
				.HasOne<Category>(s => s.Category)
				.WithMany(x => x.Products)
				.HasForeignKey(x => x.CategoryID);

			modelBuilder.Entity<Rating>()
				.HasOne<Product>(s => s.Product)
				.WithMany(g => g.Rating)
				.HasForeignKey(s => s.ProductId);

			modelBuilder.Entity<User>()
				.ToTable("AspNetUsers");
		}
	}
}
