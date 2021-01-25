using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.StrongId_Tests.IntId_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Default()
		{
			// Arrange

			// Act
			var id = new IntId();

			// Assert
			Assert.Equal(0, id.Value);
		}

		public record IntId : Jeebs.IntId;
	}
}
