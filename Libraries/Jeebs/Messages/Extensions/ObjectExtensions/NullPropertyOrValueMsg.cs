using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jm.Extensions.ObjectExtensions
{
	/// <summary>
	/// See <see cref="Jeebs.Reflection.ObjectExtensions"/>
	/// </summary>
	public sealed class NullPropertyOrValueMsg : WithValueMsg<(Type type, string property)>
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="type">Object type</param>
		/// <param name="property">Property name</param>
		public NullPropertyOrValueMsg(Type type, string property) : base((type, property)) { }
	}
}
