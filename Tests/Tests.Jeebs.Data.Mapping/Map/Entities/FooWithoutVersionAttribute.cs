using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping.Map_Tests
{
	public class FooWithoutVersionAttribute : IEntityWithVersion
	{
		[Id]
		public long Id { get; set; }

		public long Version { get; set; }

		public string Bar0 { get; set; } = string.Empty;

		public string Bar1 { get; set; } = string.Empty;
	}
}
