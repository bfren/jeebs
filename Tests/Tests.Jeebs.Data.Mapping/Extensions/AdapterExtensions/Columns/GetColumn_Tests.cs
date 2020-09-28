using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.AdapterExtensions_Tests
{
	public class GetColumn_Tests
	{
		[Fact]
		public void With_ExtractedColumn_Calls_EscapeColumn_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			var sep = '/';
			adapter.SchemaSeparator.Returns(sep);

			var extracted = Substitute.For<IExtractedColumn>();
			var table = "foo";
			extracted.Table.Returns(table);
			var column = "bar";
			extracted.Column.Returns(column);
			var alias = "Bar";
			extracted.Alias.Returns(alias);

			// Act
			AdapterExtensions.GetColumn(adapter, extracted);

			// Assert
			adapter.Received().EscapeColumn(
				Arg.Is($"{table}{sep}{column}"),
				Arg.Is(alias)
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
			mapped.Column.Returns(column);
			var alias = "Bar";
			var aliasProperty = Substitute.For<PropertyInfo>();
			aliasProperty.Name.Returns(alias);
			mapped.Property.Returns(aliasProperty);

			// Act
			AdapterExtensions.GetColumn(adapter, mapped);

			// Assert
			adapter.Received().EscapeColumn(
				Arg.Is(column),
				Arg.Is(alias)
			);
		}
	}
}
