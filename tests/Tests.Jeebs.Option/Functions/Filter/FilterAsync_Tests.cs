// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests;

public class FilterAsync_Tests : Jeebs_Tests.FilterAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
	{
		var predicate = Substitute.For<Func<int, Task<bool>>>();

		await Test00(opt => FilterAsync(opt, predicate));
		await Test00(opt => FilterAsync(opt.AsTask, predicate));
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01((opt, predicate) => FilterAsync(opt, predicate));
		await Test01((opt, predicate) => FilterAsync(opt.AsTask, predicate));
	}

	[Fact]
	public override async Task Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		await Test02((opt, predicate) => FilterAsync(opt, predicate));
		await Test02((opt, predicate) => FilterAsync(opt.AsTask, predicate));
	}

	[Fact]
	public override async Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
	{
		await Test03((opt, predicate) => FilterAsync(opt, predicate));
		await Test03((opt, predicate) => FilterAsync(opt.AsTask, predicate));
	}

	[Fact]
	public override async Task Test04_When_None_Returns_None_With_Original_Reason()
	{
		await Test04((opt, predicate) => FilterAsync(opt, predicate));
		await Test04((opt, predicate) => FilterAsync(opt.AsTask, predicate));
	}
}
