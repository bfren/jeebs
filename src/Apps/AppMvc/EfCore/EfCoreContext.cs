// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Data;
using Microsoft.EntityFrameworkCore;

namespace AppMvc.EfCore
{
	public class EfCoreContext : DbContext
	{
		public DbSet<EfCoreAuthRoleEntity> Roles =>
			Set<EfCoreAuthRoleEntity>();

		public DbSet<EfCoreAuthUserRoleEntity> UserRoles =>
			Set<EfCoreAuthUserRoleEntity>();

		public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var role = modelBuilder.Entity<EfCoreAuthRoleEntity>().ToTable("auth_role");
			role.Property(r => r.Id).HasColumnName("RoleId").HasComputedColumnSql();
			role.Property(r => r.Name).HasColumnName("RoleName");
			role.Property(r => r.Description).HasColumnName("RoleDescription");

			var userRole = modelBuilder.Entity<EfCoreAuthUserRoleEntity>().ToTable("auth_user_role");
			userRole.Property(r => r.Id).HasColumnName("UserRoleId").HasComputedColumnSql().IsRequired();
		}

		public sealed record EfCoreAuthRoleEntity
		{
			public long Id { get; init; }

			public string Name { get; init; } = string.Empty;

			public string Description { get; init; } = string.Empty;

			internal EfCoreAuthRoleEntity() { }
		}

		public sealed record EfCoreAuthUserRoleEntity
		{
			public long Id { get; init; }

			public long UserId { get; init; }

			public long RoleId { get; init; }

			internal EfCoreAuthUserRoleEntity() { }
		}
	}
}
