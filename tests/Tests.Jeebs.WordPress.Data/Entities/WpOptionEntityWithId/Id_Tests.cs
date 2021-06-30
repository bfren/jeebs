﻿// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Entities.WpOptionEntityWithId_Tests
{
	public class Id_Tests : Id_Tests<Id_Tests.Test, WpOptionId>
	{
		[Fact]
		public override void Test00_Id_Returns_Database_Id() =>
			Test00(id => new() { OptionId = id });

		public sealed record Test : WpOptionEntityWithId;
	}
}
