// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Enumerated_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void ReturnsName()
		{
			// Arrange
			var input = JeebsF.Rnd.Str;
			var test = new Foo(input);

			// Act
			var result = test.ToString();

			// Assert
			Assert.Equal(input, result);
		}

		public class Foo : Enumerated
		{
			public Foo(string name) : base(name) { }
		}
	}
}
