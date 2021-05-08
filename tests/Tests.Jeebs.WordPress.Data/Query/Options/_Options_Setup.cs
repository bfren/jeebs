// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Linq.Expressions;
using Jeebs.Data;
using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.WordPress.Data.Query_Tests.Options_Tests
{
	public static class Options_Setup
	{
		public static Vars Get()
		{
			var client = Substitute.For<IDbClient>();

			var schema = Substitute.For<IWpDbSchema>();

			var wpDb = Substitute.For<IWpDb>();
			wpDb.Schema.Returns(schema);

			var table = new TestTable(F.Rnd.Str, F.Rnd.Str, F.Rnd.Str);

			var options = new TestOptions(client, wpDb, table);

			return new(client, wpDb, schema, table, options);
		}

		public sealed record Vars(
			IDbClient DbClient,
			IWpDb WpDb,
			IWpDbSchema Schema,
			TestTable Table,
			TestOptions Options
		);
	}

	public sealed record TestId(long Value) : StrongId(Value);

	public sealed record TestTable : ITable
	{
		private readonly string name;

		public string Id { get; init; }

		public string Foo { get; init; }

		public TestTable(string name, string id, string foo) =>
			(this.name, Id, Foo) = (name, id, foo);

		public string GetName() =>
			name;
	}

	public sealed record TestOptions : Query.Options<TestId, TestTable>
	{
		protected override Expression<Func<TestTable, string>> IdColumn =>
			t => t.Id;

		public TestOptions(IDbClient client, IWpDb db, TestTable table) : base(client, db, table) { }
	}
}
