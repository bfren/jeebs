using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.SingleTests.WithState
{
	public partial class SingleTests
	{
		[Fact]
		public void IEnumberable_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			const int state = 7;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().Single<int>();
			var msg = result.Messages.Get<Jm.ExpectingSingleReceivingMoreMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void List_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			const int state = 7;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().Single<int>();
			var msg = result.Messages.Get<Jm.ExpectingSingleReceivingMoreMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void Custom_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new CustomList(1, 2);
			const int state = 7;
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().Single<int>();
			var msg = result.Messages.Get<Jm.ExpectingSingleReceivingMoreMsg>();

			// Assert
			var e = Assert.IsAssignableFrom<IError<int, int>>(result);
			Assert.Equal(state, e.State);
			Assert.NotEmpty(msg);
		}
	}
}
