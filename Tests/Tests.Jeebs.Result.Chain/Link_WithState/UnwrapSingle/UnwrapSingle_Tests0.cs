using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Jeebs.Link_Tests.WithState
{
	public partial class UnwrapSingle_Tests : ILink_UnwrapSingle_WithState
	{
		[Fact]
		public void IEnumerable_Input_One_Item_Returns_Single()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var list = new[] { value };
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().UnwrapSingle<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(result);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}

		[Fact]
		public void List_Input_One_Item_Returns_Single()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var list = new[] { value }.ToList();
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().UnwrapSingle<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(result);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}

		[Fact]
		public void Custom_Input_One_Item_Returns_Single()
		{
			// Arrange
			var value = F.Rnd.Int;
			var state = F.Rnd.Int;
			var list = new CustomList(value);
			var chain = Chain.CreateV(list, state);

			// Act
			var result = chain.Link().UnwrapSingle<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int, int>>(result);
			Assert.Equal(value, okV.Value);
			Assert.Equal(state, okV.State);
		}

		public class CustomList : List<int>
		{
			public CustomList(params int[] values) : base(values) { }
		}
	}
}
