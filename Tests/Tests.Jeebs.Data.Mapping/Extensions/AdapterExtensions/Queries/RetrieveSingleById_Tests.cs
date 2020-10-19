using System;
using System.Collections.Generic;
using System.Text;
using Jx.Data.Mapping;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Mapping.AdapterExtensions_Tests.Adapter;

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
			var adapter = GetAdapter();
			using var svc = new MapService();
			Map<Foo>.To<FooTable>(svc);
			var table = new FooTable();

			// Act
			adapter.RetrieveSingleById<Foo>(svc);

			// Assert
			adapter.Received().RetrieveSingleById(
				__(table),
				Arg.Is<List<string>>(x => x.Count == 3 && x[0] == __(table.FooId) && x[1] == __(table.Bar0) && x[2] == __(table.Bar1)),
				__(table.FooId),
				nameof(IEntity.Id)
			);
		}
	}
}
