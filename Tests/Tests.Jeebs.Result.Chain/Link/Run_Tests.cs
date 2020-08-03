using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.LinkTests
{
	public class Run_Tests : ILink_Run
	{
		[Fact]
		public void When_IOk_Runs_Action_Without_Input_Returns_Original_Result()
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
		public void When_IOk_Runs_Action_With_IOk_Input_Returns_Original_Result()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			void f(IOk _) => sideEffect++;

			// Act
			var next = chain.Link().Run(f);

			// Assert
			Assert.Same(chain, next);
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void When_IOk_Runs_Action_With_IOk_Value_Input_Returns_Original_Result()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			void f(IOk<bool> _) => sideEffect++;

			// Act
			var next = chain.Link().Run(f);

			// Assert
			Assert.Same(chain, next);
			Assert.Equal(2, sideEffect);
		}

		[Fact]
		public void When_IOk_Runs_Action_With_IOkV_Input_Returns_Original_Result()
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
		public void When_IOk_Runs_Action_Without_Input_Catches_Exception_Returns_Error()
		{
			// Arrange
			var chain = Chain.Create();
			const string error = "Error!";
			static void f() => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.ChainExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
		}

		[Fact]
		public void When_IOk_Runs_Action_With_IOk_Input_Catches_Exception_Returns_Error()
		{
			// Arrange
			var chain = Chain.Create();
			const string error = "Error!";
			static void f(IOk _) => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.ChainExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
		}

		[Fact]
		public void When_IOk_Runs_Action_With_IOk_Value_Input_Catches_Exception_Returns_Error()
		{
			// Arrange
			var chain = Chain.Create();
			const string error = "Error!";
			static void f(IOk<bool> _) => throw new Exception(error);

			// Act
			var next = chain.Link().Run(f);
			var msg = next.Messages.Get<Jm.ChainExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
		}

		[Fact]
		public void When_IOk_Runs_Action_With_IOkV_Input_Catches_Exception_Returns_Error()
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
		public void Expecting_IOk_But_IError_Returns_IError()
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

		[Fact]
		public void Expecting_IOk_With_Value_But_IError_Returns_IError()
		{
			// Arrange
			var error = Chain.Create().Error();
			static void f(IOk<bool> _) => throw new Exception();

			// Act
			var next = error.Link().Run(f);

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.Same(error.Messages, next.Messages);
		}

		[Fact]
		public void Expecting_IOkV_But_IError_Returns_IError()
		{
			// Arrange
			var error = Chain.Create().Error();
			static void f(IOk<bool> _) => throw new Exception();

			// Act
			var next = error.Link().Run(f);

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
