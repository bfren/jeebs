using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Jeebs.WordPress;

namespace Jm.WordPress.Query.Wrapper
{
	/// <summary>
	/// See <see cref="QueryWrapper.GetMetaDictionary{TModel}"/>
	/// </summary>
	/// <typeparam name="TModel">Model type</typeparam>
	public sealed class OnlyOneMetaDictionaryPropertySupportedMsg<TModel> : IMsg
	{
		/// <summary>
		/// Return message
		/// </summary>
		public override string ToString()
			=> $"Only one {typeof(MetaDictionary)} property is supported (model: {typeof(TModel)}).";
	}
}
