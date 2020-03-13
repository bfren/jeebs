using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	public static partial class Query
	{
		public sealed class Posts  : Base
		{

			private readonly StringBuilder query = new StringBuilder();


			public Posts(IWpDb wpDb, IUnitOfWork? unitOfWork) : base(wpDb, unitOfWork) { }

			public IEnumerable<T> Execute<T>(Action<PostsOptions>? modifyOptions = null)
				where T : IEntity
			{
				// Get query options
				var opt = new PostsOptions();
				modifyOptions?.Invoke(opt);

				// Get UnitOfWork
				IUnitOfWork w = Start();

				// Get table names
				var p = _.Post.ToString();
				var pm = _.PostMeta.ToString();
				var tr = _.TermRelationship.ToString();
				var tx = _.TermTaxonomy.ToString();

				// SELECT columns FROM table
				Select = $"SELECT {w.Extract<T>(_.Post)} AS '{p}' FROM `{_.Post}`";

				// WHERE Id
				if (opt.Id is int postId)
				{
					Where.Add($"`{p}`.`{_.Post.PostId}` = @{nameof(postId)}");
					Parameters.Add(nameof(postId), postId);
				}

				// WHERE search
				else if (opt.SearchText is string searchText)
				{

				}
			}
		}
	}
}
