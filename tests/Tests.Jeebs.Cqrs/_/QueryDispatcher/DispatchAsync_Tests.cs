// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Jeebs.Cqrs.QueryDispatcher_Tests;

public class DispatchAsync_Tests
{
	[Fact]
	public async Task Runs_Command_HandleAsync()
	{
		// Arrange
		var handler = Substitute.For<IQueryHandler<int, string>>();
		var collection = new ServiceCollection();
		_ = collection.AddScoped(_ => handler);
		var provider = collection.BuildServiceProvider();

		IQueryDispatcher dispatcher = new QueryDispatcher(provider);
		var value = F.Rnd.Int;

		// Act
		_ = await dispatcher.DispatchAsync<int, string>(value).ConfigureAwait(false);
		_ = await dispatcher.DispatchAsync<int, string>(value, CancellationToken.None).ConfigureAwait(false);

		// Assert
		_ = await handler.Received(2).HandleAsync(value, CancellationToken.None).ConfigureAwait(false);
	}
}