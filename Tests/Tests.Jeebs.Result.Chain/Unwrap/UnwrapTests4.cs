using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.UnwrapTests
{
	public partial class UnwrapTests
	{
		[Fact]
		public void Not_IEnumerable_Or_Same_Type_Input_Returns_IError()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);

			// Act
			var result = chain.Link().Unwrap<string>();
			var msg = result.Messages.Get<Jm.Link.Single.NotIEnumerableMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<string>>(result);
			Assert.NotEmpty(msg);
		}
	}
}
