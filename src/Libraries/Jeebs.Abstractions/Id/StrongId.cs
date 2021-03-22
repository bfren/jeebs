// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// Strongly Typed ID record type
	/// </summary>
	/// <param name="Value">ID Value</param>
	public abstract record StrongId(long Value) : IStrongId
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public StrongId() : this(0) { }

		/// <inheritdoc cref="IStrongId.IsDefault"/>
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
