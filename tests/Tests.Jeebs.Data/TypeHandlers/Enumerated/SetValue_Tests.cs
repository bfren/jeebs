// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;

namespace Jeebs.Data.TypeHandlers.Enumerated_Tests;

public class SetValue_Tests
{
	[Fact]
	public void Sets_Value_To_Enumerated_Name()
	{
		// Arrange
		var handler = Substitute.ForPartsOf<EnumeratedTypeHandler<EnumeratedTest>>();
		var parameter = Substitute.For<IDbDataParameter>();

		// Act
		handler.SetValue(parameter, EnumeratedTest.Bar);

		// Assert
		parameter.Received().Value = nameof(EnumeratedTest.Bar);
	}

	public sealed record class EnumeratedTest : Enumerated
	{
		public EnumeratedTest(string name) : base(name) { }

		public static readonly EnumeratedTest Foo = new(nameof(Foo));

		public static readonly EnumeratedTest Bar = new(nameof(Bar));
	}
}
