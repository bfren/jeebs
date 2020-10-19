using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping
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
