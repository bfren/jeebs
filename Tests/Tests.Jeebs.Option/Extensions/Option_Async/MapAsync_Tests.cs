// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

//using System;
//using System.Threading.Tasks;
//using NSubstitute;
//using Xunit;

//namespace Jeebs.Option_Async_Tests
//{
//	public class MapAsync_Tests
//	{
//		[Fact]
//		public async Task Some_Runs_Map_Function()
//		{
//			// Arrange
//			var value = F.Rnd.Int;
//			var map = Substitute.For<Func<int, Task<string>>>();
//			var option = Option.Wrap(value);

//			// Act
//			var result = await option.MapAsync(map);

//			// Assert
//			await map.Received().Invoke(value);
//			Assert.IsType<Some<string>>(result);
//		}

//		[Fact]
//		public async Task None_Returns_None()
//		{
//			// Arrange
//			var map = Substitute.For<Func<int, Task<string>>>();
//			var option = Option.None<int>(true);

//			// Act
//			var result = await option.MapAsync(map);

//			// Assert
//			await map.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
//			Assert.IsType<None<string>>(result);
//		}

//		[Fact]
//		public async Task None_Preserves_Reason()
//		{
//			// Arrange
//			var msg = new TestMsg();
//			var map = Substitute.For<Func<int, Task<string>>>();
//			var option = Option.None<int>(msg);

//			// Act
//			var result = await option.MapAsync(map);

//			// Assert
//			await map.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
//			var none = Assert.IsType<None<string>>(result);
//			Assert.Same(msg, none.Reason);
//		}

//		public class TestMsg : IMsg { }
//	}
//}
