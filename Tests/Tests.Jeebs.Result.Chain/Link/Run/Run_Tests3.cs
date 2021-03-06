using System;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class Run_Tests
	{
		[Fact]
		public void IOk_Value_Input_When_IOk_Runs_Action()
		{
			// Arrange
			var value = F.Rnd.Int;
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
			var value = F.Rnd.Int;
			var chain = Chain.CreateV(value);
			var error = F.Rnd.Str;
			void f(IOkV<int> _) => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void IOk_Value_Input_When_IError_Returns_IError()
		{
			// Arrange
			var value = F.Rnd.Int;
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
