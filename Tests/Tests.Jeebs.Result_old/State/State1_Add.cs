using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Jeebs_old.State
{
	public class State1_Add
	{
		[Fact]
		public void Add_State()
		{
			// Arrange
			const int value = 18;
			var chain = R.Chain;

			// Act
			var withState = chain.AddState(value);

			// Assert
			Assert.Equal(value, withState.State);
		}

		[Fact]
		public void Add_State_Unknown_Type_Throws_Exception()
		{
			// Arrange
			const int value = 18;
			var chain = new OtherR();

			// Act
			Action addState = () => chain.AddState(value);

			// Assert
			Assert.Throws<InvalidOperationException>(addState);
		}

		[Fact]
		public async Task Add_State_Async()
		{
			// Arrange
			const int value = 18;
			var chain = R.ChainAsync;

			// Act
			var withState = await chain.AddState(value);

			// Assert
			Assert.Equal(value, withState.State);
		}

		[Fact]
		public async Task Add_State_Unknown_Type_Throws_Exception_Async()
		{
			// Arrange
			const int value = 18;
			var chain = Task.FromResult<IR<object>>(new OtherR());

			// Act
			Func<Task> addState = () => chain.AddState(value);

			// Assert
			await Assert.ThrowsAsync<InvalidOperationException>(addState);
		}

		private class OtherR : R<object>
		{
			public override bool Val => false;
		}
	}
}
