// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	/// <summary>
	/// Represents a strongly-typed ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public abstract record StrongId(ulong Value)
	{
		/// <summary>
		/// Returns true if the current value is the default (i.e. unset) value
		/// </summary>
		public bool IsDefault =>
			Value == 0;

		/// <summary>
		/// Override to return <see cref="Value"/>
		/// </summary>
		public sealed override string ToString() =>
			Value.ToString();

		#region Operators

		/// <summary>
		/// Compare a StrongId type with a plain long
		/// </summary>
		/// <param name="l">StrongId</param>
		/// <param name="r">64-bit integer</param>
		public static bool operator ==(StrongId l, ulong r) =>
			l.Value == r;

		/// <summary>
		/// Compare a StrongId type with a plain long
		/// </summary>
		/// <param name="l">StrongId</param>
		/// <param name="r">64-bit integer</param>
		public static bool operator !=(StrongId l, ulong r) =>
			l.Value != r;

		/// <summary>
		/// Compare a StrongId type with a plain long
		/// </summary>
		/// <param name="l">StrongId</param>
		/// <param name="r">64-bit integer</param>
		public static bool operator ==(ulong l, StrongId r) =>
			l == r.Value;

		/// <summary>
		/// Compare a StrongId type with a plain long
		/// </summary>
		/// <param name="l">StrongId</param>
		/// <param name="r">64-bit integer</param>
		public static bool operator !=(ulong l, StrongId r) =>
			l != r.Value;

		#endregion
	}
}
