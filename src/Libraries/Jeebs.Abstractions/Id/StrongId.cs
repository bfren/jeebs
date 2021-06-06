// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	/// <inheritdoc cref="IStrongId"/>
	/// <param name="Value">ID Value</param>
	public abstract record StrongId(long Value) : IStrongId
	{
		/// <inheritdoc/>
		public bool IsDefault =>
			Value == 0;

		#region Operators

		/// <summary>
		/// Compare a StrongId type with a plain long
		/// </summary>
		/// <param name="l">StrongId</param>
		/// <param name="r">64-bit integer</param>
		public static bool operator ==(StrongId l, long r) =>
			l.Value == r;

		/// <summary>
		/// Compare a StrongId type with a plain long
		/// </summary>
		/// <param name="l">StrongId</param>
		/// <param name="r">64-bit integer</param>
		public static bool operator !=(StrongId l, long r) =>
			l.Value != r;

		/// <summary>
		/// Compare a StrongId type with a plain long
		/// </summary>
		/// <param name="l">StrongId</param>
		/// <param name="r">64-bit integer</param>
		public static bool operator ==(long l, StrongId r) =>
			l == r.Value;

		/// <summary>
		/// Compare a StrongId type with a plain long
		/// </summary>
		/// <param name="l">StrongId</param>
		/// <param name="r">64-bit integer</param>
		public static bool operator !=(long l, StrongId r) =>
			l != r.Value;

		#endregion
	}
}
