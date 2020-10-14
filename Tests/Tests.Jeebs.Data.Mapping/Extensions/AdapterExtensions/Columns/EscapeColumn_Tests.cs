using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class EscapeColumn_Tests
	{
		[Fact]
		public void With_ExtractedColumn_Calls_EscapeColumn_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();

			var column = Substitute.For<IColumn>();
			var table = F.Rnd.String;
			column.Table.Returns(table);
			var name = F.Rnd.String;
			column.Name.Returns(name);
			var alias = F.Rnd.String;
			column.Alias.Returns(alias);

			// Act
			adapter.EscapeColumn(column);

			// Assert
			adapter.Received(1).EscapeColumn(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().EscapeColumn(name, alias, table);
		}

		[Fact]
		public void With_MappedColumn_Calls_EscapeColumn_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();

			var mapped = Substitute.For<IMappedColumn>();
			var name = F.Rnd.String;
			mapped.Name.Returns(name);
			var alias = F.Rnd.String;
			mapped.Alias.Returns(alias);
			var table = F.Rnd.String;
			mapped.Table.Returns(table);

			// Act
			adapter.EscapeColumn(mapped);

			// Assert
			adapter.Received(1).EscapeColumn(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().EscapeColumn(name, alias, table);
		}
	}
}
