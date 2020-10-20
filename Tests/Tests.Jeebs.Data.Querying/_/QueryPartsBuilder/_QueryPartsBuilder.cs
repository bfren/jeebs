using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryPartsBuilder_Tests
{
	public static class QueryPartsBuilder
	{
		/// <summary>
		/// Get configured Builder
		/// </summary>
		public static (Builder builder, IAdapter adapter) GetQueryPartsBuilder()
		{
			var adapter = Substitute.For<IAdapter>();
			var from = F.Rnd.Str;
			var builder = new Builder(adapter, from);

			return (builder, adapter);
		}

		public static void CreatesNewListAndAddsJoin(
			Func<IQueryParts, IList<(string, string, string)>?> join,
			Action<Builder, string, string, (string, string)> addJoin
		)
		{
			// Arrange
			var (builder, _) = GetQueryPartsBuilder();
			var table = F.Rnd.Str;
			var on = F.Rnd.Str;
			(string table, string column) equals = (F.Rnd.Str, F.Rnd.Str);

			// Act
			addJoin(builder, table, on, equals);

			// Assert
			var list = Assert.IsType<List<(string table, string on, string equals)>>(join(builder.Parts));
			Assert.Single(list);
		}

		/// <summary>
		/// Test JOIN method when escape is true
		/// </summary>
		/// <param name="addJoin"></param>
		public static void EscapeTrueCallsAdapterEscapeMethods(Action<Builder, string, string, (string, string), bool> addJoin)
		{
			// Arrange
			var (builder, adapter) = GetQueryPartsBuilder();
			var table = F.Rnd.Str;
			var on = F.Rnd.Str;
			(string table, string column) equals = (F.Rnd.Str, F.Rnd.Str);

			// Act
			addJoin(builder, table, on, equals, true);

			// Assert
			adapter.Received().EscapeTable(table);
			adapter.Received().EscapeAndJoin(table, on);
			adapter.Received().EscapeAndJoin(equals.table, equals.column);
		}

		/// <summary>
		/// Test JOIN method when escape is false
		/// </summary>
		/// <param name="addJoin"></param>
		public static void EscapeFalseCallsAdapterJoin(Action<Builder, string, string, (string, string), bool> addJoin)
		{
			// Arrange
			var (builder, adapter) = GetQueryPartsBuilder();
			var table = F.Rnd.Str;
			var on = F.Rnd.Str;
			(string table, string column) equals = (F.Rnd.Str, F.Rnd.Str);

			// Act
			addJoin(builder, table, on, equals, false);

			// Assert
			adapter.DidNotReceive().EscapeTable(table);
			adapter.DidNotReceive().EscapeAndJoin(table, on);
			adapter.DidNotReceive().EscapeAndJoin(equals.table, equals.column);
			adapter.Received().Join(table, on);
			adapter.Received().Join(equals.table, equals.column);
		}

		/// <summary>
		/// Override QueryPartsBuilder to give access to protected methods
		/// </summary>
		public class Builder : QueryPartsBuilder<string, Options>
		{
			public Builder(IAdapter adapter, string from) : base(adapter, from) { }

			public override IQueryParts Build(Options opt)
				=> Parts;

			new public void AddSelect(string select, bool overwrite = false)
				=> base.AddSelect(select, overwrite);

			new public void AddInnerJoin(object table, string on, (object table, string column) equals, bool escape = false)
				=> base.AddInnerJoin(table, on, equals, escape);

			new public void AddLeftJoin(object table, string on, (object table, string column) equals, bool escape = false)
				=> base.AddLeftJoin(table, on, equals, escape);

			new public void AddRightJoin(object table, string on, (object table, string column) equals, bool escape = false)
				=> base.AddRightJoin(table, on, equals, escape);

			new public void AddWhere(string where, object? parameters = null)
				=> base.AddWhere(where, parameters);
		}

		public class Options : QueryOptions { }
	}
}
