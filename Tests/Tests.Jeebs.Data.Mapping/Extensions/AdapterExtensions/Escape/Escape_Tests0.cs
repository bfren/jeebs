// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			var table = JeebsF.Rnd.Str;
			column.Table.Returns(table);
			var name = JeebsF.Rnd.Str;
			column.Name.Returns(name);
			var alias = JeebsF.Rnd.Str;
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
			var name = JeebsF.Rnd.Str;
			mapped.Name.Returns(name);
			var alias = JeebsF.Rnd.Str;
			mapped.Alias.Returns(alias);
			var table = JeebsF.Rnd.Str;
			mapped.Table.Returns(table);

			// Act
			adapter.Escape(mapped);

			// Assert
			adapter.Received(1).Escape(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
			adapter.Received().Escape(name, alias, table);
		}
	}
}
