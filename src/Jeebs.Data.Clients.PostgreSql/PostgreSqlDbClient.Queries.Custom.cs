// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Clients.PostgreSql;

public partial class PostgreSqlDbClient
{
	/// <inheritdoc/>
	protected override string RandomSort =>
		"random()";

	/// <inheritdoc/>
	protected override string GetLimitAndOffset(ulong? maximum, ulong skip) =>
		(maximum, skip) switch
		{
			( > 0, > 0) =>
				$" LIMIT {maximum} OFFSET {skip}",

			( > 0, _) =>
				$" LIMIT {maximum}",

			(_, > 0) =>
				$" OFFSET {skip}",

			_ =>
				string.Empty
		};
}
