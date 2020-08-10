using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jm.Link;

namespace Jeebs
{
	/// <inheritdoc cref="ILink{TValue}"/>
	public partial class Link<TValue> : ILink<TValue>
	{
		private readonly IR<TValue> result;

		private readonly LinkExceptionHandlers<TValue> handlers = new LinkExceptionHandlers<TValue>();

		internal Link(IR result)
			=> this.result = result.ChangeType().To<TValue>();

		internal Link(IR<TValue> result)
			=> this.result = result;

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

		private IR<TNext> Catch<TNext>(Func<IR<TNext>> f)
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

		private IR<TNext> Catch<TNext>(Func<Task<IR<TNext>>> f)
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
		/// Dispose of this <see cref="result"/>
		/// </summary>
		public void Dispose()
			=> result.Dispose();
	}
}
