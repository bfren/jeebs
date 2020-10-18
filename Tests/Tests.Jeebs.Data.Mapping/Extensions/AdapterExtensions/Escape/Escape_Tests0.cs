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
		public void With_ExtractedColumn_Calls_EscapeColumn_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();

			var column = Substitute.For<IColumn>();
			var table = F.Rnd.Str;
			column.Table.Returns(table);
			var name = F.Rnd.Str;
			column.Name.Returns(name);
			var alias = F.Rnd.Str;
			column.Alias.Returns(alias);

			// Act
			adapter.Escape(column);

			// Assert
			adapter.Received(1).Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().Escape(name, alias, table);
		}

		[Fact]
		public void With_MappedColumn_Calls_EscapeColumn_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();

			var mapped = Substitute.For<IMappedColumn>();
			var name = F.Rnd.Str;
			mapped.Name.Returns(name);
			var alias = F.Rnd.Str;
			mapped.Alias.Returns(alias);
			var table = F.Rnd.Str;
			mapped.Table.Returns(table);

			// Act
			adapter.Escape(mapped);

			// Assert
			adapter.Received(1).Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().Escape(name, alias, table);
		}
	}
}
