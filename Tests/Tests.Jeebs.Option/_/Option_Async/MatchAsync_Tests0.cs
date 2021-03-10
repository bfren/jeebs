// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

//using System;
//using System.Threading.Tasks;
//using NSubstitute;
//using Xunit;

//namespace Jeebs.Option_Async_Tests
//{
//	public partial class MatchAsync_Tests
//	{
//		[Fact]
//		public async Task Async_Some_Runs_Some()
//		{
//			// Arrange
//			var value = F.Rnd.Int;
//			var some = Substitute.For<Func<int, Task<int>>>();
//			var none = Substitute.For<Func<int>>();
//			var option = Option.Wrap(value);

//			// Act
//			await option.MatchAsync(some, none);

//			// Assert
//			await some.Received().Invoke(value);
//		}

//		[Fact]
//		public async Task Async_Some_Runs_None()
//		{
//			// Arrange
//			var some = Substitute.For<Func<int, Task<int>>>();
//			var none = Substitute.For<Func<int>>();
//			var option = Option.None<int>(true);

//			// Act
//			var result = await option.MatchAsync(some, none);

//			// Assert
//			none.Received().Invoke();
//		}
//	}
//}
