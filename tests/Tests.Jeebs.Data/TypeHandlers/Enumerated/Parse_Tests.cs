// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.TypeHandlers.Enumerated_Tests;

public class Parse_Tests
{
	[Fact]
	public void Valid_Value_Returns_Parse_Func_Result()
	{
		// Arrange
		var value = nameof(EnumeratedTest.Foo);
		var handler = Substitute.ForPartsOf<EnumeratedTypeHandler<EnumeratedTest>>();
		var parse = Substitute.For<Func<string, EnumeratedTest>>();

		// Act
		_ = handler.ParseTest(value, parse, EnumeratedTest.Bar);

		// Assert
		_ = parse.Received().Invoke(value);
	}

	[Theory]
	[InlineData(null)]
	public void Null_Value_Returns_IfNull(object input)
	{
		// Arrange
		var handler = Substitute.ForPartsOf<EnumeratedTypeHandler<EnumeratedTest>>();
		var parse = Substitute.For<Func<string, EnumeratedTest>>();

		// Act
		var result = handler.ParseTest(input, parse, EnumeratedTest.Bar);

		// Assert
		Assert.Same(EnumeratedTest.Bar, result);
	}

	public sealed record class EnumeratedTest : Enumerated
	{
		public EnumeratedTest(string name) : base(name) { }

		public readonly static EnumeratedTest Foo = new(nameof(Foo));

		public readonly static EnumeratedTest Bar = new(nameof(Bar));
	}
}
