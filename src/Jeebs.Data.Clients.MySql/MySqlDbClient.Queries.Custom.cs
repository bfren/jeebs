// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Clients.MySql;

public partial class MySqlDbClient
{
	/// <inheritdoc/>
	protected override string RandomSort =>
		"RAND()";

	/// <inheritdoc/>
	protected override string GetLimitAndOffset(ulong? maximum, ulong skip, bool sorted) =>
		(maximum, skip) switch
		{
			( > 0, > 0) =>
				$" LIMIT {skip}, {maximum}",

			( > 0, _) =>
				$" LIMIT {maximum}",

			(_, > 0) =>
				$" LIMIT {skip}, 18446744073709551615", // MySQL has no OFFSET without LIMIT, so use the largest possible LIMIT

			_ =>
				string.Empty
		};
}
