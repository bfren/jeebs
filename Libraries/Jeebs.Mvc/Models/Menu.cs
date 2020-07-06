using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Jeebs.Mvc.Models
{
	/// <summary>
	/// Menu
	/// </summary>
	public abstract class Menu
	{
		/// <summary>
		/// List of top-level menu items
		/// </summary>
		public List<MenuItem> Items { get; set; } = new List<MenuItem>();

		/// <summary>
		/// Use a UrlHelper object to get simple menu items
		/// </summary>
		/// <param name="url">UrlHelper object</param>
		/// <returns>List of simple menu items</returns>
		public IEnumerable<MenuItemSimple> GetSimpleItems(IUrlHelper url)
		{
			foreach (var item in Items)
			{
				yield return new MenuItemSimple
				{
					Guid = Guid.NewGuid(),
					Text = item.Text ?? item.Controller,
					Url = GetUri(url, item)
				};
			}
		}

		/// <summary>
		/// Return the fully-qualified URI for the specified menu item
		/// </summary>
		/// <param name="url">UrlHelper object</param>
		/// <param name="item">Current menu item</param>
		/// <returns>Menu Item URL</returns>
		private string GetUri(IUrlHelper url, MenuItem item)
		{
			UrlActionContext c = new UrlActionContext
			{
				Protocol = url.ActionContext.HttpContext.Request.Scheme,
				Host = url.ActionContext.HttpContext.Request.Host.ToString(),
				Controller = item.Controller ?? string.Empty,
				Action = item.Action ?? "Index"
			};

			return url.Action(c);
		}

		/// <summary>
		/// Load the specified menu items (to help with caching / preloading pages)
		/// </summary>
		/// <param name="http">IHttpClientFactory</param>
		/// <param name="url">IUrlHelper</param>
		/// <param name="list">[Optional] Menu Items</param>
		/// <returns>Result to output as response</returns>
		public async Task<string> LoadItemsAsync(IHttpClientFactory http, IUrlHelper url, List<MenuItem>? list = null)
		{
			// Local function to write something to the StringBuilder
			StringBuilder result = new StringBuilder();
			void Write(string content)
			{
				result.Append(content);
			}

			// Make the respose HTML
			url.ActionContext.HttpContext.Response.ContentType = "text/html";

			// Loop through provided list, or default list of Items
			foreach (var item in list ?? Items)
			{
				// Build the URI to load and output it
				string uri = GetUri(url, item);
				Write($"Loading {uri} .. ");

				// Attempt to load the URL
				try
				{
					// Load the URL and ensure it is successful
					var response = await http.CreateClient().GetAsync(uri).ConfigureAwait(false);
					response.EnsureSuccessStatusCode();

					// Successful
					Write("done");
				}
				catch (HttpRequestException ex)
				{
					Write($"failed: {ex}");
				}

				// Put the next URL on a new line
				Write("<br/>");

				// Load any children
				if (item.Children is List<MenuItem> children)
				{
					Write("Loading children..<br/>");
					await LoadItemsAsync(http, url, children).ConfigureAwait(false);
					Write(".. done<br/>");
				}
			}

			// Return result
			return result.ToString();
		}
	}
}
