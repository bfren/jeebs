using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Mapping.AdapterExtensions_Tests.Adapter;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class ExtractEscapeAndJoin_Tests
	{
		[Fact]
		public void Extracts_Columns_Calls_EscapeColumn_And_Joins_Using_Separator()
		{
			// Arrange
			var adapter = GetAdapter();
			var table = new FooTable();

			// Act
			var result = adapter.ExtractEscapeAndJoin<Foo>(table);

			// Assert
			adapter.Received(3).Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().Escape(table.FooId, nameof(table.FooId), table.ToString());
			adapter.Received().Escape(table.Bar0, nameof(table.Bar0), table.ToString());
			adapter.Received().Escape(table.Bar1, nameof(table.Bar1), table.ToString());
			Assert.Equal($"{__(table.FooId)}| {__(table.Bar0)}| {__(table.Bar1)}", result);
		}
	}
}
