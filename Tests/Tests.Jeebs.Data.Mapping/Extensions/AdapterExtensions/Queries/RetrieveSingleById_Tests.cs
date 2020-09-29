using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class RetrieveSingleById_Tests
	{
		[Fact]
		public void Unmapped_Model_Throws_MappingException()
		{
			// Arrange
			var maps = new TableMaps();
			var adapter = Substitute.For<IAdapter>();

			// Act
			void action() => AdapterExtensions.RetrieveSingleById<Foo>(adapter, maps);

			// Assert
			var ex = Assert.Throws<Jx.Data.MappingException>(action);
			Assert.Equal($"Entity {typeof(Foo)} has not been mapped.", ex.Message);
		}

		[Fact]
		public void Calls_RetrieveSingleById_With_Correct_Arguments()
		{
			// Arrange
			var maps = new TableMaps();
			var adapter = Substitute.For<IAdapter>();
			adapter.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.Arg<string>());
			adapter.EscapeColumn(Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.ArgAt<string>(0));

			var table = new FooTable();
			Map<Foo>.To(table, adapter, maps);

			// Act
			AdapterExtensions.RetrieveSingleById<Foo>(adapter, maps);

			// Assert
			adapter.Received().RetrieveSingleById(
				Arg.Is<List<string>>(x => x.Count == 3 && x[0] == table.Id && x[1] == table.Bar0 && x[2] == table.Bar1),
				Arg.Is(table.ToString()),
				Arg.Is(table.Id)
			);
		}
	}
}
