// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	/// <summary>
	/// 32-bit Integer ID
	/// </summary>
	/// <param name="Value">ID Value</param>
	public abstract record IntId(int Value) : StrongId<int>(Value)
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
		public IntId() : this(Default) { }
	}
}
