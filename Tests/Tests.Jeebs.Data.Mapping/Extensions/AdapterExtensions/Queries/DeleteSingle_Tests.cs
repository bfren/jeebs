using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.AdapterExtensions_Tests
{
	public class DeleteSingle_Tests
	{
		[Fact]
		public void Unmapped_Model_Throws_MappingException()
		{
			// Arrange
			var maps = new TableMaps();
			var adapter = Substitute.For<IAdapter>();

			// Act
			void action() => AdapterExtensions.DeleteSingle<Foo>(adapter, maps);

			// Assert
			var ex = Assert.Throws<Jx.Data.MappingException>(action);
			Assert.Equal($"Entity {typeof(Foo)} has not been mapped.", ex.Message);
		}

		[Fact]
		public void Calls_DeleteSingle_With_Correct_Arguments()
		{
			// Arrange
			var maps = new TableMaps();
			var adapter = Substitute.For<IAdapter>();
			adapter.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.Arg<string>());

			var foo0 = new FooTable();
			Map<Foo>.To(foo0, maps);

			var foo1 = new FooWithVersionTable();
			Map<FooWithVersion>.To(foo1, maps);

			// Act
			AdapterExtensions.DeleteSingle<Foo>(adapter, maps);
			AdapterExtensions.DeleteSingle<FooWithVersion>(adapter, maps);

			// Assert
			adapter.Received().DeleteSingle(
				foo0.ToString(),
				foo0.Id,
				nameof(foo0.Id)
			);

			adapter.Received().DeleteSingle(
				foo1.ToString(),
				foo1.Id,
				nameof(foo1.Id),
				foo1.Version,
				nameof(foo1.Version)
			);
		}
	}
}
