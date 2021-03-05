using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Jeebs.Authentication.Defaults
{
	/// <summary>
	/// Default Jwt algorithms
	/// </summary>
	public static class Algorithms
	{
		/// <summary>
		/// Default signing algorithm (256-bits = 32 characters)
		/// </summary>
		public const string Signing = SecurityAlgorithms.HmacSha256;

		/// <summary>
		/// Default encrypting algorithm (512-bits = 64 characters)
		/// </summary>
		public const string Encrypting = SecurityAlgorithms.Aes256CbcHmacSha512;
	}
}
