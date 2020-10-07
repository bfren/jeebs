﻿using System;
using System.Collections.Generic;
using System.Text;
using Jx.Data.Mapping;
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
			using var svc = new MapService();
			var adapter = Substitute.For<IAdapter>();

			// Act
			void action() => adapter.DeleteSingle<Foo>(svc);

			// Assert
			var ex = Assert.Throws<UnmappedEntityException>(action);
			Assert.Equal(string.Format(UnmappedEntityException.Format, typeof(Foo)), ex.Message);
		}

		[Fact]
		public void Calls_DeleteSingle_With_Correct_Arguments()
		{
			// Arrange
			var adapter = Substitute.For<IAdapter>();
			adapter.Escape(Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.Arg<string>());

			using var svc = new MapService();
			var foo0 = new FooTable();
			Map<Foo>.To<FooTable>(svc);
			var foo1 = new FooWithVersionTable();
			Map<FooWithVersion>.To<FooWithVersionTable>(svc);

			// Act
			adapter.DeleteSingle<Foo>(svc);
			adapter.DeleteSingle<FooWithVersion>(svc);

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
