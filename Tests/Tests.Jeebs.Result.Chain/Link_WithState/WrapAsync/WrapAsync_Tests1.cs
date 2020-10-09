using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Link_Tests;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class WrapAsync_Tests
	{
		[Fact]
		public void Func_Input_When_IOk_Wraps_Value()
		{
			// Arrange
			var value = F.Rnd.Integer;
			var state = F.Rnd.Integer;
			async Task<int> f() => value;
			var r = Chain.Create(state);

			// Act
			var next = r.Link().WrapAsync(f).Await();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(next);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}

		[Fact]
		public void Func_Input_When_IOk_Catches_Exception_Returns_IError()
		{
			// Arrange
			const string error = "Ooops!";
			static async Task<int> f() => throw new Exception(error);
			var state = F.Rnd.Integer;
			var r = Chain.Create(state);

			// Act
			var next = r.Link().WrapAsync(f).Await();
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void Func_Input_When_IError_Returns_IError()
		{
			// Arrange
			static async Task<int> f() => throw new Exception();
			var state = F.Rnd.Integer;
			var r = Chain.Create(state).Error();

			// Act
			var next = r.Link().WrapAsync(f).Await();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
		}
	}
}
