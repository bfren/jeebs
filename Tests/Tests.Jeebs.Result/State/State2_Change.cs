using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result_old.State
{
	public class State2_Change
	{
		[Theory]
		[InlineData(18, 7)]
		[InlineData(18, "seven")]
		public void Change_State<T0, T1>(T0 start, T1 change)
		{
			// Arrange
			var chain = R<bool, T0>.Chain(start);

			// Act
			var changed = chain.ChangeState(change);

			// Assert
			Assert.Equal(start, chain.State);
			Assert.Equal(change, changed.State);
		}

		[Fact]
		public void Change_State_Unknown_Type_Throws_Exception()
		{
			// Arrange
			const string value = "seven";
			var chain = new OtherR();

			// Act
			Action changeState = () => chain.ChangeState(value);

			// Assert
			Assert.Throws<InvalidOperationException>(changeState);
		}

		[Theory]
		[InlineData(18, 7)]
		[InlineData(18, "seven")]
		public async Task Change_State_Async<T0, T1>(T0 start, T1 change)
		{
			// Arrange
			var chain = R<bool, T0>.ChainAsync(start);

			// Act
			var changed = chain.ChangeState(change);

			// Assert
			Assert.Equal(start, (await chain).State);
			Assert.Equal(change, (await changed).State);
		}

		[Fact]
		public async Task Change_State_Unknown_Type_Throws_Exception_Async()
		{
			// Arrange
			const string value = "seven";
			var chain = Task.FromResult<IR<object, int>>(new OtherR());

			// Act
			Func<Task> changeState = () => chain.ChangeState(value);

			// Assert
			await Assert.ThrowsAsync<InvalidOperationException>(changeState);
		}

		private class OtherR : R<object, int>
		{
			public override bool Val => false;

			public OtherR() : base(18) { }
		}
	}
}
