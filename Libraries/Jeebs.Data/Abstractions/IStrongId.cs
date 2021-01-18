using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs.Data
{
	/// <summary>
	/// Represents a strong-typed ID
	/// </summary>
	/// <typeparam name="T">ID value type</typeparam>
	public interface IStrongId<T>
	{
		/// <summary>
		/// ID Value
		/// </summary>
		T Value { get; }
	}

	public abstract record StrongId<T> : IStrongId<T>
	{
		public T Value { get; init; }

		public StrongId(T value, T defaultValue) =>
			Value = value ?? defaultValue;
	}

	public record PostId(long Value) : StrongId<long>(Value, 0);
}
