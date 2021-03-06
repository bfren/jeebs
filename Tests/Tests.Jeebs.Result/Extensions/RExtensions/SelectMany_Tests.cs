using Xunit;

namespace Jeebs.RExtensions_Tests
{
	public class SelectMany_Tests
	{
		[Fact]
		public void Linq_SelectMany_With_OkV_Returns_OkV()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			IR<int> r0() => Result.OkV(v0);
			IR<int> r1(int x) => Result.OkV(x + v1);

			// Act
			var result = from a in r0()
						 from b in r1(a)
						 select a + b;

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(v0 + v0 + v1, okV.Value);
		}

		[Fact]
		public void Linq_SelectMany_With_Ok_Returns_Error()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			IR<int> r0() => Result.OkV(v0);
			IR<int> r1(int x) => Result.OkV(x + v1);
			IR<int> r2 = Result.Ok<int>();

			// Act
			var result = from a in r0()
						 from b in r1(a)
						 from c in r2
						 select a + b + c;

			// Assert
			Assert.IsAssignableFrom<IError<int>>(result);
		}

		[Fact]
		public void Linq_SelectMany_With_Error_Returns_Error()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			IR<int> r0() => Result.OkV(v0);
			IR<int> r1(int x) => Result.OkV(x + v1);

			static IR<int> r2() => Result.Error<int>().AddMsg().OfType<InvalidIntegerMsg>();

			// Act
			var result = from a in r0()
						 from b in r1(a)
						 from c in r2()
						 select a + b + c;

			// Assert
			var error = Assert.IsAssignableFrom<IError<int>>(result);
			Assert.Contains(error.Messages.GetEnumerable(), m => m is InvalidIntegerMsg);
		}

		public class InvalidIntegerMsg : IMsg { }
	}
}
