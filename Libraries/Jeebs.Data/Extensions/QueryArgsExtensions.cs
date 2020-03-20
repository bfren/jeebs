using System;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Data
{
	public static class QueryArgsExtensions
	{
		public static QueryExec<T> GetExec<T>(this QueryArgs<T> args, IUnitOfWork unitOfWork)
		{
			return new QueryExec<T>(unitOfWork, args);
		}
	}
}
