// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Jeebs.Linq;
using Xunit;
using static F.OptionF;

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
			var o0 = Return(v0);
			var o1 = Return(v1);

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
			var o0 = Return(v0).AsTask;
			var o1 = Return(v1).AsTask;

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
			var o0 = Return(v0).AsTask;
			var o1 = Return(v1);
			var o2 = Return(v2).AsTask;
			var o3 = Return(v3);

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
			var o0 = Return(v0);
			var o1 = Return(v1);
			var o2 = None<int>(new InvalidIntegerMsg());

			// Act
			var result = from a in o0
						 from b in o1
						 from c in o2
						 select a + b + c;

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<InvalidIntegerMsg>(none.Reason);
		}

		[Fact]
		public async Task Async_SelectMany_With_None_Returns_None()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Return(v0).AsTask;
			var o1 = Return(v1).AsTask;
			var o2 = None<int>(new InvalidIntegerMsg()).AsTask;

			// Act
			var result = await (
				from a in o0
				from b in o1
				from c in o2
				select a + b + c
			);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<InvalidIntegerMsg>(none.Reason);
		}

		[Fact]
		public async Task Mixed_SelectMany_With_None_Returns_None()
		{
			// Arrange
			var v0 = F.Rnd.Int;
			var v1 = F.Rnd.Int;
			var o0 = Return(v0).AsTask;
			var o1 = Return(v1).AsTask;
			var o2 = None<int>(new InvalidIntegerMsg());

			// Act
			var result = await (
				from a in o0
				from b in o1
				from c in o2
				select a + b + c
			);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<InvalidIntegerMsg>(none.Reason);
		}

		public class InvalidIntegerMsg : IMsg { }
	}
}
