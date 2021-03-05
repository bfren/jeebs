using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Jeebs.Config;
using Microsoft.Extensions.Options;

namespace Jeebs.Auth
{
	/// <inheritdoc cref="IJwtAuthenticationProvider"/>
	public class JwtAuthenticationProvider : IJwtAuthenticationProvider
	{
		private readonly JwtConfig config;

		/// <summary>
		/// Inject dependencies
		/// </summary>
		/// <param name="config">JeebsConfig</param>
		public JwtAuthenticationProvider(IOptions<JeebsConfig> config) : this(config.Value.Web.Jwt) { }

		internal JwtAuthenticationProvider(JwtConfig config) =>
			this.config = config;

		/// <inheritdoc/>
		public Option<string> CreateToken(IPrincipal principal) =>
			F.JwtF.CreateToken(config, principal);

		/// <inheritdoc/>
		public Option<IPrincipal> ValidateToken(string token) =>
			F.JwtF.ValidateToken(config, token);
	}
}
