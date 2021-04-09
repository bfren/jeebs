// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping.Mapper_Tests
{
	public class FooWithMultipleIdAttributes : IWithId
	{
		[Ignore]
		public StrongId Id
		{
			get => FooId;
			init => FooId = new(value.Value);
		}

		[Id]
		public FooId FooId { get; init; } = new();

		[Id]
		public string Bar0 { get; init; } = string.Empty;

		[Id]
		public string Bar1 { get; init; } = string.Empty;
	}
}
