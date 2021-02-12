using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace Jeebs.Link_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void With_Exception_Handler_Adds_Exception_Handler()
		{
			// Arrange
			var chain = Chain.Create();
			var handler = Substitute.For<Func<Exception, IMsg>>();
			var msg = Substitute.For<IMsg>();
			handler.Invoke(Arg.Any<Exception>()).Returns(_ => msg);

			static void throwException() => throw new Exception();

			// Act
			var link = chain.Link(handler).Run(throwException);

			// Assert
			var error = Assert.IsAssignableFrom<IError>(link);
			Assert.Contains(link.Messages.GetEnumerable(), m => m == msg);
		}
	}
}
