using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.LinkTests
{
	public partial class Run_Tests
	{
		[Fact]
		public void IOk_Value_Input_When_IOk_Runs_Action()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);
			var sideEffect = 1;
			void f(IOkV<int> _) => sideEffect++;

			// Act
			var next = chain.Link().Run(f);

			// Assert
			Assert.Same(chain, next);
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void IOk_Value_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);
			const string error = "Error!";
			static void f(IOkV<int> _) => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.ChainExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
		}

		[Fact]
		public void IOk_Value_Input_When_IError_Returns_IError()
		{
			// Arrange
			const int value = 18;
			var error = Chain.CreateV(value).Error();
			static void f(IOkV<int> _) => throw new Exception();

			// Act
			var next = error.Link().Run(f);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
