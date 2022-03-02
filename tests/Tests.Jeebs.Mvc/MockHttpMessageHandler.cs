// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Jeebs.Mvc;

public class MockHttpMessageHandler : HttpMessageHandler
{
	private string Response { get; init; }

	private HttpStatusCode StatusCode { get; init; }

	public MockHttpMessageHandler() : this(F.Rnd.Str, HttpStatusCode.OK) { }

	public MockHttpMessageHandler(string response) : this(response, HttpStatusCode.OK) { }

	public MockHttpMessageHandler(HttpStatusCode statusCode) : this(F.Rnd.Str, statusCode) { }

	public MockHttpMessageHandler(string response, HttpStatusCode statusCode) =>
		(Response, StatusCode) = (response, statusCode);

	protected override Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken
	) =>
		Task.FromResult(new HttpResponseMessage
		{
			StatusCode = StatusCode,
			Content = new StringContent(Response)
		});
}
