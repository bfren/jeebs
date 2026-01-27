// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.Data.Map;
using Jeebs.Data.Query;
using Jeebs.Data.Query.Functions;
using Wrap.Ids;

namespace Jeebs.WordPress.Query;

/// <summary>
/// Common functions for building QueryParts.
/// </summary>
/// <typeparam name="TId">Entity ID type.</typeparam>
public abstract class PartsBuilder<TId> : QueryPartsBuilder<TId>
	where TId : ULongId<TId>, new()
{
	/// <summary>
	/// IDbClient.
	/// </summary>
	protected IDbClient Client { get; private init; }

	internal IDbClient ClientTest =>
		Client;

	/// <summary>
	/// IWpDbSchema.
	/// </summary>
	protected IWpDbSchema T { get; private init; }

	internal IWpDbSchema TTest =>
		T;

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="schema">IWpDbSchema.</param>
	protected PartsBuilder(IWpDbSchema schema) : this(new Extract(), schema) { }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="extract">IExtract.</param>
	/// <param name="schema">IWpDbSchema.</param>
	protected PartsBuilder(IExtract extract, IWpDbSchema schema) : this(extract, new MySqlDbClient(), schema) { }

	/// <summary>
	/// Create object.
	/// </summary>
	/// <param name="extract">IExtract.</param>
	/// <param name="client">IDbClient.</param>
	/// <param name="schema">IWpDbSchema.</param>
	protected PartsBuilder(IExtract extract, IDbClient client, IWpDbSchema schema) : base(extract) =>
		(Client, T) = (client, schema);

	/// <summary>
	/// Escape a table.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <param name="table">Table object.</param>
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1707 // Identifiers should not contain underscores
	protected string __<TTable>(TTable table)
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE1006 // Naming Styles
			where TTable : ITable =>
		Client.Escape(table);

	/// <summary>
	/// Get and escape a column using a Linq Expression selector.
	/// </summary>
	/// <typeparam name="TTable">Table type.</typeparam>
	/// <param name="table">Table object.</param>
	/// <param name="selector">Column selector.</param>
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1707 // Identifiers should not contain underscores
	protected string __<TTable>(TTable table, Expression<Func<TTable, string>> selector)
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE1006 // Naming Styles
			where TTable : ITable =>
		QueryF.GetColumnFromExpression(table, selector).Match(
			ok: Client.EscapeWithTable,
			fail: R.ThrowFailure<string>
		);

	#region Testing

	internal string EscapeTest<TTable>(TTable table)
		where TTable : ITable =>
		__(table);

	internal string EscapeTest<TTable>(TTable table, Expression<Func<TTable, string>> selector)
		where TTable : ITable =>
		__(table, selector);

	#endregion Testing
}
