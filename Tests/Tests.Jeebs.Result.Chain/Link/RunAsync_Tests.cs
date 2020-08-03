using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.LinkTests;
using Xunit;

namespace Jeebs.LinkAsyncTests
{
	public class RunAsync_Tests : ILink_Run
	{
		[Fact]
		public void When_IOk_Runs_Action_Without_Input_Returns_Original_Result()
		{
			// Arrange
			var chain = Chain.Create();
			var sideEffect = 1;
			async Task f() => sideEffect++;

			// Act
			var next = chain.Link().RunAsync(f).Await();

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
			async Task f(IOk _) => sideEffect++;

			// Act
			var next = chain.Link().RunAsync(f).Await();

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
			async Task f(IOk<bool> _) => sideEffect++;

			// Act
			var next = chain.Link().RunAsync(f).Await();

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
			async Task f(IOkV<int> _) => sideEffect++;

			// Act
			var next = chain.Link().RunAsync(f).Await();

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
			static async Task f() => throw new Exception(error);

			// Act
			var next = chain.Link().RunAsync(f).Await();
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
			static async Task f(IOk _) => throw new Exception(error);

			// Act
			var next = chain.Link().RunAsync(f).Await();
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
			static async Task f(IOk<bool> _) => throw new Exception(error);

			// Act
			var next = chain.Link().RunAsync(f).Await();
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
			static async Task f(IOkV<int> _) => throw new Exception(error);

			// Act
			var next = chain.Link().RunAsync(f).Await();
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
			static async Task f() => throw new Exception();

			// Act
			var next = error.Link().RunAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.Same(error.Messages, next.Messages);
		}

		[Fact]
		public void Expecting_IOk_With_Value_But_IError_Returns_IError()
		{
			// Arrange
			var error = Chain.Create().Error();
			static async Task f(IOk<bool> _) => throw new Exception();

			// Act
			var next = error.Link().RunAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.Same(error.Messages, next.Messages);
		}

		[Fact]
		public void Expecting_IOkV_But_IError_Returns_IError()
		{
			// Arrange
			var error = Chain.Create().Error();
			static async Task f(IOk<bool> _) => throw new Exception();

			// Act
			var next = error.Link().RunAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
