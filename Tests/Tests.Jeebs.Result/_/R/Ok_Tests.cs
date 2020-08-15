using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.R_Tests
{
	public class Ok_Tests
	{
		[Fact]
		public void Creates_Messages()
		{
			// Arrange

			// Act
			var r = R.Ok();

			// Assert
			Assert.NotNull(r.Messages);
		}

		[Fact]
		public void Without_Value_Type_Returns_Ok_Bool()
		{
			// Arrange

			// Act
			var r = R.Ok();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<bool>>(r);
		}

		[Fact]
		public void Without_Value_Type_With_State_Returns_Ok_Bool_Sets_State()
		{
			// Arrange
			const int state = 18;

			// Act
			var r = R.Ok(state);

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<bool>>(r);
			Assert.IsAssignableFrom<IOk<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void With_Value_Type_Returns_Ok_Type()
		{
			// Arrange

			// Act
			var r = R.Ok<int>();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<int>>(r);
		}

		[Fact]
		public void With_Value_Type_With_State_Returns_Ok_Type_Sets_State()
		{
			// Arrange
			const int state = 18;

			// Act
			var r = R.Ok<int, int>(state);

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<int>>(r);
			Assert.IsAssignableFrom<IOk<int, int>>(r);
			Assert.Equal(state, r.State);
		}
	}
}
