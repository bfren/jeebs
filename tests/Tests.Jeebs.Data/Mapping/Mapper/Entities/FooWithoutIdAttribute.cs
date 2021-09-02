﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class FooWithoutIdAttribute : IWithId
	{
		[Ignore]
		public IStrongId Id
		{
			get => FooId;
			init => FooId = new(value.Value);
		}

		public FooId FooId { get; init; } = new();

		public string Bar0 { get; init; } = string.Empty;

		public string Bar1 { get; init; } = string.Empty;
	}
}
