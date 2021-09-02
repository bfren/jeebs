// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class FooWithMultipleVersionAttributes : IWithVersion
	{
		[Ignore]
		public IStrongId Id
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
