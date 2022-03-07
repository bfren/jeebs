// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace AppRazor;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		_ = services.AddRazorPages();
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			_ = app.UseDeveloperExceptionPage();
		}
		else
		{
			_ = app.UseExceptionHandler("/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			_ = app.UseHsts();
		}

		_ = app.UseHttpsRedirection();
		_ = app.UseStaticFiles();

		_ = app.UseRouting();

		_ = app.UseAuthorization();

		_ = app.UseEndpoints(endpoints =>
		  {
			  _ = endpoints.MapRazorPages();
		  });
	}
}
