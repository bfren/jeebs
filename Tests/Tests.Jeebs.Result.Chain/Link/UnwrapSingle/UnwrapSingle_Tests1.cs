using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.LinkTests
{
	public partial class UnwrapSingle_Tests
	{
		[Fact]
		public void IEnumberable_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().UnwrapSingle<int>();
			var msg = result.Messages.Get<Jm.Link.Single.MoreThanOneItemMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(result);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void List_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new[] { 1, 2 };
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().UnwrapSingle<int>();
			var msg = result.Messages.Get<Jm.Link.Single.MoreThanOneItemMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(result);
			Assert.NotEmpty(msg);
		}

		[Fact]
		public void Custom_Input_Multiple_Items_Returns_IError()
		{
			// Arrange
			var list = new CustomList(1, 2);
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().UnwrapSingle<int>();
			var msg = result.Messages.Get<Jm.Link.Single.MoreThanOneItemMsg>();

			// Assert
			Assert.IsAssignableFrom<IError<int>>(result);
			Assert.NotEmpty(msg);
		}
	}
}
