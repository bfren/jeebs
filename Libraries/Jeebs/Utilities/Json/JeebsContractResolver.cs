using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jeebs.Util
{
	/// <summary>
	/// Jeebs Contract Resolver
	/// </summary>
	public sealed class JeebsContractResolver : DefaultContractResolver
	{
		/// <summary>
		/// Set <see cref="NamingStrategy"/> to <see cref="CamelCaseNamingStrategy"/>
		/// </summary>
		public JeebsContractResolver()
			=> NamingStrategy = new CamelCaseNamingStrategy();

		/// <summary>
		/// If <paramref name="objectType"/> is an <see cref="Enumerated"/>, returns <see cref="EnumConverter{T}"/>
		/// <para>Otherwise, returns default contract resolved</para>
		/// </summary>
		/// <param name="objectType">Object to be converted</param>
		protected override JsonConverter? ResolveContractConverter(Type objectType)
		{
			if (objectType.IsSubclassOf(typeof(Enumerated)))
			{
				var converterType = typeof(EnumConverter<>).MakeGenericType(objectType);
				return (JsonConverter)Activator.CreateInstance(converterType);
			}

			return base.ResolveContractConverter(objectType);
		}
	}
}
