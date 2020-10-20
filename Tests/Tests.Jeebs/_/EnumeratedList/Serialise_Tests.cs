using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.EnumeratedList_Tests
{
	public class Serialise_Tests
	{
		[Fact]
		public void Empty_List_Returns_Empty_Json()
		{
			// Arrange
			var list = new EnumeratedList<Foo>();

			// Act
			var result = list.Serialise();

			// Assert
			Assert.Equal(F.JsonF.Empty, result);
		}

		[Fact]
		public void List_Returns_Json()
		{
			// Arrange
			var list = new EnumeratedList<Foo> { { Foo.A }, { Foo.B } };
			const string? json = "[\"Item A\",\"Item B\"]";

			// Act
			var result = list.Serialise();

			// Assert
			Assert.Equal(json, result);
		}

		public class Foo : Enumerated
		{
			public Foo(string name) : base(name) { }

			public static Foo A = new Foo("Item A");

			public static Foo B = new Foo("Item B");
		}
	}
}
