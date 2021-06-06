// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.WordPress.Data.Mapping
{
	public class FooUnwriteable : IEntity
	{
		[Ignore]
		public long Id
		{
			get => FooId;
			set => FooId = value;
		}

		[Id]
		public long FooId { get; set; }

		[Computed]
		public string Bar2 { get; set; } = string.Empty;

		[Readonly]
		public string Bar3 { get; set; } = string.Empty;
	}
}
