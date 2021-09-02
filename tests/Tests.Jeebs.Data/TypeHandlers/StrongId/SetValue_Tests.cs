﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.TypeHandlers.StrongId_Tests
{
	public class SetValue_Tests
	{
		[Fact]
		public void Sets_Parameter_Value_As_UInt64()
		{
			// Arrange
			var handler = new StrongIdTypeHandler<TestId>();
			var value = F.Rnd.Ulng;
			var id = new TestId { Value = value };
			var parameter = Substitute.For<IDbDataParameter>();

			// Act
			handler.SetValue(parameter, id);

			// Assert
			Assert.Equal(parameter.Value, value);
		}

		public readonly record struct TestId(ulong Value) : IStrongId;
	}
}
