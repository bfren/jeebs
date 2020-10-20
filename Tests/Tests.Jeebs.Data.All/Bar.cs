using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Mapping;

namespace Jeebs.Data
{
	public class Bar : IEntity
	{
		[Ignore]
		public long Id
		{
			get => BarId;
			set => BarId = value;
		}

		[Id]
		public long BarId { get; set; }
	}
}
