// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Data.Entities;

namespace Jeebs.Data.Mapping
{
	public class FooUnwriteable : IWithId
	{
		[Ignore]
		public StrongId Id
		{
			get => FooId;
			init => FooId = new(value.Value);
		}

		[Id]
		public FooId FooId { get; init; } = new();

		[Computed]
		public string Bar2 { get; init; } = string.Empty;

		[Readonly]
		public string Bar3 { get; init; } = string.Empty;
	}
}
