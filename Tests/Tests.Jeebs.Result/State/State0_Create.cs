using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.State
{
	public class State0_Create
	{
		[Fact]
		public void Create_With_State()
		{
			// Arrange
			const int value = 18;

			// Act
			var chain = R<bool, int>.Chain(value);

			// Assert
			Assert.Equal(value, chain.State);
		}

		[Fact]
		public void Create_With_Value_And_State()
		{
			// Arrange
			const int value = 18;
			const int state = 7;

			// Act
			var chain = (IOkV<int, int>)R<int, int>.ChainV(value, state);

			// Assert
			Assert.Equal(value, chain.Val);
			Assert.Equal(state, chain.State);
		}

		[Fact]
		public async Task Create_With_State_Async()
		{
			// Arrange
			const int value = 18;

			// Act
			var chain = await R<bool, int>.ChainAsync(value);

			// Assert
			Assert.Equal(value, chain.State);
		}

		[Fact]
		public async Task Create_With_Value_And_State_Async()
		{
			// Arrange
			const int value = 18;
			const int state = 7;

			// Act
			var chain = (IOkV<int, int>)await R<int, int>.ChainVAsync(value, state);

			// Assert
			Assert.Equal(value, chain.Val);
			Assert.Equal(state, chain.State);
		}
	}
}
