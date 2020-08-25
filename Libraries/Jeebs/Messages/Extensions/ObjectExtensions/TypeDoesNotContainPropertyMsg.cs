using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jm.Extensions.ObjectExtensions
{
	/// <summary>
	/// See <see cref="Jeebs.Reflection.ObjectExtensions"/>
	/// </summary>
	public sealed class TypeDoesNotContainPropertyMsg : WithValueMsg<(Type type, string property)>
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="type">Object type</param>
		/// <param name="property">Property name</param>
		public TypeDoesNotContainPropertyMsg(Type type, string property) : base((type, property)) { }
	}
}
