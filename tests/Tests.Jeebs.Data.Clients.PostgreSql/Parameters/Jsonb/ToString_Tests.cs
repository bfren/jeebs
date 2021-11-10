// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Data.Clients.PostgreSql.Parameters.Jsonb_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Value()
		{
			// Arrange
			var value = F.Rnd.Str;
			var param = new Jsonb(value);

			// Act
			var result = param.ToString();

			// Assert
			Assert.Equal(value, result);
		}
	}
}
