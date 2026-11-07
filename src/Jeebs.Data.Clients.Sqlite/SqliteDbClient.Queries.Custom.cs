// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Clients.Sqlite;

public partial class SqliteDbClient
{
	/// <inheritdoc/>
	protected override string RandomSort =>
		"random()";

	/// <inheritdoc/>
	protected override string GetLimitAndOffset(ulong? maximum, ulong skip) =>
		(maximum, skip) switch
		{
			( > 0, > 0) =>
				$" LIMIT {skip}, {maximum}",

			( > 0, _) =>
				$" LIMIT {maximum}",

			(_, > 0) =>
				$" LIMIT {skip}, -1", // a negative LIMIT means no limit

			_ =>
				string.Empty
		};
}
