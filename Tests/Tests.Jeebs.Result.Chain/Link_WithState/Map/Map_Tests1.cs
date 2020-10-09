using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class Map_Tests
	{
		[Fact]
		public void IOk_Input_Maps_To_Next_Type()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var chain = Chain.Create(state);
			IR<int> f(IOk r) => r.WithState(state).Ok<int>();

			// Act
			var next = chain.Link().Map(f);

			// Assert
			var ok = Assert.IsAssignableFrom<IOk<int, int>>(next);
			Assert.Equal(state, ok.State);
		}

		[Fact]
		public void IOk_Input_When_IOk_Catches_Exception()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var chain = Chain.Create(state);
			var error = F.Rnd.String;
			IR<int> f(IOk _) => throw new Exception(error);

			// Act
			var next = chain.Link().Map(f);
			var msg = next.Messages.Get<Jm.Link.LinkExceptionMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void IOk_Input_When_IError_Returns_IError()
		{
			// Arrange
			var state = F.Rnd.Integer;
			var error = Chain.Create(state).Error();
			static IR<int> f(IOk _) => throw new Exception();

			// Act
			var next = error.Link().Map(f);

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(next);
			Assert.Equal(state, e.State);
			Assert.Same(error.Messages, next.Messages);
		}
	}
}
