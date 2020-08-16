using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Link_Tests;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class MapAsync_Tests
	{
		[Fact]
		public void IOk_Value_Input_When_IOk_Maps_To_Next_Type()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);
			static async Task<IR<string>> f(IOkV<int> r) => r.Ok<string>();

			// Act
			var next = chain.Link().MapAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IOk<string>>(next);
		}

		[Fact]
		public void IOk_Value_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			const int value = 18;
			var chain = Chain.CreateV(value);
			const string error = "Error!";
			static async Task<IR<string>> f(IOkV<int> _) => throw new Exception(error);

			// Act
			var next = chain.Link().MapAsync(f).Await();
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<string>>(next);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void IOk_Value_Input_When_IError_Returns_IError()
		{
			// Arrange
			var error = Chain<int>.Create().Error();
			static async Task<IR<int>> f(IOkV<int> _) => throw new Exception();

			// Act
			var next = error.Link().MapAsync(f).Await();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(next);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
