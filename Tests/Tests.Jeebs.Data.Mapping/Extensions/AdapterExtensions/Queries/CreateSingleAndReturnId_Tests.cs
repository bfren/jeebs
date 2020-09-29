using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class CreateSingleAndReturnId_Tests
	{
		[Fact]
		public void Unmapped_Model_Throws_MappingException()
		{
			// Arrange
			var maps = new TableMaps();
			var adapter = Substitute.For<IAdapter>();

			// Act
			void action() => AdapterExtensions.CreateSingleAndReturnId<Foo>(adapter, maps);

			// Assert
			var ex = Assert.Throws<Jx.Data.Mapping.UnmappedEntityException>(action);
			Assert.Equal($"Entity {typeof(Foo)} has not been mapped.", ex.Message);
		}

		[Fact]
		public void Mapped_Model_No_Writeable_Columns_Throws_MappingException()
		{
			// Arrange
			var maps = new TableMaps();
			var adapter = Substitute.For<IAdapter>();
			adapter.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.Arg<string>());

			var table = new FooUnwriteableTable();
			Map<FooUnwriteable>.To(table, maps);

			// Act
			void action() => AdapterExtensions.CreateSingleAndReturnId<FooUnwriteable>(adapter, maps);

			// Assert
			var ex = Assert.Throws<Jx.Data.Mapping.NoWriteableColumnsException>(action);
			Assert.Equal($"Table {table} does not have any writeable columns.", ex.Message);
		}

		[Fact]
		public void Calls_CreateSingleAndReturnId_With_Correct_Arguments()
		{
			// Arrange
			var maps = new TableMaps();
			var adapter = Substitute.For<IAdapter>();
			adapter
				.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.Arg<string>());

			var table = new FooTable();
			Map<Foo>.To(table, maps);

			// Act
			AdapterExtensions.CreateSingleAndReturnId<Foo>(adapter, maps);

			// Assert
			adapter.Received().CreateSingleAndReturnId(
				table.ToString(),
				Arg.Is<List<string>>(x => x.Count == 2 && x[0] == table.Bar0 && x[1] == table.Bar1),
				Arg.Is<List<string>>(x => x.Count == 2 && x[0] == nameof(table.Bar0) && x[1] == nameof(table.Bar1))
			); ;
		}
	}
}
