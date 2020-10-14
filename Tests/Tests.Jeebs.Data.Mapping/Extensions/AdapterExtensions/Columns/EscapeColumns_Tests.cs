using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class EscapeColumns_Tests
	{
		[Fact]
		public void Calls_Escape_For_Each_String_And_Returns_String_List()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var c0 = F.Rnd.String;
			var c1 = F.Rnd.String;

			// Act
			var result = adapter.EscapeColumns(new[] { c0, c1 });

			// Assert
			adapter.Received(2).Escape(Arg.Any<string>());
			adapter.Received().Escape(c0);
			adapter.Received().Escape(c1);
			Assert.Equal(2, result.Count);
		}

		[Fact]
		public void Calls_EscapeColumn_For_Each_Column_And_Returns_String_List()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var table = new FooTable();
			var columns = Extract<Foo>.From(table);

			// Act
			var result = adapter.EscapeColumns(columns);

			// Assert
			adapter.Received(3).EscapeColumn(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().EscapeColumn(table.Id, nameof(table.Id), table.ToString());
			adapter.Received().EscapeColumn(table.Bar0, nameof(table.Bar0), table.ToString());
			adapter.Received().EscapeColumn(table.Bar1, nameof(table.Bar1), table.ToString());
			Assert.Equal(3, result.Count);
		}

		[Fact]
		public void Calls_EscapeColumn_For_Each_MappedColumn_And_Returns_String_List()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();

			var svc = new MapService();
			var table = new FooTable();
			var map = Map<Foo>.To(table, svc);

			// Act
			var result = adapter.EscapeColumns(map.Columns);

			// Assert
			adapter.Received(3).EscapeColumn(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().EscapeColumn(table.Id, nameof(table.Id), table.ToString());
			adapter.Received().EscapeColumn(table.Bar0, nameof(table.Bar0), table.ToString());
			adapter.Received().EscapeColumn(table.Bar1, nameof(table.Bar1), table.ToString());
			Assert.Equal(3, result.Count);
		}
	}
}
