// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs.Id
{
	/// <summary>
	/// Strongly Typed ID record type
	/// </summary>
	/// <typeparam name="T">ID Value Type</typeparam>
	/// <param name="Value">ID Value</param>
	public abstract record StrongId<T>(T Value) : IStrongId<T>
	{
		/// <summary>
		/// Return the value as a string
		/// </summary>
		public string ValueStr =>
			Value?.ToString() ?? "Unknown ID";

		/// <summary>
		/// Whether or not this is the default value
		/// </summary>
		public abstract bool IsDefault { get; }
	}
}