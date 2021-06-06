// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Id
{
	/// <summary>
	/// 64-bit Integer ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public abstract record LongId(long Value) : StrongId<long>(Value)
	{
		/// <summary>
		/// Default value
		/// </summary>
		public const int Default = 0;

		/// <inheritdoc cref="IStrongId.IsDefault"/>
		public override bool IsDefault =>
			Value == Default;

		/// <summary>
		/// Create with default value
		/// </summary>
		public LongId() : this(Default) { }
	}
}