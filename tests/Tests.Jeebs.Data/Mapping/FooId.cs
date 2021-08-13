// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data.Mapping
{
	public record class FooId(ulong Value) : StrongId(Value)
	{
		public FooId() : this(0) { }
	}
}
