using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class Join_Tests
	{
		[Fact]
		public void Joins_Using_Separator()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			adapter.SchemaSeparator
				.Returns('/');
			adapter.ColumnSeparator
				.Returns('|');
			adapter.EscapeColumn(Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.ArgAt<string>(0));

			var t = new FooTable();
			var columns = Extract<Foo>.From(t);

			// Act
			var result = AdapterExtensions.Join(adapter, columns);

			// Assert
			Assert.Equal($"{t}/{t.Id}| {t}/{t.Bar0}| {t}/{t.Bar1}", result);
		}
	}
}
