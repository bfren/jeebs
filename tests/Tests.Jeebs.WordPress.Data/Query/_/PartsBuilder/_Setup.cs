// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Data;
using Jeebs.Data.Mapping;
using NSubstitute;

namespace Jeebs.WordPress.Data.Query_Tests.PartsBuilder_Tests
{
	public abstract class PartsBuilder_Tests
	{
		public (TestPartsBuilder builder, Vars v) Setup()
		{
			var extract = Substitute.For<IExtract>();

			var client = Substitute.For<IDbClient>();

			var schema = Substitute.For<IWpDbSchema>();

			var builder = new TestPartsBuilder(extract, client, schema);

			var table = new TestTable(F.Rnd.Str, F.Rnd.Str, F.Rnd.Str);

			return (builder, new(client, schema, table));
		}

		public sealed record Vars(
			IDbClient Client,
			IWpDbSchema Schema,
			TestTable Table
		);
	}

	public sealed record TestId(long Value) : StrongId(Value);

	public class TestPartsBuilder : Query.PartsBuilder<TestId>
	{
		public TestPartsBuilder(IExtract extract, IDbClient client, IWpDbSchema schema) : base(extract, client, schema) { }

		public override ITable Table =>
			throw new NotImplementedException();

		public override IColumn IdColumn =>
			throw new NotImplementedException();
	}

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
}
