using System;
using System.Collections.Generic;
using System.Text;
using Jx.Data.Mapping;
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
			using var svc = new MapService();
			var adapter = Substitute.For<IAdapter>();

			// Act
			void action() => adapter.RetrieveSingleById<Foo>(svc);

			// Assert
			var ex = Assert.Throws<UnmappedEntityException>(action);
			Assert.Equal(string.Format(UnmappedEntityException.Format, typeof(Foo)), ex.Message);
		}

		[Fact]
		public void Calls_RetrieveSingleById_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			adapter.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.Arg<string>());
			adapter.EscapeColumn(Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.ArgAt<string>(0));

			using var svc = new MapService();
			Map<Foo>.To<FooTable>(svc);

			var table = new FooTable();

			// Act
			adapter.RetrieveSingleById<Foo>(svc);

			// Assert
			adapter.Received().RetrieveSingleById(
				Arg.Is<List<string>>(x => x.Count == 3 && x[0] == table.Id && x[1] == table.Bar0 && x[2] == table.Bar1),
				Arg.Is(table.ToString()),
				Arg.Is(table.Id)
			);
		}
	}
}
