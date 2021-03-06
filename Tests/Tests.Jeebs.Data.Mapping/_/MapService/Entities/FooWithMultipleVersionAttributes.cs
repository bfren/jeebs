using System;

namespace Jeebs.Data.Mapping.MapService_Tests
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
