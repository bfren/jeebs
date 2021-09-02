// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests.Enumerable;

public abstract class FilterMap_Tests
{
	public abstract void Test00_Maps_And_Returns_Only_Some_From_List();

	protected static void Test00(Func<IEnumerable<Option<int>>, Func<int, string>, IEnumerable<string>> act)
	{
		// Arrange
		var v0 = F.Rnd.Int;
		var v1 = F.Rnd.Int;
		var o0 = Some(v0);
		var o1 = Some(v1);
		var o2 = Create.None<int>();
		var o3 = Create.None<int>();
		var list = new[] { o0, o1, o2, o3 };
		var map = Substitute.For<Func<int, string>>();
		map.Invoke(Arg.Any<int>()).Returns(x => x.ArgAt<int>(0).ToString());

		// Act
		var result = act(list, map);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(v0.ToString(), x),
			x => Assert.Equal(v1.ToString(), x)
		);
		map.ReceivedWithAnyArgs(2).Invoke(Arg.Any<int>());
	}

	public abstract void Test01_Maps_And_Returns_Only_Some_From_List();

	protected static void Test01(Func<IEnumerable<Option<int>>, Func<int, Option<string>>, IEnumerable<Option<string>>> act)
	{
		// Arrange
		var v0 = F.Rnd.Int;
		var v1 = F.Rnd.Int;
		var o0 = Some(v0);
		var o1 = Some(v1);
		var o2 = Create.None<int>();
		var o3 = Create.None<int>();
		var list = new[] { o0, o1, o2, o3 };
		var map = Substitute.For<Func<int, Option<string>>>();
		map.Invoke(Arg.Any<int>()).Returns(x => Some(x.ArgAt<int>(0).ToString()));

		// Act
		var result = act(list, map);

		// Assert
		Assert.Collection(result,
			x =>
			{
				var s0 = x.AssertSome();
				Assert.Equal(v0.ToString(), s0);
			},
			x =>
			{
				var s1 = x.AssertSome();
				Assert.Equal(v1.ToString(), s1);
			}
		);
		map.ReceivedWithAnyArgs(2).Invoke(Arg.Any<int>());
	}

	public abstract void Test02_Returns_Matching_Some_From_List();

	protected static void Test02(Func<IEnumerable<Option<int>>, Func<int, string>, Func<int, bool>, IEnumerable<string>> act)
	{
		// Arrange
		var v0 = F.Rnd.Int;
		var v1 = F.Rnd.Int;
		var o0 = Some(v0);
		var o1 = Some(v1);
		var o2 = Create.None<int>();
		var o3 = Create.None<int>();
		var list = new[] { o0, o1, o2, o3 };

		var map = Substitute.For<Func<int, string>>();
		map.Invoke(Arg.Any<int>()).Returns(x => x.ArgAt<int>(0).ToString());

		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(v1).Returns(true);

		// Act
		var r0 = act(list, map, predicate);

		// Assert
		Assert.Collection(r0,
			x => Assert.Equal(v1.ToString(), x)
		);
		map.ReceivedWithAnyArgs(1).Invoke(Arg.Any<int>());
	}

	public abstract void Test03_Returns_Matching_Some_From_List();

	protected static void Test03(Func<IEnumerable<Option<int>>, Func<int, Option<string>>, Func<int, bool>, IEnumerable<Option<string>>> act)
	{
		// Arrange
		var v0 = F.Rnd.Int;
		var v1 = F.Rnd.Int;
		var o0 = Some(v0);
		var o1 = Some(v1);
		var o2 = Create.None<int>();
		var o3 = Create.None<int>();
		var list = new[] { o0, o1, o2, o3 };

		var map = Substitute.For<Func<int, Option<string>>>();
		map.Invoke(Arg.Any<int>()).Returns(x => Some(x.ArgAt<int>(0).ToString()));

		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(v1).Returns(true);

		// Act
		var r0 = act(list, map, predicate);

		// Assert
		Assert.Collection(r0,
			x => Assert.Equal(v1.ToString(), x)
		);
		map.ReceivedWithAnyArgs(1).Invoke(Arg.Any<int>());
	}
}
