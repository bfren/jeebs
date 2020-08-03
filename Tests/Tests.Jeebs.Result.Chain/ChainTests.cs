using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs
{
	public class ChainTests
	{
		[Fact]
		public void Create_Returns_Ok_Bool()
		{
			// Arrange

			// Act
			var r = Chain.Create();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<bool>>(r);
		}

		[Fact]
		public void Create_Type_Returns_Ok_Type()
		{
			// Arrange

			// Act
			var r = Chain<int>.Create();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<int>>(r);
		}

		[Fact]
		public void Create_WithState_Returns_Ok()
		{
			// Arrange
			const int state = 7;

			// Act
			var r = Chain.Create(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void Create_Type_WithState_Returns_Ok()
		{
			// Arrange
			const int state = 7;

			// Act
			var r = Chain<int>.Create(state);

			// Assert
			Assert.IsAssignableFrom<IOk<int, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void CreateV_Returns_OkV()
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
	}
}
