// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Jeebs.Mvc.Models;

/// <summary>
/// Menu
/// </summary>
public abstract class Menu
{
	/// <summary>
	/// List of top-level menu items
	/// </summary>
	public List<MenuItem> Items { get; private init; } = new();

	/// <inheritdoc cref="GetSimpleItems(IUrlHelper, Func{IUrlHelper, MenuItem, string?})"/>
	public IEnumerable<MenuItemSimple> GetSimpleItems(IUrlHelper urlHelper) =>
		GetSimpleItems(urlHelper, GetUri);

	/// <summary>
	/// Use a UrlHelper object to get simple menu items
	/// </summary>
	/// <param name="urlHelper">UrlHelper object</param>
	/// <param name="getUri">Function to get a URI from a menu item</param>
	internal IEnumerable<MenuItemSimple> GetSimpleItems(IUrlHelper urlHelper, Func<IUrlHelper, MenuItem, string?> getUri)
	{
		foreach (var item in Items)
		{
			if (getUri(urlHelper, item) is string uri)
			{
				yield return new(Guid.NewGuid(), item.Text ?? item.Controller, uri);
			}
		}
	}

	/// <summary>
	/// Load the specified menu items (to help with caching / preloading pages)
	/// </summary>
	/// <param name="http">IHttpClientFactory</param>
	/// <param name="urlHelper">IUrlHelper</param>
	public Task<Option<string>> LoadItemsAsync(IHttpClientFactory http, IUrlHelper urlHelper) =>
		LoadItemsAsync(http.CreateClient(), urlHelper, null);

	/// <summary>
	/// Load the specified menu items (to help with caching / preloading pages)
	/// </summary>
	/// <param name="client">HttpClient</param>
	/// <param name="urlHelper">IUrlHelper</param>
	/// <param name="list">Menu Items</param>
	internal async Task<Option<string>> LoadItemsAsync(HttpClient client, IUrlHelper urlHelper, List<MenuItem>? list)
	{
		// Use a StringBuilder to hold the response text
		var result = new StringBuilder();

		// Loop through provided list, or default list of Items
		await Parallel.ForEachAsync(
			source: list ?? Items,
			parallelOptions: new() { MaxDegreeOfParallelism = 3 },
			body: async (item, token) =>
			{
				// Build the URI to load it
				if (GetUri(urlHelper, item) is string uri)
				{
					await LoadUriAsync(result, client, uri, token);
				}

				// Continue if there are no children
				if (item.Children.Count == 0)
				{
					return;
				}

				// Load child items
				result.AppendLine("Loading children..<br/>");
				await LoadItemsAsync(client, urlHelper, item.Children).ConfigureAwait(false);
				result.AppendLine(".. done<br/>");
			}
		);

		// Return result
		return result.ToString();
	}

	#region Static Members

	/// <summary>
	/// Return the fully-qualified URI for the specified menu item
	/// </summary>
	/// <param name="urlHelper">UrlHelper</param>
	/// <param name="item">Current menu item</param>
	/// <returns>Menu Item URL</returns>
	internal static string? GetUri(IUrlHelper urlHelper, MenuItem item)
	{
		// Build the context
		var actionContext = new UrlActionContext
		{
			Protocol = urlHelper.ActionContext.HttpContext.Request.Scheme,
			Host = urlHelper.ActionContext.HttpContext.Request.Host.ToString(),
			Controller = item.Controller ?? string.Empty,
			Action = item.Action ?? "Index",
			Values = item.RouteValues
		};

		// Create action URL
		return urlHelper.Action(actionContext);
	}

	/// <summary>
	/// Load a URI asynchronously, writing output to <paramref name="result"/>
	/// </summary>
	/// <param name="result">StringBuilder</param>
	/// <param name="client">HttpClient</param>
	/// <param name="uri">The URI to load</param>
	/// <param name="token">CancellationToken</param>
	internal static async Task LoadUriAsync(StringBuilder result, HttpClient client, string uri, CancellationToken token)
	{
		// Output URI to be loaded
		result.Append($"Loading {uri} .. ");

		try
		{
			// Attempt to load the URL and ensure it is successful
			var response = await client.GetAsync(uri, token).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();

			// Successful
			result.Append("done");
		}
		catch (HttpRequestException ex)
		{
			result.Append($"failed: {ex}");
		}

		// Put the next URL on a new line
		result.AppendLine("<br/>");
	}

	#endregion
}
