using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class Extract_Tests
	{
		[Fact]
		public void Extracts_Columns_Joins_Using_Separator()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			adapter.ColumnSeparator
				.Returns('|');
			adapter.EscapeColumn(Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.ArgAt<string>(0));

			var t = new FooTable();

			// Act
			var result= AdapterExtensions.Extract<Foo>(adapter, t);

			// Assert
			Assert.Equal($"{t.Id}| {t.Bar0}| {t.Bar1}", result);
		}
	}
}
