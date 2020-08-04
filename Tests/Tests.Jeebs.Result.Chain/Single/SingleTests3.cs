using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.SingleTests
{
	public partial class SingleTests
	{
		[Fact]
		public void Not_IEnumerable_Input_Returns_IError()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);

			// Act
			var result = chain.Link().Single<int>();
			var msg = result.Messages.Get<Jm.NotIEnumerableMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(result);
			Assert.NotEmpty(msg);
		}
	}
}
