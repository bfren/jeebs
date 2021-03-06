using System;
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
			Action result = () => new PropertyInfo<Foo, object>(F.Rnd.Str);

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		[Fact]
		public void WithInvalidPropertyType_ThrowsInvalidOperationException()
		{
			// Arrange

			// Act
			Action result = () => new PropertyInfo<Foo, int>(nameof(Foo.Bar));

			// Assert
			Assert.Throws<InvalidOperationException>(result);
		}

		public sealed class Foo
		{
			public string Bar { get; set; } = string.Empty;
		}
	}
}
