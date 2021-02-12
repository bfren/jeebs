using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public class ToOption_Tests
	{
		[Fact]
		public void OkV_Returns_Some_With_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var result = Result.OkV(value);

			// Act
			var option = result.ToOption();

			// Assert
			var some = Assert.IsType<Some<int>>(option);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void OkV_WithState_Returns_Some_With_Value()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var result = Result.OkV(value, state);

			// Act
			var option = result.ToOption();

			// Assert
			var some = Assert.IsType<Some<int>>(option);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void Ok_Returns_None()
		{
			// Arrange
			var result = Result.Ok<bool>();

			// Act
			var option = result.ToOption();

			// Assert
			Assert.IsType<None<bool>>(option);
		}

		[Fact]
		public void Ok_WithState_Returns_None()
		{
			// Arrange
			var state = F.Rnd.Int;
			var result = Result.Ok().WithState(state);

			// Act
			var option = result.ToOption();

			// Assert
			Assert.IsType<None<bool>>(option);
		}

		[Fact]
		public void Error_Returns_None()
		{
			// Arrange
			var result = Result.Error<bool>();

			// Act
			var option = result.ToOption();

			// Assert
			Assert.IsType<None<bool>>(option);
		}

		[Fact]
		public void Error_WithState_Returns_None()
		{
			// Arrange
			var state = F.Rnd.Int;
			var result = Result.Ok().WithState(state).Error();

			// Act
			var option = result.ToOption();

			// Assert
			Assert.IsType<None<bool>>(option);
		}

		[Fact]
		public void Ok_HasMessages_Returns_None_With_Last_Message()
		{
			// Arrange
			var m0 = new StringMsg(F.Rnd.Str);
			var m1 = new StringMsg(F.Rnd.Str);
			var result = Result.Ok<bool>().AddMsg(m0, m1);

			// Act
			var option = result.ToOption();

			// Assert
			var none = Assert.IsType<None<bool>>(option);
			Assert.Equal(m1, none.Reason);
		}

		[Fact]
		public void Ok_HasMessages_WithState_Returns_None_With_Last_Message()
		{
			// Arrange
			var state = F.Rnd.Int;
			var m0 = new StringMsg(F.Rnd.Str);
			var m1 = new StringMsg(F.Rnd.Str);
			var result = Result.Ok<bool>().WithState(state).AddMsg(m0, m1);

			// Act
			var option = result.ToOption();

			// Assert
			var none = Assert.IsType<None<bool>>(option);
			Assert.Equal(m1, none.Reason);
		}

		[Fact]
		public void Error_HasMessages_Returns_None_With_Last_Message()
		{
			// Arrange
			var m0 = new StringMsg(F.Rnd.Str);
			var m1 = new StringMsg(F.Rnd.Str);
			var result = Result.Ok<bool>().Error().AddMsg(m0, m1);

			// Act
			var option = result.ToOption();

			// Assert
			var none = Assert.IsType<None<bool>>(option);
			Assert.Equal(m1, none.Reason);
		}

		[Fact]
		public void Error_HasMessages_WithState_Returns_None_With_Last_Message()
		{
			// Arrange
			var state = F.Rnd.Int;
			var m0 = new StringMsg(F.Rnd.Str);
			var m1 = new StringMsg(F.Rnd.Str);
			var result = Result.Ok<bool>().WithState(state).Error().AddMsg(m0, m1);

			// Act
			var option = result.ToOption();

			// Assert
			var none = Assert.IsType<None<bool>>(option);
			Assert.Equal(m1, none.Reason);
		}

		public class StringMsg : Jm.WithValueMsg<string>
		{
			public StringMsg(string value) : base(value) { }
		}
	}
}
