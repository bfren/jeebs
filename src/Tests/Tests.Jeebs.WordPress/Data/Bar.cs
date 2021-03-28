// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Mapping;

namespace Jeebs.WordPress.Data
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
