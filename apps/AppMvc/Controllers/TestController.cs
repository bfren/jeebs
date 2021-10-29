// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Diagnostics;
using System.Text;
using AppMvc.EfCore;
using Jeebs;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Tables;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static F.OptionF;

namespace AppMvc.Controllers;

public class TestController : Controller
{
	private readonly AuthDb db;

	private readonly AuthDataProvider provider;

	private readonly AuthDbQuery query;

	private readonly EfCoreContext context;

	public TestController(AuthDb db, AuthDataProvider provider, AuthDbQuery query, EfCoreContext context) =>
		(this.db, this.provider, this.query, this.context) = (db, provider, query, context);

	public async Task<IActionResult> Test0()
	{
		var timer = new Stopwatch();
		var results = new StringBuilder();
		const int reps = 1;
		var userId = new AuthUserId(1);

		results.AppendLine($"Running {reps} times.");

		//
		// Query 1: separate queries
		//

		results.AppendLine("Separate queries:");
		timer.Start();
		for (int i = 0; i < reps; i++)
		{
			var roles = await
				Some(
					userId
				)
				.BindAsync(
					userId => provider.UserRole.QueryAsync<AuthUserRoleEntity>(
						(ur => ur.UserId, Compare.Equal, userId.Value)
					)
				)
				.BindAsync(
					userRoles => provider.Role.QueryAsync<AuthRoleEntity>(
						(r => r.Id, Compare.In, userRoles.Select(ur => ur.RoleId.Value))
					)
				)
				.MapAsync(
					roles => roles.ToList(),
					DefaultHandler
				)
				.AuditAsync(
					some: _ => results.Append('.'),
					none: r => results.AppendLine(r.ToString())
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Separate queries took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		//
		// Query 2: query with builder
		//

		timer.Reset();
		results.AppendLine("Query builder:");
		timer.Start();
		for (int i = 0; i < reps; i++)
		{
			var roles = await
				Some(
					userId
				)
				.BindAsync(
					x => query
						.QueryAsync<AuthRoleEntity>(builder => builder
						.From<AuthRoleTable>()
						.Join<AuthRoleTable, AuthUserRoleTable>(QueryJoin.Inner, t => t.Id, t => t.RoleId)
						.Where<AuthUserRoleTable>(t => t.UserId, Compare.Equal, x.Value)
					)
				)
				.MapAsync(
					x => x.ToList(),
					DefaultHandler
				)
				.AuditAsync(
					some: _ => results.Append('.'),
					none: r => results.AppendLine(r.ToString())
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Query builder took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		//
		// Query 3: manual query
		//

		timer.Reset();
		results.AppendLine("Manual query:");
		timer.Start();
		for (int i = 0; i < reps; i++)
		{
			var sql =
				"SELECT " +
					$"`{db.Role}`.`{db.Role.Id}` AS '{nameof(db.Role.Id)}', " +
					$"`{db.Role}`.`{db.Role.Name}` AS '{nameof(db.Role.Name)}', " +
					$"`{db.Role}`.`{db.Role.Description}` AS '{nameof(db.Role.Description)}' " +
				$"FROM `{db.Role}` " +
				$"INNER JOIN `{db.UserRole}` " +
					$"ON `{db.Role}`.`{db.Role.Id}` " +
					$"= `{db.UserRole}`.`{db.UserRole.RoleId}` " +
				$"WHERE `{db.UserRole}`.`{db.UserRole.UserId}` = @P0;";

			var roles = await
				Some(
					userId
				)
				.BindAsync(
					x => query.QueryAsync<AuthRoleEntity>(sql, new { P0 = x.Value })
				)
				.MapAsync(
					x => x.ToList(),
					DefaultHandler
				)
				.AuditAsync(
					some: _ => results.Append('.'),
					none: r => results.AppendLine(r.ToString())
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Manual query took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		if (true)
		{
			//
			// Query 4: EF Core
			//

			timer.Reset();
			results.AppendLine("Entity Framework:");
			timer.Start();
			for (int i = 0; i < reps; i++)
			{
				var roles = context.Roles.Join(
					context.UserRoles,
					r => r.Id,
					ur => ur.RoleId,
					(r, ur) => new { Role = r, ur.UserId }
				).Where(
					x => x.UserId == userId.Value
				)
				.Select(
					x => x.Role
				);

				if (await roles.CountAsync() == 2)
				{
					results.Append('.');
				}
				else
				{
					results.AppendLine("Error.");
				}
			}
			timer.Stop();
			results.AppendLine(" done.");
			results.AppendFormat("Entity Framework took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
			results.AppendLine();

			//
			// Query 5: EF Core Linq
			//

			timer.Reset();
			results.AppendLine("Entity Framework Linq:");
			timer.Start();
			for (int i = 0; i < reps; i++)
			{
				var roles = from r in context.Roles
							join ur in context.UserRoles on r.Id equals ur.RoleId
							where ur.UserId == userId.Value
							select r;

				if (await roles.CountAsync() == 2)
				{
					results.Append('.');
				}
				else
				{
					results.AppendLine("Error.");
				}
			}
			timer.Stop();
			results.AppendLine(" done.");
			results.AppendFormat("Entity Framework Linq took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
			results.AppendLine();
		}

		return Content(results.ToString());
	}

	public async Task<IActionResult> Test1()
	{
		var timer = new Stopwatch();
		var results = new StringBuilder();
		const int reps = 1;
		var userId = new AuthUserId(1);

		results.AppendLine($"Running {reps} times.");

		//
		// Query 1: separate queries
		//

		results.AppendLine("Separate queries:");
		timer.Start();
		for (int i = 0; i < reps; i++)
		{
			await Some(
					userId
				)
				.BindAsync(
					x => provider.User.RetrieveAsync<AuthUserEntity>(x)
				)
				.AuditAsync(
					some: _ => results.Append('.'),
					none: r => results.AppendLine(r.ToString())
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Separate queries took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		//
		// Query 2: query with builder
		//

		timer.Reset();
		results.AppendLine("Query builder:");
		timer.Start();
		for (int i = 0; i < reps; i++)
		{
			await Some(
					userId
				)
				.BindAsync(
					x => query
						.QuerySingleAsync<AuthUserEntity>(builder => builder
						.From<AuthUserTable>()
						.Where<AuthUserTable>(t => t.Id, Compare.Equal, x.Value)
					)
				)
				.AuditAsync(
					some: _ => results.Append('.'),
					none: r => results.AppendLine(r.ToString())
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Query builder took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		//
		// Query 3: manual query
		//

		timer.Reset();
		results.AppendLine("Manual query:");
		timer.Start();
		for (int i = 0; i < reps; i++)
		{
			var sql =
				"SELECT " +
					$"`{db.User}`.`{db.User.Id}` AS '{nameof(db.User.Id)}', " +
					$"`{db.User}`.`{db.User.EmailAddress}` AS '{nameof(db.User.EmailAddress)}', " +
					$"`{db.User}`.`{db.User.FamilyName}` AS '{nameof(db.User.FamilyName)}', " +
					$"`{db.User}`.`{db.User.FriendlyName}` AS '{nameof(db.User.FriendlyName)}', " +
					$"`{db.User}`.`{db.User.GivenName}` AS '{nameof(db.User.GivenName)}', " +
					$"`{db.User}`.`{db.User.IsEnabled}` AS '{nameof(db.User.IsEnabled)}', " +
					$"`{db.User}`.`{db.User.IsSuper}` AS '{nameof(db.User.IsSuper)}', " +
					$"`{db.User}`.`{db.User.LastSignedIn}` AS '{nameof(db.User.LastSignedIn)}', " +
					$"`{db.User}`.`{db.User.PasswordHash}` AS '{nameof(db.User.PasswordHash)}', " +
					$"`{db.User}`.`{db.User.Version}` AS '{nameof(db.User.Version)}' " +
				$"FROM `{db.User}` " +
				$"WHERE `{db.User}`.`{db.User.Id}` = @P0;";

			await Some(
					userId
				)
				.BindAsync(
					x => query.QuerySingleAsync<AuthUserEntity>(sql, new { P0 = x.Value })
				)
				.AuditAsync(
					some: _ => results.Append('.'),
					none: r => results.AppendLine(r.ToString())
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Manual query took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		if (true)
		{
			//
			// Query 4: EF Core
			//

			timer.Reset();
			results.AppendLine("Entity Framework:");
			timer.Start();
			for (int i = 0; i < reps; i++)
			{
				var user = await context.Users.SingleAsync(u => u.Id == userId.Value);

				if (user.Id == userId.Value)
				{
					results.Append('.');
				}
				else
				{
					results.AppendLine("Error.");
				}
			}
			timer.Stop();
			results.AppendLine(" done.");
			results.AppendFormat("Entity Framework took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
			results.AppendLine();

			//
			// Query 5: EF Core Linq
			//

			timer.Reset();
			results.AppendLine("Entity Framework Linq:");
			timer.Start();
			for (int i = 0; i < reps; i++)
			{
				var users = from u in context.Users
							where u.Id == userId.Value
							select u;

				var user = await users.SingleAsync();

				if (user.Id == userId.Value)
				{
					results.Append('.');
				}
				else
				{
					results.AppendLine("Error.");
				}
			}
			timer.Stop();
			results.AppendLine(" done.");
			results.AppendFormat("Entity Framework Linq took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
			results.AppendLine();
		}

		return Content(results.ToString());
	}

	public async Task<IActionResult> Test2()
	{
		var timer = new Stopwatch();
		var results = new StringBuilder();
		const int reps = 1000;
		var userId = new AuthUserId(1);

		results.AppendLine($"Running {reps} times.");

		//
		// Query 1: separate queries
		//

		results.AppendLine("Separate queries:");
		timer.Start();
		for (int i = 0; i < reps; i++)
		{
			var roles = await
				Some(
					F.Rnd.Lng
				)
				.BindAsync(
					userId => provider.UserRole.QueryAsync<AuthUserRoleEntity>(
						(ur => ur.UserId, Compare.Equal, userId)
					)
				)
				.BindAsync(
					userRoles => provider.Role.QueryAsync<AuthRoleEntity>(
						(r => r.Id, Compare.In, userRoles.Select(ur => ur.RoleId.Value))
					)
				)
				.MapAsync(
					roles => roles.ToList(),
					DefaultHandler
				)
				.AuditAsync(
					any: _ => results.Append('.')
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Separate queries took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		//
		// Query 2: query with builder
		//

		timer.Reset();
		results.AppendLine("Query builder:");
		timer.Start();
		for (int i = 0; i < reps; i++)
		{
			var roles = await
				Some(
					F.Rnd.Lng
				)
				.BindAsync(
					x => query
						.QueryAsync<AuthRoleEntity>(builder => builder
						.From<AuthRoleTable>()
						.Join<AuthRoleTable, AuthUserRoleTable>(QueryJoin.Inner, t => t.Id, t => t.RoleId)
						.Where<AuthUserRoleTable>(t => t.UserId, Compare.Equal, x)
					)
				)
				.MapAsync(
					x => x.ToList(),
					DefaultHandler
				)
				.AuditAsync(
					any: _ => results.Append('.')
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Query builder took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		//
		// Query 3: manual query
		//

		timer.Reset();
		results.AppendLine("Manual query:");
		timer.Start();
		const string sql =
			"SELECT " +
				"`auth_role`.`RoleId` AS 'Id', " +
				"`auth_role`.`RoleName` AS 'Name', " +
				"`auth_role`.`RoleDescription` AS 'Description' " +
			"FROM `auth_role` " +
			"INNER JOIN `auth_user_role` " +
				"ON `auth_role`.`RoleId` " +
				"= `auth_user_role`.`RoleId` " +
			"WHERE `auth_user_role`.`UserId` = @P0;";
		for (int i = 0; i < reps; i++)
		{
			var roles = await
				Some(
					F.Rnd.Lng
				)
				.BindAsync(
					x => query.QueryAsync<AuthRoleEntity>(sql, new { P0 = x })
				)
				.MapAsync(
					x => x.ToList(),
					DefaultHandler
				)
				.AuditAsync(
					any: _ => results.Append('.')
				);
		}
		timer.Stop();
		results.AppendLine(" done.");
		results.AppendFormat("Manual query took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
		results.AppendLine();

		if (true)
		{
			//
			// Query 4: EF Core
			//

			timer.Reset();
			results.AppendLine("Entity Framework:");
			timer.Start();
			for (int i = 0; i < reps; i++)
			{
				var id = F.Rnd.Lng;

				var roles = context.UserRoles.Join(
					context.Roles,
					ur => ur.RoleId,
					r => r.Id,
					(ur, r) => new { Role = r, ur.UserId }
				).Where(
					x => x.UserId == id
				)
				.Select(
					x => x.Role
				);

				await roles.CountAsync();
				results.Append('.');
			}
			timer.Stop();
			results.AppendLine(" done.");
			results.AppendFormat("Entity Framework took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
			results.AppendLine();

			//
			// Query 5: EF Core Linq
			//

			timer.Reset();
			results.AppendLine("Entity Framework Linq:");
			timer.Start();
			for (int i = 0; i < reps; i++)
			{
				var id = F.Rnd.Lng;

				var roles = from r in context.Roles
							join ur in context.UserRoles on r.Id equals ur.RoleId
							where ur.UserId == id
							select r;

				await roles.CountAsync();
				results.Append('.');
			}
			timer.Stop();
			results.AppendLine(" done.");
			results.AppendFormat("Entity Framework Linq took {0:0.000}s.", TimeSpan.FromTicks(timer.ElapsedTicks).TotalSeconds);
			results.AppendLine();
		}

		return Content(results.ToString());
	}
}
