using System;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs
{
	public class RTests
	{
		[Fact]
		public void R_Ok_Creates_Messages()
		{
			// Arrange

			// Act
			var r = R.Ok();

			// Assert
			Assert.NotNull(r.Messages);
		}

		[Fact]
		public void R_Ok_Without_Value_Type_Returns_Ok_Bool()
		{
			// Arrange

			// Act
			var r = R.Ok();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<bool>>(r);
		}

		[Fact]
		public void R_Ok_Without_Value_Type_With_State_Returns_Ok_Bool_Sets_State()
		{
			// Arrange
			var state = 18;

			// Act
			var r = R.Ok(state);

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<bool>>(r);
			Assert.IsAssignableFrom<IOk<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void R_Ok_With_Value_Type_Returns_Ok_Type()
		{
			// Arrange

			// Act
			var r = R.Ok<int>();

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<int>>(r);
		}

		[Fact]
		public void R_Ok_With_Value_Type_With_State_Returns_Ok_Type_Sets_State()
		{
			// Arrange
			var state = 18;

			// Act
			var r = R.Ok<int, int>(state);

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOk<int>>(r);
			Assert.IsAssignableFrom<IOk<int, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void R_OkV_Returns_OkV()
		{
			// Arrange
			var value = 18;

			// Act
			var r = R.OkV(value);

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.Equal(value, r.Value);
		}

		[Fact]
		public void R_OkV_With_State_Returns_OkV_Sets_State()
		{
			// Arrange
			var value = 18;
			var state = 7;

			// Act
			var r = R.OkV(value, state);

			// Assert
			Assert.IsAssignableFrom<IOk>(r);
			Assert.IsAssignableFrom<IOkV<int>>(r);
			Assert.IsAssignableFrom<IOkV<int, int>>(r);
			Assert.Equal(value, r.Value);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void Error_Without_Type_Returns_Error_Of_Type()
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
		public void R_Error_Creates_Messages()
		{
			// Arrange

			// Act
			var r = R.Error();

			// Assert
			Assert.NotNull(r.Messages);
		}

		[Fact]
		public void R_Error_Without_Value_Type_Returns_Error_Bool()
		{
			// Arrange

			// Act
			var r = R.Error();

			// Assert
			Assert.IsAssignableFrom<IError>(r);
			Assert.IsAssignableFrom<IError<bool>>(r);
		}

		[Fact]
		public void R_Error_With_Value_Type_Returns_Error_Type()
		{
			// Arrange

			// Act
			var r = R.Error<int>();

			// Assert
			Assert.IsAssignableFrom<IError>(r);
			Assert.IsAssignableFrom<IError<int>>(r);
		}

		[Fact]
		public void R_Error_Without_Value_Type_With_State_Returns_Error_Sets_State()
		{
			// Arrange
			var state = 18;

			// Act
			var r = R.Error(state);

			// Assert
			Assert.IsAssignableFrom<IError>(r);
			Assert.IsAssignableFrom<IError<bool>>(r);
			Assert.IsAssignableFrom<IError<bool, int>>(r);
			Assert.Equal(state, r.State);
		}

		[Fact]
		public void R_Error_With_Value_Type_With_State_Returns_Error_Sets_State()
		{
			// Arrange
			var state = 18;

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
