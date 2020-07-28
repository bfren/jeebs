using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result2
{
	public class R_WithState_Error_Tests
	{
		[Fact]
		public void Error_Without_Type_Returns_Error_Of_Type()
		{
			// Arrange
			var state = 18;
			var r = R.Ok(state);

			// Act
			var next = r.Error();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.IsAssignableFrom<IError<bool, int>>(next);
		}

		[Fact]
		public void Error_Same_Type_Returns_Error_Of_Type()
		{
			// Arrange
			var state = 18;
			var r = R.Ok(state);

			// Act
			var next = r.Error<bool>();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.IsAssignableFrom<IError<bool, int>>(next);
		}

		[Fact]
		public void Error_Different_Type_Returns_Error_Of_Different_Type()
		{
			// Arrange
			var state = 18;
			var r = R.Ok(state);

			// Act
			var next = r.Error<int>();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.IsAssignableFrom<IError<int, int>>(next);
		}

		[Fact]
		public void Error_Different_Type_Keeps_Messages()
		{
			// Arrange
			var state = 18;
			var m0 = new Jm.WithIntMsg(18);
			var m1 = new Jm.WithStringMsg("July");
			var r = R.Ok(state).AddMsg().Messages(m0, m1);

			// Act
			var next = r.Error<int>();

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.WithIntMsg>());
			Assert.True(next.Messages.Contains<Jm.WithStringMsg>());
		}
	}
}
