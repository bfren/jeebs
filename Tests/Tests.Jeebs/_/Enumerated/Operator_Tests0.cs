using System;
using System.Collections.Generic;
using System.Text;
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
			var foo = new Foo(value);

			// Act
			string result = foo;

			// Assert
			Assert.Equal(value, result);
		}

		public class Foo : Enumerated
		{
			public Foo(string name) : base(name) { }
		}
	}
}
