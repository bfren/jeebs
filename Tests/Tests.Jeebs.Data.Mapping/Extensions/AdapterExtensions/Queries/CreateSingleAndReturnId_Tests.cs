using System;
using System.Collections.Generic;
using System.Text;
using Jx.Data.Mapping;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Mapping.AdapterExtensions_Tests.Adapter;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class CreateSingleAndReturnId_Tests
	{
		[Fact]
		public void Unmapped_Model_Throws_MappingException()
		{
			// Arrange
			using var svc = new MapService();
			var adapter = Substitute.For<IAdapter>();

			// Act
			void action() => adapter.CreateSingleAndReturnId<Foo>(svc);

			// Assert
			var ex = Assert.Throws<UnmappedEntityException>(action);
			Assert.Equal(string.Format(UnmappedEntityException.Format, typeof(Foo)), ex.Message);
		}

		[Fact]
		public void Mapped_Model_No_Writeable_Columns_Throws_MappingException()
		{
			// Arrange
			var adapter = GetAdapter();
			using var svc = new MapService();
			Map<FooUnwriteable>.To<FooUnwriteableTable>(svc);
			var table = new FooUnwriteableTable();

			// Act
			void action() => adapter.CreateSingleAndReturnId<FooUnwriteable>(svc);

			// Assert
			var ex = Assert.Throws<NoWriteableColumnsException>(action);
			Assert.Equal(string.Format(NoWriteableColumnsException.Format, table), ex.Message);
		}

		[Fact]
		public void Calls_CreateSingleAndReturnId_With_Correct_Arguments()
		{
			// Arrange
			var adapter = GetAdapter();
			using var svc = new MapService();
			Map<Foo>.To<FooTable>(svc);
			var table = new FooTable();

			// Act
			adapter.CreateSingleAndReturnId<Foo>(svc);

			// Assert
			adapter.Received().CreateSingleAndReturnId(
				__(table),
				Arg.Is<List<string>>(x => x.Count == 2 && x[0] == __(table.Bar0) && x[1] == __(table.Bar1)),
				Arg.Is<List<string>>(x => x.Count == 2 && x[0] == nameof(table.Bar0) && x[1] == nameof(table.Bar1))
			);
		}
	}
}
