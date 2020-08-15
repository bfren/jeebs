using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.R_Tests
{
	public class Error_Tests
	{
		[Fact]
		public void Without_Type_Returns_Error_Of_Type()
		{
			// Arrange
			var r = R.Ok();

			// Act
			var next = r.Error();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<bool>>(next);
		}
		[Fact]
		public void Creates_Messages()
		{
			// Arrange

			// Act
			var r = R.Error();

			// Assert
			Assert.NotNull(r.Messages);
		}

		[Fact]
		public void Without_Value_Type_Returns_Error_Bool()
		{
			// Arrange

			// Act
			var r = R.Error();

			// Assert
			Assert.IsAssignableFrom<IError>(r);
			Assert.IsAssignableFrom<IError<bool>>(r);
		}

		[Fact]
		public void With_Value_Type_Returns_Error_Type()
		{
			// Arrange

			// Act
			var r = R.Error<int>();

			// Assert
			Assert.IsAssignableFrom<IError>(r);
			Assert.IsAssignableFrom<IError<int>>(r);
		}

		[Fact]
		public void Without_Value_Type_With_State_Returns_Error_Sets_State()
		{
			// Arrange
			const int state = 18;

			// Act
			var r = R.Error(state);

			// Assert
			Assert.IsAssignableFrom<IError>(r);
			Assert.IsAssignableFrom<IError<bool>>(r);
			Assert.IsAssignableFrom<IError<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void With_Value_Type_With_State_Returns_Error_Sets_State()
		{
			// Arrange
			const int state = 18;

			// Act
			var r = R.Error<int, int>(state);

			// Assert
			Assert.IsAssignableFrom<IError>(r);
			Assert.IsAssignableFrom<IError<int>>(r);
			Assert.IsAssignableFrom<IError<int, int>>(r);
			Assert.Equal(state, r.State);
		}
	}
}
