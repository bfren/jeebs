using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class GetColumn_Tests
	{
		[Fact]
		public void With_ExtractedColumn_Calls_EscapeColumn_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();

			var extracted = Substitute.For<IColumn>();
			var table = "foo";
			extracted.Table.Returns(table);
			var column = "bar";
			extracted.Name.Returns(column);
			var alias = "Bar";
			extracted.Alias.Returns(alias);

			// Act
			adapter.GetColumn(extracted);

			// Assert
			adapter.Received().EscapeColumn(
				Arg.Is(column),
				Arg.Is(alias),
				Arg.Is(table)
			);
		}

		[Fact]
		public void With_MappedColumn_Calls_EscapeColumn_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var sep = '/';
			adapter.SchemaSeparator.Returns(sep);

			var mapped = Substitute.For<IMappedColumn>();
			var column = "bar";
			mapped.Name.Returns(column);
			var alias = "Bar";
			mapped.Alias.Returns(alias);
			var table = "foo";
			mapped.Table.Returns(table);

			// Act
			adapter.GetColumn(mapped);

			// Assert
			adapter.Received().EscapeColumn(
				Arg.Is(column),
				Arg.Is(alias),
				Arg.Is(table)
			);
		}
	}
}
