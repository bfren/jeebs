using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.LinkTests
{
	public class Map_Tests : ILink_Map
	{
		[Fact]
		public void When_IOk_Runs_Func()
		{
			// Arrange
			var chain = Chain.Create();
			static IR<int> f(IOk r) => r.Ok<int>();

			// Act
			var next = chain.Link().Map(f);

			// Assert
			Assert.IsAssignableFrom<IOk<int>>(next);
		}

		[Fact]
		public void When_IOkV_Runs_Func()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.Create().OkV(value);
			static IR<string> f(IOkV<int> r) => r.Ok<string>();

			// Act
			var next = chain.Link().Map(f);

			// Assert
			Assert.IsAssignableFrom<IOk<string>>(next);
		}

		[Fact]
		public void When_IOk_Catches_Exception_Returns_IError()
		{
			// Arrange
			var chain = Chain.Create();
			const string error = "Error!";
			static IR<int> f(IOk _) => throw new Exception(error);

			// Act
			var next = chain.Link().Map(f);
			var msg = next.Messages.Get<Jm.ChainExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
		}

		[Fact]
		public void When_IOkV_Catches_Exception_Returns_IError()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.Create().OkV(value);
			const string error = "Error!";
			static IR<string> f(IOkV<int> _) => throw new Exception(error);

			// Act
			var next = chain.Link().Map(f);
			var msg = next.Messages.Get<Jm.ChainExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<string>>(next);
			Assert.NotEmpty(msg);
			Assert.Equal($"{typeof(Exception)}: {error}", msg.Single().ToString());
		}

		[Fact]
		public void Expecting_IOk_But_IError_Returns_IError()
		{
			// Arrange
			var error = Chain.Create().Error();
			static IR<int> f(IOk _) => throw new Exception();

			// Act
			var next = error.Link().Map(f);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.Same(error.Messages, next.Messages);
		}

		[Fact]
		public void Expecting_IOkV_But_IError_Returns_IError()
		{
			// Arrange
			var error = Chain<int>.Create().Error();
			static IR<int> f(IOkV<int> _) => throw new Exception();

			// Act
			var next = error.Link().Map(f);

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
