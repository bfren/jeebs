using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace Jeebs.Apps
{
	/// <summary>
	/// Razor Pages Application bootstrapped using IHost
	/// </summary>
	public abstract class RazorApp : MvcApp
	{
		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="useHsts">HSTS should only be disabled if the application is in development mode, or behind a reverse proxy</param>
		protected RazorApp(bool useHsts) : base(useHsts) { }

		/// <summary>
		/// Override to configure endpoints - default is MVC
		/// </summary>
		/// <param name="services">IServiceCollection</param>
		protected override void ConfigureServices_Endpoints(IServiceCollection services)
		{
			services
				.AddRazorPages(ConfigureServices_RazorPagesOptions)
				.AddRazorRuntimeCompilation(ConfigureServices_RuntimeCompilation)
				.AddJsonOptions(ConfigureServices_EndpointsJson);
		}

		/// <summary>
		/// Override to configure Razor Pages options
		/// </summary>
		/// <param name="opt">RazorPagesOptions</param>
		public virtual void ConfigureServices_RazorPagesOptions(RazorPagesOptions opt)
		{

		}

		/// <summary>
		/// Override to configure endpoints
		/// </summary>
		/// <param name="app">IApplicationBuilder</param>
		protected override void Configure_Endpoints(IApplicationBuilder app)
		{
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
			});
		}
	}
}
