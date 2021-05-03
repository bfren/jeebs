// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.WordPress.Data;
using Jeebs.WordPress.Data.Entities;
using static F.OptionF;

namespace F.WordPressF.DataF
{
	public static partial class QueryTermsF
	{
		/// <summary>
		/// Execute Terms query
		/// </summary>
		/// <typeparam name="TTerm">Term Entity type</typeparam>
		/// <typeparam name="TModel">Return Model type</typeparam>
		/// <param name="db">IWpDb</param>
		/// <param name="opt">Function to return query options</param>
		public static Task<Option<IEnumerable<TModel>>> ExecuteAsync<TTerm, TModel>(
			IWpDb db,
			Query.GetTermsOptions<TTerm> opt
		)
			where TTerm : WpTermEntity
			where TModel : IWithId =>
			Return(
				() => opt(new Query.TermsOptions<TTerm>(db)),
				e => new Msg.ErrorGettingQueryTermsOptionsMsg(e)
			)
			.Bind(
				x => x.GetParts<TModel>()
			)
			.BindAsync(
				x => db.Query.QueryAsync<TModel>(x)
			);

		/// <summary>Messages</summary>
		public static partial class Msg
		{
			/// <summary>Unable to get terms query</summary>
			/// <param name="Exception">Exception object</param>
			public sealed record ErrorGettingQueryTermsOptionsMsg(Exception Exception) : ExceptionMsg(Exception) { }
		}
	}
}
