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
		public void Incorrect_Subtype_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().Single<string>();
			var msg = result.Messages.Get<Jm.Link.Single.IncorrectTypeMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<string>>(result);
			Assert.NotEmpty(msg);
		}
	}
}
