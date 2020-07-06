using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.State
{
	public class State3_Remove
	{
		[Fact]
		public void Remove_State_Generic()
		{
			// Arrange
			var chain = R<bool, int>.Chain(18).Ok<TestMessage>();

			// Act
			var removed = chain.RemoveState();

			// Assert
			Assert.IsAssignableFrom<IR<bool>>(removed);
			Assert.IsAssignableFrom<IOk<bool>>(removed);
			Assert.True(removed.Messages.Contains<TestMessage>());
		}

		[Fact]
		public void Remove_State_IOk()
		{
			// Arrange
			var chain = R<bool, int>.Chain(18).Ok<TestMessage>();

			// Act
			var removed = chain.RemoveState();

			// Assert
			Assert.IsAssignableFrom<IOk<bool>>(removed);
			Assert.True(removed.Messages.Contains<TestMessage>());
		}

		[Fact]
		public void Remove_State_IOkV()
		{
			// Arrange
			const int value = 18;
			var chain = R<int, int>.Chain(7).OkV<int, TestMessage>(value);

			// Act
			var removed = chain.RemoveState();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(removed);
			Assert.Equal(value, okV.Val);
			Assert.True(removed.Messages.Contains<TestMessage>());
		}

		[Fact]
		public void Remove_State_IError()
		{
			// Arrange
			var chain = R<bool, int>.Chain(18).Error<TestMessage>();

			// Act
			var removed = chain.RemoveState();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(removed);
			Assert.True(removed.Messages.Contains<TestMessage>());
		}

		[Fact]
		public void Remove_State_Unknown_Type_Throws_Exception()
		{
			// Arrange
			var chain = new OtherR();

			// Act
			Action remove = () => chain.RemoveState();

			// Assert
			Assert.Throws<InvalidOperationException>(remove);
		}

		[Fact]
		public async Task Remove_State_Generic_Async()
		{
			// Arrange
			var chain = Task.FromResult((IR<bool, int>)R<bool, int>.Chain(18).Ok<TestMessage>());

			// Act
			var removed = await chain.RemoveState();

			// Assert
			Assert.IsAssignableFrom<IR<bool>>(removed);
			Assert.IsAssignableFrom<IOk<bool>>(removed);
			Assert.True(removed.Messages.Contains<TestMessage>());
		}

		[Fact]
		public async Task Remove_State_IOk_Async()
		{
			// Arrange
			var chain = Task.FromResult(R<bool, int>.Chain(18).Ok<TestMessage>());

			// Act
			var removed = await chain.RemoveState();

			// Assert
			Assert.IsAssignableFrom<IOk<bool>>(removed);
			Assert.True(removed.Messages.Contains<TestMessage>());
		}

		[Fact]
		public async Task Remove_State_IOkV_Async()
		{
			// Arrange
			const int value = 18;
			var chain = Task.FromResult(R<bool, int>.Chain(18).OkV<int, TestMessage>(18));

			// Act
			var removed = await chain.RemoveState();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(removed);
			Assert.Equal(value, okV.Val);
			Assert.True(okV.Messages.Contains<TestMessage>());
		}

		[Fact]
		public async Task Remove_State_IError_Async()
		{
			// Arrange
			var chain = Task.FromResult(R<bool, int>.Chain(18).Error<TestMessage>());

			// Act
			var removed = await chain.RemoveState();

			// Assert
			Assert.IsAssignableFrom<IError<bool>>(removed);
			Assert.True(removed.Messages.Contains<TestMessage>());
		}

		[Fact]
		public async Task Remove_State_Unknown_Type_Throws_Exception_Async()
		{
			// Arrange
			var chain = Task.FromResult<IR<object, int>>(new OtherR());

			// Act
			Func<Task> remove = () => chain.RemoveState();

			// Assert
			await Assert.ThrowsAsync<InvalidOperationException>(remove);
		}

		private class TestMessage : IMessage { }

		private class OtherR : R<object, int>
		{
			public override bool Val => false;

			public OtherR() : base(18) { }
		}
	}
}
