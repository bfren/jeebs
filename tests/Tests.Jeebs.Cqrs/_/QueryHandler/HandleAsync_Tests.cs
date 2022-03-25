// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cqrs.Internals;
using Jeebs.Cqrs.Messages;

namespace Jeebs.Cqrs.QueryHandler_Tests;

public class HandleAsync_Tests
{
	[Fact]
	public async Task Without_CancellationToken_Calls_HandleAsync_With_CancellationToken_None()
	{
		// Arrange
		var handler = Substitute.For<QueryHandler<Query, string>>();
		var query = new Query();

		// Act
		await handler.HandleAsync(query);

		// Assert
		await handler.Received().HandleAsync(query, CancellationToken.None);
	}

	[Fact]
	public async Task As_Interface_With_Incorrect_Command_Type_Returns_None_With_IncorrectCommandTypeMsg()
	{
		// Arrange
		var handler = Substitute.For<QueryHandler<Query, string>>();
		var query = new IncorrectQuery();

		// Act
		var result = await ((IQueryHandler<string>)handler).HandleAsync(query, CancellationToken.None);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<IncorrectQueryTypeMsg>(none);
		Assert.Equal(typeof(Query), msg.ExpectedType);
		Assert.Equal(typeof(IncorrectQuery), msg.ActualType);
	}

	[Fact]
	public async Task As_Interface_With_Correct_Command_Type_Calls_HandleAsync()
	{
		// Arrange
		var handler = Substitute.For<QueryHandler<Query, string>>();
		var command = new Query();

		// Act
		await ((IQueryHandler<string>)handler).HandleAsync(command, CancellationToken.None);

		// Assert
		await handler.Received().HandleAsync(command, CancellationToken.None);
	}

	public sealed record class Query : IQuery<string>;

	public sealed record class IncorrectQuery : IQuery<string>;
}
