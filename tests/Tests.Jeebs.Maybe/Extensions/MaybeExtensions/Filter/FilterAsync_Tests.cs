// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.MaybeExtensions_Tests;

public class FilterAsync_Tests : Jeebs_Tests.FilterAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg()
	{
		var syncPredicate = Substitute.For<Func<int, bool>>();
		var asyncPredicate = Substitute.For<Func<int, Task<bool>>>();

		await Test00(mbe => mbe.AsTask.FilterAsync(syncPredicate)).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.FilterAsync(asyncPredicate)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01((mbe, predicate) => mbe.AsTask.FilterAsync(x => predicate(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, predicate) => mbe.AsTask.FilterAsync(predicate)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		await Test02((mbe, predicate) => mbe.AsTask.FilterAsync(x => predicate(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test02((mbe, predicate) => mbe.AsTask.FilterAsync(predicate)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
	{
		await Test03((mbe, predicate) => mbe.AsTask.FilterAsync(x => predicate(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test03((mbe, predicate) => mbe.AsTask.FilterAsync(predicate)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_When_None_Returns_None_With_Original_Reason()
	{
		await Test04((mbe, predicate) => mbe.AsTask.FilterAsync(x => predicate(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test04((mbe, predicate) => mbe.AsTask.FilterAsync(predicate)).ConfigureAwait(false);
	}
}
