// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class Run_Tests : ILink_Run
	{
		[Fact]
		public void No_Input_When_IOk_Runs_Action()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			void f() => sideEffect++;

			// Act
			var next = chain.Link().Run(f);

			// Assert
			Assert.Same(chain, next);
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void No_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			var chain = Chain.Create();
			var error = F.Rnd.Str;
			void f() => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void No_Input_When_IError_Returns_IError()
		{
			// Arrange
			var error = Chain.Create().Error();
			static void f() => throw new Exception();

			// Act
			var next = error.Link().Run(f);

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
