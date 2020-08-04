using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jeebs
{
	/// <inheritdoc cref="ILink{TValue, TState}"/>
	public partial class Link<TValue, TState> : Link<TValue>, ILink<TValue, TState>
	{
		private readonly IR<TValue, TState> result;

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
				return result.Error<TNext>().AddMsg(new Jm.ChainExceptionMsg(ex));
			}
		}

		private IR<TNext, TState> Catch<TNext>(Func<Task<IR<TNext, TState>>> f)
		{
			try
			{
				return f().Await();
			}
			catch (Exception ex)
			{
				return result.Error<TNext>().AddMsg(new Jm.ChainExceptionMsg(ex));
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
