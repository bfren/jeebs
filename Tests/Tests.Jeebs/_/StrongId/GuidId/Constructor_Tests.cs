using System;
using Xunit;

namespace Jeebs.StrongId_Tests.GuidId_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Default()
		{
			// Arrange

			// Act
			var id = new GuidId();

			// Assert
			Assert.Equal(Guid.Empty, id.Value);
		}

		public record GuidId : Jeebs.GuidId;
	}
}
