// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping
{
	public record class FooWithIgnored : IWithId
	{
		[Ignore]
		public StrongId Id
		{
			get => FooId;
			init => FooId = new(value.Value);
		}

		[Id]
		public FooId FooId { get; init; } = new();

		[Ignore]
		public string Bar0 { get; init; } = string.Empty;

		public string Bar1 { get; init; } = string.Empty;
	}
}
