// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Jeebs.Mvc.Models;

/// <summary>
/// Menu.
/// </summary>
public abstract class Menu
{
	/// <summary>
	/// Get a URI from a Menu Item.
	/// </summary>
	/// <param name="urlHelper">IUrlHelper.</param>
	/// <param name="item">MenuItem.</param>
	public delegate string? GetUri(IUrlHelper urlHelper, MenuItem item);

	/// <summary>
	/// Load a URI and return a line describing the result.
	/// </summary>
	/// <param name="client">HttpClient.</param>
	/// <param name="uri">URI to load.</param>
	/// <param name="token">CancellationToken.</param>
	public delegate ValueTask<string> LoadUri(HttpClient client, string uri, CancellationToken token);

	/// <summary>
	/// List of top-level menu items.
	/// </summary>
	public List<MenuItem> Items { get; private init; } = [];

	/// <inheritdoc cref="MenuF.GetSimpleItems(IUrlHelper, List{MenuItem}, GetUri)"/>
	public IEnumerable<MenuItemSimple> GetSimpleItems(IUrlHelper urlHelper) =>
		MenuF.GetSimpleItems(urlHelper, Items, GetUriFromActionContext);

	/// <summary>
	/// Load this menu's items (to speed up page loading).
	/// </summary>
	/// <param name="http">IHttpClientFactory.</param>
	/// <param name="urlHelper">IUrlHelper.</param>
	public Task<Maybe<string>> LoadItemsAsync(IHttpClientFactory http, IUrlHelper urlHelper)
	{
		// Create client
		var client = http.CreateClient();

		// Get URIs
		var uris = MenuF.GetUris(urlHelper, Items, GetUriFromActionContext);

		// Load items
		return MenuF.LoadUrisAsync(client, uris, MenuF.LoadUriAsync);
	}

	/// <summary>
	/// Build a URI using the <paramref name="urlHelper"/> ActionContext.
	/// </summary>
	/// <param name="urlHelper">IUrlHelper.</param>
	/// <param name="item">MenuItem.</param>
	private static string? GetUriFromActionContext(IUrlHelper urlHelper, MenuItem item)
	{
		// Build the context
		var actionContext = new UrlActionContext
		{
			Protocol = urlHelper.ActionContext.HttpContext.Request.IsHttps ? "https" : "http",
			Host = urlHelper.ActionContext.HttpContext.Request.Host.ToString(),
			Controller = item.Controller,
			Action = item.Action,
			Values = item.RouteValues
		};

		// Create action URL
		return urlHelper.Action(actionContext);
	}

	/// <summary>
	/// Helper Functions.
	/// </summary>
	internal static class MenuF
	{
		/// <summary>
		/// Use a UrlHelper object to get simple menu items.
		/// </summary>
		/// <param name="urlHelper">UrlHelper object.</param>
		/// <param name="items">Menu Items.</param>
		/// <param name="getUri">Function to get a URI from a menu item.</param>
		internal static IEnumerable<MenuItemSimple> GetSimpleItems(IUrlHelper urlHelper, List<MenuItem> items, GetUri getUri)
		{
			foreach (var item in items)
			{
				if (getUri(urlHelper, item) is string uri)
				{
					yield return new(Guid.NewGuid(), item.Text ?? item.Controller, uri);
				}
			}
		}

		/// <summary>
		/// Get URIs from a list of menu items.
		/// </summary>
		/// <param name="urlHelper">IUrlHelper.</param>
		/// <param name="items">Menu Items.</param>
		/// <param name="getUri">Get URI delegate.</param>
		internal static List<string> GetUris(IUrlHelper urlHelper, List<MenuItem> items, GetUri getUri)
		{
			// Holds list of URIs
			var uris = new List<string>();

			// Add each item, and any children
			foreach (var item in items)
			{
				// Get parent URI
				if (getUri(urlHelper, item) is string uri)
				{
					uris.Add(uri);
				}

				// Get child URIs
				if (item.Children.Count == 0)
				{
					continue;
				}

				uris.AddRange(GetUris(urlHelper, item.Children, getUri));
			}

			// Return list
			return uris;
		}

		/// <summary>
		/// Load the specified URIs.
		/// </summary>
		/// <param name="client">HttpClient.</param>
		/// <param name="uris">List of URIs to load.</param>
		/// <param name="loadUri">LoadUri.</param>
		internal static async Task<Maybe<string>> LoadUrisAsync(HttpClient client, List<string> uris, LoadUri loadUri)
		{
			// Each URI writes only its own slot, so the parallel loads can't interfere and the lines stay in order
			var lines = new string[uris.Count];

			// Load up to three URIs at a time
			await Parallel.ForAsync(
				fromInclusive: 0,
				toExclusive: uris.Count,
				parallelOptions: new() { MaxDegreeOfParallelism = 3 },
				body: async (i, token) => lines[i] = await loadUri(client, uris[i], token)
			);

			// Return result
			return string.Concat(lines);
		}

		/// <summary>
		/// Load a URI asynchronously, returning a line describing the result.
		/// </summary>
		/// <param name="client">HttpClient.</param>
		/// <param name="uri">The URI to load.</param>
		/// <param name="token">CancellationToken.</param>
		internal static async ValueTask<string> LoadUriAsync(HttpClient client, string uri, CancellationToken token)
		{
			// Output URI to be loaded
			var output = $"Loading {uri} .. ";

			try
			{
				// Attempt to load the URL and ensure it is successful
				var response = await client.GetAsync(uri, token);
				_ = response.EnsureSuccessStatusCode();

				// Successful
				output += "done";
			}
			catch (HttpRequestException ex)
			{
				output += $"failed: {ex}";
			}
			catch (TaskCanceledException ex) when (!token.IsCancellationRequested)
			{
				// the request timed out (rather than the whole load being cancelled)
				output += $"failed: {ex}";
			}

			// Put the next URL on a new line
			return output + "<br/>" + Environment.NewLine;
		}
	}
}
