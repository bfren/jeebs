using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping.MapService_Tests
{
	public class FooWithMultipleVersionAttributes : IEntityWithVersion
	{
		[Id]
		public long Id { get; set; }

		[Version]
		public long Version { get; set; }

		[Version]
		public string Bar0 { get; set; } = string.Empty;

		[Version]
		public string Bar1 { get; set; } = string.Empty;
	}
}
