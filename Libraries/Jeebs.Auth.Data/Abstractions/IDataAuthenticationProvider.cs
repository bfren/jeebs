using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Auth.Entities;

namespace Jeebs.Auth
{
	/// <summary>
	/// User authentication provider
	/// </summary>
	public interface IDataAuthenticationProvider
	{
		/// <summary>
		/// Validate a username and password
		/// </summary>
		/// <typeparam name="T">UserEntity type</typeparam>
		/// <param name="email">Email (username)</param>
		/// <param name="password">Password</param>
		Task<Option<T>> ValidateUserAsync<T>(string email, string password)
			where T : UserEntity;
	}
}
