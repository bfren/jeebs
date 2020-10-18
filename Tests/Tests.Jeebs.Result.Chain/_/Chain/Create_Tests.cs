using System;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Chain_Tests
{
	public class Create_Tests
	{
		[Fact]
		public void Returns_Ok_Bool()
		{
			// Arrange

			// Act
			var r = Chain.Create();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<bool>>(r);
		}

		[Fact]
		public void With_Type_Returns_Ok_Of_Type()
		{
			// Arrange

			// Act
			var r = Chain<int>.Create();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<int>>(r);
		}

		[Fact]
		public void With_State_Returns_Ok_With_State()
		{
			// Arrange
			var state = F.Rnd.Int;

			// Act
			var r = Chain.Create(state);

			// Assert
			Assert.IsAssignableFrom<IOk<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void With_Type_With_State_Returns_Ok_Of_Type_With_State()
		{
			// Arrange
			var state = F.Rnd.Int;

			// Act
			var r = Chain<int>.Create(state);

			// Assert
			Assert.IsAssignableFrom<IOk<int, int>>(r);
			Assert.Equal(state, r.State);
		}
	}
}
