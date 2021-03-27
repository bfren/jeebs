// Jeebs Test Applications
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Data;
using Jeebs.Data.Enums;
using Jeebs.Data.Querying;

namespace AppMvc
{
	internal sealed class Program : Jeebs.Apps.Program
	{
		private static async Task Main(string[] args) =>
			await MainAsync<App>(args).ConfigureAwait(false);

		void Fake()
		{
			var query = new DbQuery().QueryAsync<TestModel>(builder => builder
				.From<FooTable>()
				.Join<FooTable, BarTable>(QueryJoin.Inner, f => f.FooId, b => b.BarId)
				.Where<FooTable>(f => f.FooString, SearchOperator.Equal, "Hello!")
				.Where<BarTable>(b => b.BarDate, SearchOperator.LessThan, DateTime.Now)
				.SortBy<FooTable>(f => f.FooUpdated)
				.Maximum(4)
				.Skip(10)
			);
		}
	}

	public class TestModel
	{

	}

	public class FooTable : ITable
	{
		public string FooId { get; set; }

		public string FooString { get; set; }

		public string FooUpdated { get; set; }

		public string GetName()
		{
			throw new System.NotImplementedException();
		}
	}


	public class BarTable : ITable
	{
		public string BarId { get; set; }

		public string BarDate { get; set; }

		public string GetName()
		{
			throw new System.NotImplementedException();
		}
	}

	public class DbQuery : IDbQuery
	{
		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(string query, object? param, System.Data.CommandType type, System.Data.IDbTransaction? transaction = null)
		{
			throw new NotImplementedException();
		}

		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(string query, object? param, System.Data.IDbTransaction? transaction = null)
		{
			throw new NotImplementedException();
		}

		public Task<Option<IPagedList<TModel>>> QueryAsync<TModel>(long page, IQueryParts parts, System.Data.IDbTransaction? transaction = null)
		{
			throw new NotImplementedException();
		}

		public Task<Option<IEnumerable<TModel>>> QueryAsync<TModel>(IQueryParts parts, System.Data.IDbTransaction? transaction = null)
		{
			throw new NotImplementedException();
		}

		public Task<Option<TModel>> QuerySingleAsync<TModel>(string query, object? param, System.Data.CommandType type, System.Data.IDbTransaction? transaction = null)
		{
			throw new NotImplementedException();
		}

		public Task<Option<TModel>> QuerySingleAsync<TModel>(string query, object? param, System.Data.IDbTransaction? transaction = null)
		{
			throw new NotImplementedException();
		}

		public Task<Option<TModel>> QuerySingleAsync<TModel>(IQueryParts parts, System.Data.IDbTransaction? transaction = null)
		{
			throw new NotImplementedException();
		}
	}
}
