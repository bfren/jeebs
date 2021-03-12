// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Option.Exceptions;
using Xunit;

namespace Jeebs.OptionExceptions.UnknownOption_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void No_Args_Creates_Default_Exception()
		{
			// Arrange

			// Act
			var result = new UnknownOptionException();

			// Assert
			Assert.Equal($"Exception of type '{typeof(UnknownOptionException)}' was thrown.", result.Message);
			Assert.Null(result.InnerException);
		}

		[Fact]
		public void With_Message_Sets_Message()
		{
			// Arrange
			var message = JeebsF.Rnd.Str;

			// Act
			var result = new UnknownOptionException(message);

			// Assert
			Assert.Equal(message, result.Message);
			Assert.Null(result.InnerException);
		}

		[Fact]
		public void With_Inner_Exception_Sets_InnerException()
		{
			// Arrange
			var inner = new Exception(JeebsF.Rnd.Str);

			// Act
			var result = new UnknownOptionException(JeebsF.Rnd.Str, inner);

			// Assert
			Assert.Same(inner, result.InnerException);
		}
	}
}
