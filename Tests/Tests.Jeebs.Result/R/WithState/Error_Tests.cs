// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.R_Tests.WithState
{
	public class Error_Tests : IR_Error
	{
		[Fact]
		public void Without_Type_Returns_Error_Of_Type()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Result.Ok(state);

			// Act
			var next = r.Error();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.IsAssignableFrom<IError<bool, int>>(next);
		}

		[Fact]
		public void Same_Type_Returns_Error_Of_Type()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Result.Ok(state);

			// Act
			var next = r.Error<bool>();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<bool>>(next);
			Assert.IsAssignableFrom<IError<bool, int>>(next);
		}

		[Fact]
		public void Different_Type_Returns_Error_Of_Different_Type()
		{
			// Arrange
			var state = F.Rnd.Int;
			var r = Result.Ok(state);

			// Act
			var next = r.Error<int>();

			// Assert
			Assert.IsAssignableFrom<IError>(next);
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.IsAssignableFrom<IError<int, int>>(next);
		}

		[Fact]
		public void Different_Type_Keeps_Messages()
		{
			// Arrange
			var state = F.Rnd.Int;
			var m0 = new IntMsg(F.Rnd.Int);
			var m1 = new StringMsg(F.Rnd.Str);
			var r = Result.Ok(state).AddMsg(m0, m1);

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
