using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jeebs.Link_Tests
{
	public partial class UnwrapSingle_Tests : ILink_UnwrapSingle
	{
		[Fact]
		public void IEnumerable_Input_One_Item_Returns_Single()
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { value };
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().UnwrapSingle<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, okV.Value);
		}

		[Fact]
		public void List_Input_One_Item_Returns_Single()
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { value }.ToList();
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().UnwrapSingle<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, okV.Value);
		}

		[Fact]
		public void Custom_Input_One_Item_Returns_Single()
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new CustomList(value);
			var chain = Chain.CreateV(list);

			// Act
			var result = chain.Link().UnwrapSingle<int>();

			// Assert
			var okV = Assert.IsAssignableFrom<IOkV<int>>(result);
			Assert.Equal(value, okV.Value);
		}

		public class CustomList : List<int>
		{
			public CustomList(params int[] values) : base(values) { }
		}
	}
}
