// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Data;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.TypeHandlers.Guid_Tests;

public class SetValue_Tests
{
	[Fact]
	public void Sets_Parameter_Value_As_Guid()
	{
		// Arrange
		var handler = new GuidTypeHandler();
		var value = F.Rnd.Guid;
		var parameter = Substitute.For<IDbDataParameter>();

		// Act
		handler.SetValue(parameter, value);

		// Assert
		Assert.Equal(parameter.Value, value);
	}
}
