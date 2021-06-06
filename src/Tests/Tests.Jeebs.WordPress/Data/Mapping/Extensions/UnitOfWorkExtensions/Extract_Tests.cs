// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using NSubstitute;
using Xunit;

namespace Jeebs.WordPress.Data.Mapping.UnitOfWorkExtensions_Tests
{
	public class Extract_Tests
	{
		[Fact]
		public void Calls_Extract_With_Correct_Arguments()
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();

			var adapter = Substitute.For<IAdapter>();
			adapter.ColumnSeparator
				.Returns('|');
			adapter.Escape(Arg.Any<string>(), Arg.Any<string>())
				.ReturnsForAnyArgs(x => x.ArgAt<string>(0));

			unitOfWork.Adapter.Returns(adapter);
			var table = new FooTable();

			// Act
			var result = unitOfWork.Extract<Foo>(table);

			// Assert
			Assert.Equal($"{table.FooId}| {table.Bar0}| {table.Bar1}", result);
		}
	}
}
