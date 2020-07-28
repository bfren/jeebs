using System;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class ChainTests
	{
		[Fact]
		public void R_Ok_Returns_Ok()
		{
			// Arrange

			// Act
			var r = Chain.Create();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<bool>>(r);
		}

		[Fact]
		public async Task R_ChainAsync_Returns_Ok()
		{
			// Arrange

			// Act
			var r = await Chain.CreateAsync();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<bool>>(r);
		}

		[Fact]
		public void R_Chain_WithState_Returns_Ok()
		{
			// Arrange
			const int state = 18;

			// Act
			var r = Chain.Create(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public async Task R_ChainAsync_WithState_Returns_Ok()
		{
			// Arrange
			const int state = 18;

			// Act
			var r = await Chain.CreateAsync(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void R_ChainV_Returns_OkV()
		{
			// Arrange
			const int value = 18;

			// Act
			var r = Chain.CreateV(value);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.Equal(value, r.Value);
		}

		[Fact]
		public async Task R_ChainVAsync_Returns_OkV()
		{
			// Arrange
			const int value = 18;

			// Act
			var r = await Chain.CreateVAsync(value);

			// Assert
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.Equal(value, r.Value);
		}

		[Fact]
		public void R_ChainV_WithState_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const int state = 7;

			// Act
			var r = Chain.CreateV(value, state);

			// Assert
			Assert.IsAssignableFrom<IOkV<int, int>>(r);
			Assert.Equal(value, r.Value);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public async Task R_ChainVAsync_WithState_Returns_OkV()
		{
			// Arrange
			const int value = 18;
			const int state = 7;

			// Act
			var r = await Chain.CreateVAsync(value, state);

			// Assert
			Assert.IsAssignableFrom<IOkV<int, int>>(r);
			Assert.Equal(value, r.Value);
			Assert.Equal(state, r.State);
		}
	}
}
