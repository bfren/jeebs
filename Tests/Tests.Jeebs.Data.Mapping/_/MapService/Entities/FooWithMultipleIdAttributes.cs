using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping.MapService_Tests
{
	public class FooWithMultipleIdAttributes : IEntity
	{
		[Ignore]
		public long Id
		{
			get => FooId;
			set => FooId = value;
		}

		[Id]
		public long FooId { get; set; }

		[Id]
		public string Bar0 { get; set; } = string.Empty;

		[Id]
		public string Bar1 { get; set; } = string.Empty;
	}
}
