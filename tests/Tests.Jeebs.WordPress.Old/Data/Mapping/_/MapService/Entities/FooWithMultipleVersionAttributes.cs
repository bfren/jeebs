// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;

namespace Jeebs.WordPress.Data.Mapping.Mapper_Tests
{
	public class FooWithMultipleVersionAttributes : IEntityWithVersion
	{
		[Ignore]
		public long Id
		{
			get => FooId;
			set => FooId = value;
		}

		[Id]
		public long FooId { get; set; }

		[Version]
		public long Version { get; set; }

		[Version]
		public string Bar0 { get; set; } = string.Empty;

		[Version]
		public string Bar1 { get; set; } = string.Empty;
	}
}
