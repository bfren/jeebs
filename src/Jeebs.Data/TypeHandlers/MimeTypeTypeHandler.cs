// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
