// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MaybeF;

namespace Jeebs.Mvc.Result_Tests;

public class Reason_Tests
{
	[Fact]
	public void Maybe_Is_Some__Returns_SuccessMsg()
	{
		// Arrange
		var jsonResult = new Result<Guid>(Rnd.Guid);

		// Act
		var result = jsonResult.Reason;

		// Assert
		Assert.Equal(nameof(Result.Success), result);
	}

	[Fact]
	public void Maybe_Is_None__Returns_Reason()
	{
		// Arrange
		var msg = Substitute.For<IMsg>();
		var maybe = F.None<long>(msg);
		var jsonResult = new Result<long>(maybe);

		// Act
		var result = jsonResult.Reason;

		// Assert
		Assert.Same(msg.ToString(), result);
	}
}
