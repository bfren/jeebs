// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Jeebs.Apps.Web.MvcApp_Tests;

public class Configure_Tests
{
	public sealed class TestApp : MvcApp
	{
		public const string Handled = "handled by /Error";

		public TestApp() : base(false) { }

		protected override void ConfigureEndpoints(WebApplication app)
		{
			_ = app.MapGet("/throw", () => { throw new InvalidOperationException(); });
			_ = app.MapGet("/Error", () => Results.Text(Handled, statusCode: StatusCodes.Status500InternalServerError));
		}
	}

	[Fact]
	public async Task Endpoint_Throws__In_Production__Runs_Exception_Handler()
	{
		// Arrange
		var (app, _) = WebAppBuilder.Create<TestApp>(
			["--environment", "Production", "--urls", "http://127.0.0.1:0"], (_, _) => { }
		);
		await using var _ = app;
		await app.StartAsync(TestContext.Current.CancellationToken);
		using var client = new HttpClient { BaseAddress = new(app.Urls.First()) };

		// Act
		var result = await client.GetAsync("/throw", TestContext.Current.CancellationToken);

		// Assert
		Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
		Assert.Equal(TestApp.Handled, await result.Content.ReadAsStringAsync(TestContext.Current.CancellationToken));
	}
}
