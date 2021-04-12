// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.TypeHandlers.Enumerated_Tests
{
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

		public sealed class EnumeratedTest : Enumerated
		{
			public EnumeratedTest(string name) : base(name) { }

			public readonly static EnumeratedTest Foo = new(nameof(Foo));

			public readonly static EnumeratedTest Bar = new(nameof(Bar));
		}
	}
}
