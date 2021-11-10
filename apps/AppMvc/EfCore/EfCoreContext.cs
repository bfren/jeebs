// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Microsoft.EntityFrameworkCore;

namespace AppMvc.EfCore;

public class EfCoreContext : DbContext
{
	public DbSet<EfCoreAuthRoleEntity> Roles =>
		Set<EfCoreAuthRoleEntity>();

	public DbSet<EfCoreAuthUserRoleEntity> UserRoles =>
		Set<EfCoreAuthUserRoleEntity>();
	public DbSet<EfCoreAuthUserEntity> Users =>
		Set<EfCoreAuthUserEntity>();

	public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var role = modelBuilder.Entity<EfCoreAuthRoleEntity>().ToTable("auth_role");
		role.Property(r => r.Id).HasColumnName("RoleId").HasComputedColumnSql();
		role.Property(r => r.Name).HasColumnName("RoleName");
		role.Property(r => r.Description).HasColumnName("RoleDescription");

		var userRole = modelBuilder.Entity<EfCoreAuthUserRoleEntity>().ToTable("auth_user_role");
		userRole.Property(r => r.Id).HasColumnName("UserRoleId").HasComputedColumnSql().IsRequired();

		var user = modelBuilder.Entity<EfCoreAuthUserEntity>().ToTable("auth_user");
		user.Property(u => u.Id).HasColumnName("UserId").HasComputedColumnSql().IsRequired();
		user.Property(u => u.EmailAddress).HasColumnName("UserEmailAddress");
		user.Property(u => u.FriendlyName).HasColumnName("UserFriendlyName");
		user.Property(u => u.GivenName).HasColumnName("UserGivenName");
		user.Property(u => u.FamilyName).HasColumnName("UserFamilyName");
		user.Property(u => u.IsSuper).HasColumnName("UserIsSuper");
		user.Property(u => u.Version).HasColumnName("UserVersion");
		user.Property(u => u.PasswordHash).HasColumnName("UserPasswordHash");
		user.Property(u => u.IsEnabled).HasColumnName("UserIsEnabled");
		user.Property(u => u.LastSignedIn).HasColumnName("UserLastSignedIn");
	}

	public sealed record class EfCoreAuthRoleEntity
	{
		public long Id { get; init; }

		public string Name { get; init; } = string.Empty;

		public string Description { get; init; } = string.Empty;

		internal EfCoreAuthRoleEntity() { }
	}

	public sealed record class EfCoreAuthUserRoleEntity
	{
		public long Id { get; init; }

		public long UserId { get; init; }

		public long RoleId { get; init; }

		internal EfCoreAuthUserRoleEntity() { }
	}

	public sealed record class EfCoreAuthUserEntity
	{
		public long Id { get; init; }

		public string EmailAddress { get; init; } = string.Empty;

		public string? FriendlyName { get; init; }

		public string? GivenName { get; init; }

		public string? FamilyName { get; init; }

		public bool IsSuper { get; init; }

		public long Version { get; init; }

		public string PasswordHash { get; init; } = string.Empty;

		public bool IsEnabled { get; init; }

		public DateTimeOffset? LastSignedIn { get; init; }

		internal EfCoreAuthUserEntity() { }
	}
}
