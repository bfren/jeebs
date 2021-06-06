// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.Data.Querying.QueryParameters_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_Json()
		{
			// Arrange
			var param = new QueryParameters();
			var p0 = F.Rnd.Str;
			var p1 = F.Rnd.Int;
			param.TryAdd(new { p0, p1 });

			// Act
			var result = param.ToString();

			// Assert
			Assert.Equal($"{{\"{nameof(p0)}\":\"{p0}\",\"{nameof(p1)}\":{p1}}}", result);
		}
	}
}
