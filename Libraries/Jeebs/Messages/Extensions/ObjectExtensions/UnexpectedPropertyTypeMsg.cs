using System;
using System.Collections.Generic;
using System.Text;

namespace Jm.Extensions.ObjectExtensions
{
	/// <summary>
	/// See <see cref="Jeebs.Reflection.ObjectExtensions"/>
	/// </summary>
	public sealed class UnexpectedPropertyTypeMsg : WithValueMsg<(Type objectType, string property, Type expectedPropertyType)>
	{
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="objectType">Object type</param>
		/// <param name="property">Property name</param>
		/// <param name="expectedPropertyType">Expected property type</param>
		public UnexpectedPropertyTypeMsg(Type objectType, string property, Type expectedPropertyType) : base((objectType, property, expectedPropertyType)) { }
	}
}
