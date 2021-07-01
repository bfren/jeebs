// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class FooWithMultipleVersionAttributes : IWithVersion
	{
		[Ignore]
		public StrongId Id
		{
			get => FooId;
			init => FooId = new(value.Value);
		}

		[Id]
		public FooId FooId { get; init; } = new();

		[Version]
		public ulong Version { get; init; }

		[Version]
		public string Bar0 { get; init; } = string.Empty;

		[Version]
		public string Bar1 { get; init; } = string.Empty;
	}
}
