// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Clients.SqlServer;

public partial class SqlServerDbClient
{
	/// <inheritdoc/>
	protected override string RandomSort =>
		"NEWID()";

	/// <inheritdoc/>
	protected override string QueryTerminator =>
		string.Empty;

	/// <inheritdoc/>
	protected override string GetLimitAndOffset(ulong? maximum, ulong skip, bool sorted)
	{
		var offset = (maximum, skip) switch
		{
			( > 0, _) =>
				$" OFFSET {skip} ROWS FETCH NEXT {maximum} ROWS ONLY",

			(_, > 0) =>
				$" OFFSET {skip} ROWS",

			_ =>
				string.Empty
		};

		// SQL Server only allows OFFSET after ORDER BY, so sort by nothing when there is no sort
		return (offset.Length > 0 && !sorted) switch
		{
			true =>
				$" ORDER BY (SELECT NULL){offset}",

			false =>
				offset
		};
	}
}
