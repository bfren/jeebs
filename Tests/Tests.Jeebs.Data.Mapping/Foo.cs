using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
{
	public class Foo : IEntity
	{
		[Id]
		public long Id { get; set; }

		public string Bar0 { get; set; } = string.Empty;

		public string Bar1 { get; set; } = string.Empty;
	}
}
