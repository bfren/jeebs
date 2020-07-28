using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result
{
	public class R_Error_Tests
	{
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
		public void Error_Same_Type_Returns_Error_Of_Type()
		{
			// Arrange
			var r = R.Ok();

			// Act
			var next = r.Error<bool>();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<bool>>(next);
		}

		[Fact]
		public void Error_Different_Type_Returns_Error_Of_Different_Type()
		{
			// Arrange
			var r = R.Ok();

			// Act
			var next = r.Error<int>();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<int>>(next);
		}

		[Fact]
		public void Error_Different_Type_Keeps_Messages()
		{
			// Arrange
			var m0 = new Jm.WithIntMsg(18);
			var m1 = new Jm.WithStringMsg("July");
			var r = R.Ok().AddMsg(m0, m1);

			// Act
			var next = r.Error<int>();

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<Jm.WithIntMsg>());
			Assert.True(next.Messages.Contains<Jm.WithStringMsg>());
		}
	}
}
