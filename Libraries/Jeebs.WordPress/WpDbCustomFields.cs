using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;
using Jeebs.Data.Clients.MySql;
using Jeebs.WordPress.Entities;

namespace Jeebs.WordPress
{
	/// <summary>
	/// WordPress Database Instance
	/// It should be registered with a DI container as a singleton to avoid the (expensive) table creation
	/// each time the instance is required
	/// </summary>
	/// <typeparam name="Tc">WpCommentEntity</typeparam>
	/// <typeparam name="Tcm">WpCommentMetaEntity</typeparam>
	/// <typeparam name="Tl">WpLinkEntity</typeparam>
	/// <typeparam name="To">WpOptionEntity</typeparam>
	/// <typeparam name="Tp">WpPostEntity</typeparam>
	/// <typeparam name="Tpm">WpPostMetaEntity</typeparam>
	/// <typeparam name="Tt">WpTermEntity</typeparam>
	/// <typeparam name="Ttm">WpTermMetaEntity</typeparam>
	/// <typeparam name="Ttr">WpTermRelationshipEntity</typeparam>
	/// <typeparam name="Ttt">WpTermTaxonomyEntity</typeparam>
	/// <typeparam name="Tu">WpUserEntity</typeparam>
	/// <typeparam name="Tum">WpUserMetaEntity</typeparam>
	public sealed partial class WpDb<Tc, Tcm, Tl, To, Tp, Tpm, Tt, Ttm, Ttr, Ttt, Tu, Tum> : Db<MySqlDbClient>
		where Tc : WpCommentEntity
		where Tcm : WpCommentMetaEntity
		where Tl : WpLinkEntity
		where To : WpOptionEntity
		where Tp : WpPostEntity
		where Tpm : WpPostMetaEntity
		where Tt : WpTermEntity
		where Ttm : WpTermMetaEntity
		where Ttr : WpTermRelationshipEntity
		where Ttt : WpTermTaxonomyEntity
		where Tu : WpUserEntity
		where Tum : WpUserMetaEntity
	{
		/// <summary>
		/// Hydrate a Text CustomField
		/// </summary>
		/// <param name="customField">TextCustomField</param>
		/// <param name="meta">MetaDictionary</param>
		public void Hydrate(ref TextCustomField customField, in MetaDictionary meta)
		{
			customField.Value = meta.ContainsKey(customField.Key) ? meta[customField.Key] : $"'{customField.Key}' not found.";
		}
	}
}
