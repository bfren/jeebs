using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Jm.Link;

namespace Jeebs
{
	/// <inheritdoc cref="ILink{TValue, TState}"/>
	public partial class Link<TValue, TState> : Link<TValue>, ILink<TValue, TState>
	{
		private readonly IR<TValue, TState> result;

		private readonly LinkExceptionHandlers<TValue, TState> handlers = new LinkExceptionHandlers<TValue, TState>();

		internal Link(IR<TValue, TState> result) : base(result)
			=> this.result = result;

		private IR<TNext, TState> Catch<TNext>(Func<IR<TNext, TState>> f)
		{
			try
			{
				return f();
			}
			catch (Exception ex)
			{
				Handle(ex);
				return result.Error<TNext>();
			}
		}
		private void Handle(Exception ex)
		{
			var handle = handlers.Get(ex.GetType()) switch
			{
				{ } specific => specific,
				_ => handlers.Get(typeof(Exception)) switch
				{
					{ } generic => generic,
					_ => (a, b) => a.AddMsg(new LinkExceptionMsg(b))
				}
			};

			handle(result, ex);
		}

		private IR<TNext, TState> Catch<TNext>(Func<Task<IR<TNext, TState>>> f)
		{
			try
			{
				return f().Await();
			}
			catch (Exception ex)
			{
				Handle(ex);
				return result.Error<TNext>();
			}
		}

		/// <summary>
		/// Dispose of this Link and <see cref="result"/>
		/// </summary>
		new public void Dispose()
		{
			base.Dispose();
			result.Dispose();
		}
	}
}
