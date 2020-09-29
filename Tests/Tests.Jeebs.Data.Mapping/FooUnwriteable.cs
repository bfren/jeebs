using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	public class FooUnwriteable : IEntity
	{
		[Id]
		public long Id { get; set; }

		[Computed]
		public string Bar2 { get; set; } = string.Empty;

		[Readonly]
		public string Bar3 { get; set; } = string.Empty;
	}
}
