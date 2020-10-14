using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class ExtractEscapeAndJoinColumns_Tests
	{
		[Fact]
		public void Extracts_Columns_Calls_EscapeColumn_And_Joins_Using_Separator()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			adapter.ColumnSeparator
				.Returns('|');
			adapter.EscapeColumn(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs("c");

			var table = new FooTable();

			// Act
			var result = adapter.ExtractEscapeAndJoinColumns<Foo>(table);

			// Assert
			adapter.Received(3).EscapeColumn(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().EscapeColumn(table.Id, nameof(table.Id), table.ToString());
			adapter.Received().EscapeColumn(table.Bar0, nameof(table.Bar0), table.ToString());
			adapter.Received().EscapeColumn(table.Bar1, nameof(table.Bar1), table.ToString());
			Assert.Equal("c| c| c", result);
		}
	}
}
