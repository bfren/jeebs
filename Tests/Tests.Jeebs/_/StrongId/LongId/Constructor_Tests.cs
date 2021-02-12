using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.StrongId_Tests.LongId_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_Default()
		{
			// Arrange

			// Act
			var id = new LongId();

			// Assert
			Assert.Equal(0L, id.Value);
		}

		public record LongId : Jeebs.LongId;
	}
}
