// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public class Constructor_Tests
	{
		[Fact]
		public void With_Exception_Handler_Adds_Exception_Handler()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			var handler = Substitute.For<Func<Exception, IMsg>>();
			var msg = Substitute.For<IMsg>();
			handler.Invoke(Arg.Any<Exception>()).Returns(_ => msg);

			static void throwException() => throw new Exception();

			// Act
			var link = chain.Link(handler).Run(throwException);

			// Assert
			var error = Assert.IsAssignableFrom<IError<bool, int>>(link);
			Assert.Contains(link.Messages.GetEnumerable(), m => m == msg);
		}
	}
}
