// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class FooWithoutVersionAttribute : IEntityWithVersion
	{
		[Ignore]
		public StrongId Id
		{
			get => FooId;
			init => FooId = new(value.Value);
		}

		[Id]
		public FooId FooId { get; init; } = new();

		public long Version { get; init; }

		public string Bar0 { get; init; } = string.Empty;

		public string Bar1 { get; init; } = string.Empty;
	}
}
