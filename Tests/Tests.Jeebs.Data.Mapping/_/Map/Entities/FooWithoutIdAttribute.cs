using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping.Map_Tests
{
	public class FooWithoutIdAttribute : IEntity
	{
		public long Id { get; set; }

		public string Bar0 { get; set; } = string.Empty;

		public string Bar1 { get; set; } = string.Empty;
	}
}
