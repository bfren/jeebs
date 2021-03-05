using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Auth.Entities;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IDataAuthenticationProvider"/>
	public abstract class DataAuthenticationProvider : IDataAuthenticationProvider
	{
		/// <inheritdoc/>
		public async Task<Option<T>> ValidateUserAsync<T>(string email, string password)
			where T : UserEntity
		{



			throw new NotImplementedException();
		}
	}
}
