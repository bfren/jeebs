// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class SelectMany_Tests
	{
		[Fact]
		public void SelectMany_With_Some_Returns_Some()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Option.Wrap(v0);
			var o1 = Option.Wrap(v1);

			// Act
			var result = from a in o0
						 from b in o1
						 select a + b;

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(v0 + v1, some.Value);
		}

		[Fact]
		public async Task Async_SelectMany_With_Some_Returns_Some()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Task.FromResult(Option.Wrap(v0));
			var o1 = Task.FromResult(Option.Wrap(v1));

			// Act
			var result = await (
				from a in o0
				from b in o1
				select a + b
			);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(v0 + v1, some.Value);
		}

		[Fact]
		public async Task Mixed_SelectMany_With_Some_Returns_Some()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var v2 = F.Rnd.Int;
			var v3 = F.Rnd.Int;
			var o0 = Task.FromResult(Option.Wrap(v0));
			var o1 = Option.Wrap(v1);
			var o2 = Task.FromResult(Option.Wrap(v2));
			var o3 = Option.Wrap(v3);

			// Act
			var result = await (
				from a in o0
				from b in o1
				from c in o2
				from d in o3
				select a + b + c + d
			);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(v0 + v1 + v2 + v3, some.Value);
		}

		[Fact]
		public void SelectMany_With_None_Returns_None()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Option.Wrap(v0);
			var o1 = Option.Wrap(v1);
			var o2 = Option.None<int>(new InvalidIntegerMsg());

			// Act
			var result = from a in o0
						 from b in o1
						 from c in o2
						 select a + b + c;

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.True(none.Reason is InvalidIntegerMsg);
		}

		[Fact]
		public async Task Async_SelectMany_With_None_Returns_None()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Task.FromResult(Option.Wrap(v0));
			var o1 = Task.FromResult(Option.Wrap(v1));
			var o2 = Task.FromResult(Option.None<int>(new InvalidIntegerMsg()).AsOption);

			// Act
			var result = await (
				from a in o0
				from b in o1
				from c in o2
				select a + b + c
			);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.True(none.Reason is InvalidIntegerMsg);
		}

		[Fact]
		public async Task Mixed_SelectMany_With_None_Returns_None()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Task.FromResult(Option.Wrap(v0));
			var o1 = Task.FromResult(Option.Wrap(v1));
			var o2 = Option.None<int>(new InvalidIntegerMsg());

			// Act
			var result = await (
				from a in o0
				from b in o1
				from c in o2
				select a + b + c
			);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.True(none.Reason is InvalidIntegerMsg);
		}

		public class InvalidIntegerMsg : IMsg { }
	}
}
