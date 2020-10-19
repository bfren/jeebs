using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping.MapService_Tests
{
	public class FooWithoutIdAttribute : IEntity
	{
		[Ignore]
		public long Id
		{
			get => FooId;
			set => FooId = value;
		}

		public long FooId { get; set; }

		public string Bar0 { get; set; } = string.Empty;

		public string Bar1 { get; set; } = string.Empty;
	}
}
