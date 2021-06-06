// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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

		/// <inheritdoc cref="IStrongId.IsDefault"/>
		public abstract bool IsDefault { get; }
	}
}