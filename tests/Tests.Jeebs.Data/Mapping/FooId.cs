// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Data.Mapping
{
	public record FooId(long Value) : StrongId(Value)
	{
		public FooId() : this(0) { }
	}
}
