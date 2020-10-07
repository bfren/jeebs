using System;
using System.Collections.Generic;
using System.Text;
using Jx.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class UpdateSingle_Tests
	{
		[Fact]
		public void Unmapped_Model_Throws_MappingException()
		{
			// Arrange
			using var svc = new MapService();
			var adapter = Substitute.For<IAdapter>();

			// Act
			void action() => adapter.UpdateSingle<Foo>(svc);

			// Assert
			var ex = Assert.Throws<UnmappedEntityException>(action);
			Assert.Equal(string.Format(UnmappedEntityException.Format, typeof(Foo)), ex.Message);
		}

		[Fact]
		public void Mapped_Model_No_Writeable_Columns_Throws_MappingException()
		{
			// Arrange
			using var svc = new MapService();
			var adapter = Substitute.For<IAdapter>();
			adapter.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.Arg<string>());

			var table = new FooUnwriteableTable();
			Map<FooUnwriteable>.To<FooUnwriteableTable>(svc);

			// Act
			void action() => adapter.UpdateSingle<FooUnwriteable>(svc);

			// Assert
			var ex = Assert.Throws<NoWriteableColumnsException>(action);
			Assert.Equal(string.Format(NoWriteableColumnsException.Format, table), ex.Message);
		}

		[Fact]
		public void Calls_UpdateSingle_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();

			using var svc = new MapService();
			var foo0 = new FooTable();
			Map<Foo>.To<FooTable>(svc);
			var foo1 = new FooWithVersionTable();
			Map<FooWithVersion>.To<FooWithVersionTable>(svc);

			// Act
			adapter.UpdateSingle<Foo>(svc);
			adapter.UpdateSingle<FooWithVersion>(svc);

			// Assert
			adapter.Received().UpdateSingle(
				foo0.ToString(),
				Arg.Is<List<string>>(x => x.Count == 2 && x[0] == foo0.Bar0 && x[1] == foo0.Bar1),
				Arg.Is<List<string>>(x => x.Count == 2 && x[0] == nameof(foo0.Bar0) && x[1] == nameof(foo0.Bar1)),
				foo0.Id,
				nameof(foo0.Id)
			);

			adapter.Received().UpdateSingle(
				foo1.ToString(),
				Arg.Is<List<string>>(x => x.Count == 3 && x[0] == foo1.Bar0 && x[1] == foo1.Bar1 && x[2] == foo1.Version),
				Arg.Is<List<string>>(x => x.Count == 3 && x[0] == nameof(foo1.Bar0) && x[1] == nameof(foo1.Bar1) && x[2] == nameof(foo1.Version)),
				foo1.Id,
				nameof(foo1.Id),
				foo1.Version,
				nameof(foo1.Version)
			);
		}
	}
}
