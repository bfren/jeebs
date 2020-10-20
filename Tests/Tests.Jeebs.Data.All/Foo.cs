using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	public class Foo : IEntity
	{
		[Ignore]
		public long Id
		{
			get => FooId;
			set => FooId = value;
		}

		[Id]
		public long FooId { get; set; }

		public string Bar0 { get; set; } = string.Empty;

		public string Bar1 { get; set; } = string.Empty;
	}
}
