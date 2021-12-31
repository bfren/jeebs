// Jeebs Test Applications
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using AppApi;
using Jeebs;
using Jeebs.Cqrs;
using Microsoft.AspNetCore.Mvc;

var (app, log) = Jeebs.Apps.Builder.Create(args, useHsts: false, configure: builder => builder.Services.AddCqrs());

app.MapGet("/", () => "Hello, world!");
app.MapGet("/hello/{name}", HandleSayHello);

app.Run();

static async Task<IResult> HandleSayHello(
	[FromServices] IQueryDispatcher query,
	[FromRoute] string name
)
{
	var text = await query
		.DispatchAsync<SayHelloQuery, string>(
			new(name)
		)
		.UnwrapAsync(
			x => x.Value(ifNone: "Nothing to say.")
		);

	return Results.Text(text);
}
