using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	public static partial class Query
	{
		public sealed class Posts : IDisposable
		{
			private readonly IWpDb wpDb;

			private readonly IUnitOfWork? unitOfWork;

			private bool disposeUnitOfWork;

			private readonly StringBuilder query = new StringBuilder();

			public Dictionary<string,object> Parameters { get; }

			public Posts(in IWpDb wpDb, in IUnitOfWork? unitOfWork)
			{
				this.wpDb = wpDb;
				this.unitOfWork = unitOfWork;
				Parameters = new Dictionary<string, object>();
			}

			public IEnumerable<T> Execute<T>(Action<PostsOptions>? modifyOptions = null)
				where T : IEntity
			{
				// Get query options
				var opt = new PostsOptions();
				modifyOptions?.Invoke(opt);

				// Get UnitOfWork
				IUnitOfWork w;
				if (unitOfWork == null)
				{
					w = wpDb.UnitOfWork;
					disposeUnitOfWork = true;
				}
				else
				{
					w = unitOfWork;
				}

				// Get table names
				var pt = wpDb.Post.ToString();
				var pm = wpDb.PostMeta.ToString();
				var tr = wpDb.TermRelationship.ToString();
				var tx = wpDb.TermTaxonomy.ToString();

				// SELECT columns FROM table
				query.Append($"SELECT {w.Extract<T>(wpDb.Post)} AS '{pt}' FROM `{wpDb.Post}` ");

				// WHERE
				var where = new List<string>();

				// WHERE Id
				if (opt.Id is int postId)
				{
					where.Add($"{wpDb.Post.PostId} = @{nameof(postId)}");
					Parameters.Add(nameof(postId), postId);
					goto appendWhere;
				}

				// WHERE search
				if (opt.SearchText is string searchText)
				{

				}

				// Add WHERE to query
				appendWhere:
				query.Append($" WHERE {string.Join(" AND ", where)}");
			}

			public void Dispose()
			{
				if (disposeUnitOfWork)
				{
					unitOfWork?.Dispose();
				}
			}
		}
	}
}
