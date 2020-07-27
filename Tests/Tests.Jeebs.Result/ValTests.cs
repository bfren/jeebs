using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result_old
{
	public class ValTests
	{
		[Fact]
		public void Ok_Val_Is_True()
		{
			// Arrange
			var chain = R.Chain;

			// Act

			// Assert
			Assert.True(chain.Val);
		}

		[Fact]
		public void Error_Val_Is_False()
		{
			// Arrange
			var chain = R.Chain.Error();

			// Act

			// Assert
			Assert.False(chain.Val);
		}
	}
}
