// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Collections.Generic;
using Jeebs.WordPress.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.QueryPartsBuilderExtended_Tests
{
	public static class QueryPartsBuilderExtended
	{
		public const string BarTable = "bar";

		public const string FooTable = "foo";

		public static (Builder builder, IAdapter adapter, FooTable table) GetQueryPartsBuilder()
		{
			var adapter = Substitute.For<IAdapter>();
			adapter.EscapeTable(Arg.Any<FooTable>())
				.Returns($"[{FooTable}]");

			var table = new FooTable();

			var builder = new Builder(adapter, table);

			return (builder, adapter, table);
		}

		public static void CreatesNewListEscapesAndAddsJoin(
			Func<IQueryParts, IList<(string, string, string)>?> join,
			Action<Builder, FooTable, Func<FooTable, string>, (BarTable table, Func<BarTable, string>)> addJoin
		)
		{
			// Arrange
			var (builder, adapter, _) = GetQueryPartsBuilder();
			var foo = new FooTable();
			var bar = new BarTable();
#pragma warning disable IDE0039 // Use local function
			Func<FooTable, string> on = f => f.FooId;
#pragma warning restore IDE0039 // Use local function
			(BarTable, Func<BarTable, string>) equals = (bar, b => b.BarId);

			// Act
			addJoin(builder, foo, on, equals);

			// Assert
			var list = Assert.IsType<List<(string table, string on, string equals)>>(join(builder.Parts));
			Assert.Single(list);

			adapter.Received().EscapeTable(foo);
			adapter.Received().EscapeAndJoin(foo, foo.FooId);
			adapter.Received().EscapeAndJoin(bar, bar.BarId);
		}

		public class Builder : QueryPartsBuilderExtended<Foo, Options>
		{
			public Builder(IAdapter adapter, FooTable table) : base(adapter, table) { }

			public override IQueryParts Build(Options opt)
				=> Parts;

			new public void AddSelect(params Table[] tables)
				=> base.AddSelect(tables);

#pragma warning disable IDE1006 // Naming Styles
			new public string __<T>(T table)
#pragma warning restore IDE1006 // Naming Styles
				where T : Table
				=> base.__(table);

#pragma warning disable IDE1006 // Naming Styles
			new public string __<T>(T table, Func<T, string> column)
#pragma warning restore IDE1006 // Naming Styles
				where T : Table
				=> base.__(table, column);

			new public void AddInnerJoin<T1, T2>(T1 table, Func<T1, string> on, (T2 table, Func<T2, string> column) equals)
				where T1 : Table
				where T2 : Table
				=> base.AddInnerJoin(table, on, equals);

			new public void AddLeftJoin<T1, T2>(T1 table, Func<T1, string> on, (T2 table, Func<T2, string> column) equals)
				where T1 : Table
				where T2 : Table
				=> base.AddLeftJoin(table, on, equals);

			new public void AddRightJoin<T1, T2>(T1 table, Func<T1, string> on, (T2 table, Func<T2, string> column) equals)
				where T1 : Table
				where T2 : Table
				=> base.AddRightJoin(table, on, equals);
		}

		public class Options : Querying.QueryOptions { }
	}
}
