using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Mapping.UnitOfWorkExtensions_Tests
{
	public class Extract_Tests
	{
		[Fact]
		public void Calls_Extract_With_Correct_Arguments()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();
			var table = new FooTable();

			// Act
			UnitOfWorkExtensions.Extract<Foo>(unitOfWork, table);

			// Assert

		}
	}
}
