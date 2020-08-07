using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.LinkTests;
using Xunit;

namespace Jeebs.LinkTests.Async
{
	public partial class WrapAsync_Tests
	{
		[Fact]
		public void Func_Input_When_IOk_Wraps_Value()
		{
			// Arrange
			const int value = 18;
			static async Task<int> f() => value;
			var r = Chain.Create();

			// Act
			var next = r.Link().WrapAsync(f).Await();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(next);
			Assert.Equal(value, okV.Value);
		}

		[Fact]
		public void Func_Input_When_IOk_Catches_Exception_Returns_IError()
		{
			// Arrange
			const string error = "Ooops!";
			static async Task<int> f() => throw new Exception(error);
			var r = Chain.Create();

			// Act
			var next = r.Link().WrapAsync(f).Await();
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void Func_Input_When_IError_Returns_IError()
		{
			// Arrange
			static async Task<int> f() => throw new Exception();
			var r = Chain.Create().Error();

			// Act
			var next = r.Link().WrapAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
		}
	}
}
