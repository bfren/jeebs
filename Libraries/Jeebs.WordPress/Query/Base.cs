using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data;

namespace Jeebs.WordPress
{
	public static partial class Query
	{
		/// <summary>
		/// Provides base options and methods for Queries
		/// </summary>
		public abstract class Base : IDisposable
		{
			/// <summary>
			/// IWpDb
			/// </summary>
			protected IWpDb _ { get; }

			/// <summary>
			/// IUnitOfWork
			/// </summary>
			private readonly IUnitOfWork? unitOfWork;

			/// <summary>
			/// Will be set to true by <see cref="Start"/> if a UnitOfWork is not passed to the query on creation
			/// </summary>
			private bool disposeUnitOfWork;

			/// <summary>
			/// Query SELECT
			/// </summary>
			protected string Select { get; }

			/// <summary>
			/// Query JOIN
			/// </summary>
			protected List<string> Join { get; }

			/// <summary>
			/// Query WHERE
			/// </summary>
			protected List<string> Where { get; }

			/// <summary>
			/// Query Parameters
			/// </summary>
			public Dictionary<string, object> Parameters { get; }

			protected Base(IWpDb wpDb, IUnitOfWork? unitOfWork)
			{
				_ = wpDb;
				this.unitOfWork = unitOfWork;

				Select = string.Empty;
				Join = new List<string>();
				Where = new List<string>();
				Parameters = new Dictionary<string, object>();
			}

			/// <summary>
			/// Start the query, using the passed UnitOfWork, or creating one just for this query
			/// </summary>
			protected IUnitOfWork Start()
			{
				if (unitOfWork == null)
				{
					disposeUnitOfWork = true;
					return _.UnitOfWork;
				}
				else
				{
					return unitOfWork;
				}
			}

			/// <summary>
			/// Return the query SQL
			/// </summary>
			protected string End()
			{

			}

			/// <summary>
			/// Dispose of the UnitOfWork if we created one just for this query
			/// Otherwise, the UnitOfWork was passed to the query, so will be disposed of in the wider context
			/// </summary>
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
