using System;
using System.Collections.Generic;
using System.Text;
using Jx.Data.Mapping;
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
			var ex = Assert.Throws<UnmappedEntityException>(action);
			Assert.Equal(string.Format(UnmappedEntityException.Format, typeof(Foo)), ex.Message);
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
			var ex = Assert.Throws<NoWriteableColumnsException>(action);
			Assert.Equal(string.Format(NoWriteableColumnsException.Format, table), ex.Message);
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
