using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public class FooUnwriteable : IEntity
	{
		[Id]
		public long Id { get; set; }

		[Computed]
		public string Bar0 { get; set; } = string.Empty;

		[Readonly]
		public string Bar1 { get; set; } = string.Empty;
	}
}
