// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Linq;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class UnwrapSingle_Tests
	{
		[Fact]
		public void IEnumerable_Input_Incorrect_Subtype_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 }.ToList();
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().UnwrapSingle<string>();
			var msg = result.Messages.Get<Jm.Link.Unwrap.IncorrectTypeMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<string>>(result);
			Assert.NotEmpty(msg);
		}
	}
}
