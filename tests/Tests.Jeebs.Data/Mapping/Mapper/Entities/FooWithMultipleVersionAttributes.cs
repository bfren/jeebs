// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		public long Version { get; init; }

		[Version]
		public string Bar0 { get; init; } = string.Empty;

		[Version]
		public string Bar1 { get; init; } = string.Empty;
	}
}
