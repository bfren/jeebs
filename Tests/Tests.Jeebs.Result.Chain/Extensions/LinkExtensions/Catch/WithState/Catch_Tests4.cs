// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.LinkExtensions_Tests.WithState
{
	public partial class Catch_Tests
	{
		[Fact]
		public void No_Handler_With_Generic_Custom_ExceptionMsg_Adds_Msg()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			static void throwGeneric() => throw new Exception();
			static void throwOther() => throw new DivideByZeroException();

			// Act
			chain.Link().Catch().AllUnhandled().With<CustomExceptionMsg>().Run(throwGeneric);
			chain.Link().Catch().AllUnhandled().With<CustomExceptionMsg>().Run(throwOther);
			var msg = chain.Messages.Get<CustomExceptionMsg>();

			// Assert
			Assert.Equal(2, msg.Count);
		}

		[Fact]
		public void No_Handler_With_Specific_Custom_ExceptionMsg_Adds_Msg()
		{
			// Arrange
			var state = F.Rnd.Int;
			var chain = Chain.Create(state);
			static void throwGeneric() => throw new Exception();
			static void throwOther() => throw new DivideByZeroException();

			// Act
			chain.Link().Catch().Unhandled<DivideByZeroException>().With<CustomExceptionMsg>().Run(throwGeneric);
			chain.Link().Catch().Unhandled<DivideByZeroException>().With<CustomExceptionMsg>().Run(throwOther);
			chain.Link().Catch().Unhandled<DivideByZeroException>().With<CustomExceptionMsg>().Run(throwOther);
			var msg = chain.Messages.Get<CustomExceptionMsg>();

			// Assert
			Assert.Equal(2, msg.Count);
		}

		public class CustomExceptionMsg : Jm.ExceptionMsg { }
	}
}
