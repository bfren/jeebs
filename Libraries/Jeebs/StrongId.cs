using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Strongly Typed ID record type
	/// </summary>
	/// <typeparam name="T">ID Value Type</typeparam>
	public abstract record StrongId<T>(T Value) : IStrongId<T>
		where T : notnull;

	/// <summary>
	/// 32-bit Integer ID
	/// </summary>
	public abstract record IntId(int Value) : StrongId<int>(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public IntId() : this(0) { }
	}

	/// <summary>
	/// 64-bit Integer ID
	/// </summary>
	public abstract record LongId(long Value) : StrongId<long>(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public LongId() : this(0) { }
	}

	/// <summary>
	/// Guid ID
	/// </summary>
	public abstract record GuidId(Guid Value) : StrongId<Guid>(Value)
	{
		/// <summary>
		/// Create with default value
		/// </summary>
		public GuidId() : this(Guid.Empty) { }
	}
}
