﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public record FooTableWithoutAny : Table
	{
		public FooTableWithoutAny() : base("foo_without_any") { }
	}
}