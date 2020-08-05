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
		public void IEnumerable_Input_Incorrect_Subtype_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 }.ToList();
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().Unwrap<string>();
			var msg = result.Messages.Get<Jm.Link.Single.IncorrectTypeMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<string>>(result);
			Assert.NotEmpty(msg);
		}
	}
}
