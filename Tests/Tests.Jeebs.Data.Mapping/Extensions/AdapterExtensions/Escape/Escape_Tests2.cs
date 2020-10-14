using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public partial class Escape_Tests
	{
		[Fact]
		public void Calls_EscapeColumn_For_Each_Column_And_Returns_String_List()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var table = new FooTable();
			var columns = Extract<Foo>.From(table);

			// Act
			var result = adapter.Escape(columns);

			// Assert
			adapter.Received(3).Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().Escape(table.Id, nameof(table.Id), table.ToString());
			adapter.Received().Escape(table.Bar0, nameof(table.Bar0), table.ToString());
			adapter.Received().Escape(table.Bar1, nameof(table.Bar1), table.ToString());
			Assert.Equal(3, result.Count);
		}
	}
}
