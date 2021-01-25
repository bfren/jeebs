using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <summary>
	/// Represents a strong-typed ID
	/// </summary>
	/// <typeparam name="T">ID value type</typeparam>
	public interface IStrongId<T>
		where T : notnull
	{
		/// <summary>
		/// ID Value
		/// </summary>
		T Value { get; }
	}
}
