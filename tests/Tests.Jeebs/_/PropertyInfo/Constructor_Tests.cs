// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.PropertyInfo_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void WithInvalidPropertyName_ThrowsInvalidOperationException()
		{
			// Arrange

			// Act
			static PropertyInfo<Foo, object> result() => new(F.Rnd.Str);

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Fact]
		public void WithInvalidPropertyType_ThrowsInvalidOperationException()
		{
			// Arrange

			// Act
			static PropertyInfo<Foo, int> result() => new(nameof(Foo.Bar));

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		public sealed class Foo
		{
			public string Bar { get; set; } = string.Empty;
		}
	}
}
