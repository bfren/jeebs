using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class Escape_Tests
	{
		[Fact]
		public void Calls_EscapeAndJoin_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var table = new FooTable();

			// Act
			AdapterExtensions.Escape(adapter, table, x => x.Bar0);

			// Assert
			adapter.Received().EscapeAndJoin(
				Arg.Is(table),
				Arg.Is(table.Bar0)
			);
		}
	}
}
