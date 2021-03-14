// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Enumerated_Tests
{
	public partial class Operator_Tests
	{
		[Fact]
		public void Implicit_Returns_String()
		{
			// Arrange
			const string value = "foo";

			// Act
			string result = new Foo(value);

			// Assert
			Assert.Equal(value, result);
		}

		public class Foo : Enumerated
		{
			public Foo(string name) : base(name) { }
		}
	}
}
