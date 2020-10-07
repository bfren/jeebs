using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data.Mapping.MapService_Tests
{
	public class FooWithMultipleIdAttributes : IEntity
	{
		[Id]
		public long Id { get; set; }

		[Id]
		public string Bar0 { get; set; } = string.Empty;

		[Id]
		public string Bar1 { get; set; } = string.Empty;
	}
}
