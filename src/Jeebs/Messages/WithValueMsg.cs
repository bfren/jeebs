// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	/// <summary>Message with value</summary>
	/// <typeparam name="TValue">Value type</typeparam>
	public abstract record WithValueMsg<TValue> : IMsg
	{
		/// <summary>Message Value</summary>
		public abstract TValue Value { get; init; }
	}
}
