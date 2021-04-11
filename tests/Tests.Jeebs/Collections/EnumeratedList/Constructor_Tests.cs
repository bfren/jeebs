// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jeebs.EnumeratedList_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Create_Empty_List()
		{
			// Arrange
			var list = new EnumeratedList<Foo>();

			// Act

			// Assert
			Assert.Empty(list);
		}

		[Theory]
		[InlineData(null)]
		public void Create_Empty_List_From_Null_Strings(List<string> input)
		{
			// Arrange

			// Act
			var result = new EnumeratedList<Foo>(input);

			// Assert
			Assert.Empty(result);
		}

		[Fact]
		public void Create_List_From_Strings()
		{
			// Arrange
			var values = new[] { nameof(Foo.A), nameof(Foo.B) }.ToList();

			// Act
			var result = new EnumeratedList<Foo>(values);

			// Assert
			Assert.Equal(Foo.A.ToString(), result[0].ToString());
			Assert.Equal(Foo.B.ToString(), result[1].ToString());
		}

		public class Foo : Enumerated
		{
			public Foo(string name) : base(name) { }

			public static readonly Foo A = new("A");

			public static readonly Foo B = new("B");
		}
	}
}