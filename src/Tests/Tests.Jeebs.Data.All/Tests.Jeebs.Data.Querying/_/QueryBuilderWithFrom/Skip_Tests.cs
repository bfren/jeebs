// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests
{
	public class Skip_Tests
	{
		[Fact]
		public void Sets_Skip()
		{
			// Arrange
			var table = Substitute.For<ITable>();
			var builder = new QueryBuilderWithFrom(table);
			var value = F.Rnd.Lng;

			// Act
			var result = builder.Skip(value);

			// Assert
			var from = Assert.IsType<QueryBuilderWithFrom>(result);
			Assert.Equal(value, from.Parts.Skip);
		}
	}
}
