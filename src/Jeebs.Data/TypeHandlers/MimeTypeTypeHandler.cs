// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs.Data.TypeHandlers
{
	/// <summary>
	/// MimeType TypeHandler
	/// </summary>
	public sealed class MimeTypeTypeHandler : EnumeratedTypeHandler<MimeType>
	{
		/// <summary>
		/// Parse the MimeType value
		/// </summary>
		/// <param name="value">Database table value</param>
		/// <returns>MimeType object</returns>
		public override MimeType Parse(object value) =>
			Parse(value, MimeType.Parse, MimeType.Blank);
	}
}
