using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.R_Tests
{
	public class Error_Tests : IR_Error
	{
		[Fact]
		public void Without_Type_Returns_Error_Of_Type()
		{
			// Arrange
			var r = Result.Ok();

			// Act
			var next = r.Error();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<bool>>(next);
		}

		[Fact]
		public void Same_Type_Returns_Error_Of_Type()
		{
			// Arrange
			var r = Result.Ok();

			// Act
			var next = r.Error<bool>();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<bool>>(next);
		}

		[Fact]
		public void Different_Type_Returns_Error_Of_Different_Type()
		{
			// Arrange
			var r = Result.Ok();

			// Act
			var next = r.Error<int>();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<int>>(next);
		}

		[Fact]
		public void Different_Type_Keeps_Messages()
		{
			// Arrange
			var m0 = new IntMsg(18);
			var m1 = new StringMsg("July");
			var r = Result.Ok().AddMsg(m0, m1);

			// Act
			var next = r.Error<int>();

			// Assert
			Assert.Equal(2, next.Messages.Count);
			Assert.True(next.Messages.Contains<IntMsg>());
			Assert.True(next.Messages.Contains<StringMsg>());
		}

		public class IntMsg : Jm.WithValueMsg<int>
		{
			public IntMsg(int value) : base(value) { }
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
