// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
			var p0 = JeebsF.Rnd.Str;
			var p1 = JeebsF.Rnd.Int;
			param.TryAdd(new { p0, p1 });

			// Act
			var result = param.ToString();

			// Assert
			Assert.Equal($"{{\"{nameof(p0)}\":\"{p0}\",\"{nameof(p1)}\":{p1}}}", result);
		}
	}
}
