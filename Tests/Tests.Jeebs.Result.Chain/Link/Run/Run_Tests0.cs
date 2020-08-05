using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.LinkTests
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
			const string error = "Error!";
			static void f() => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
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
