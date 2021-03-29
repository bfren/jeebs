// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppMvc.EfCore;
using Jeebs;
using Jeebs.Auth;
using Jeebs.Auth.Data;
using Jeebs.Auth.Data.Entities;
using Jeebs.Auth.Data.Tables;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static F.OptionF;

namespace AppMvc.Controllers
{
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
			const int reps = 500;
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
					Return(
						userId
					)
					.BindAsync(
						userId => provider.UserRole.QueryAsync<AuthUserRoleEntity>(
							(ur => ur.UserId, SearchOperator.Equal, userId.Value)
						)
					)
					.BindAsync(
						userRoles => provider.Role.QueryAsync<AuthRoleEntity>(
							(r => r.Id, SearchOperator.In, userRoles.Select(ur => ur.RoleId.Value))
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
					Return(
						userId
					)
					.BindAsync(
						x => query
							.QueryAsync<AuthRoleEntity>(builder => builder
							.From<AuthRoleTable>()
							.Join<AuthRoleTable, AuthUserRoleTable>(QueryJoin.Inner, t => t.Id, t => t.RoleId)
							.Where<AuthUserRoleTable>(t => t.UserId, SearchOperator.Equal, x.Value)
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
						$"`{db.Role.GetName()}`.`{db.Role.Id}` AS '{nameof(db.Role.Id)}', " +
						$"`{db.Role.GetName()}`.`{db.Role.Name}` AS '{nameof(db.Role.Name)}', " +
						$"`{db.Role.GetName()}`.`{db.Role.Description}` AS '{nameof(db.Role.Description)}' " +
					$"FROM `{db.Role.GetName()}` " +
					$"INNER JOIN `{db.UserRole.GetName()}` " +
						$"ON `{db.Role.GetName()}`.`{db.Role.Id}` " +
						$"= `{db.UserRole.GetName()}`.`{db.UserRole.RoleId}` " +
					$"WHERE `{db.UserRole.GetName()}`.`{db.UserRole.UserId}` = @P0;";

				var roles = await
					Return(
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

			if (false)
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
						(r, _) => r
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
	}
}
