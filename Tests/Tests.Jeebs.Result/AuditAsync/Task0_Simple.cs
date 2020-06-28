﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeebs;
using Xunit;

namespace Tests.Jeebs.Result.AuditAsync
{
	public class Task0_Simple
	{
		[Fact]
		public async Task StartSync_AuditAsync_Returns_Original_Object()
		{
			// Arrange
			var chain = R.Chain;
			static async Task audit<T>(IR<T> _) { }

			// Act
			var result = await chain.AuditAsync(audit);

			// Assert
			Assert.StrictEqual(chain, result);
		}

		[Fact]
		public async Task StartSync_Successful_AuditAsync_Writes_To_Log()
		{
			// Arrange
			var chain = R.Chain;

			static async Task<IR<int>> l0<T>(IOk<T> r) => r.OkV(18);
			static async Task<IR<T>> l1<T>(IOkV<T> r) => r.Error();

			var log = new List<string>();
			async Task audit<T>(IR<T> r)
			{
				if (r is IError<T> error)
				{
					log.Add("Error!");
				}
				else if (r is IOkV<T> ok)
				{
					log.Add($"Value: {ok.Val}");
				}
				else
				{
					log.Add("Unknown state.");
				}
			}

			var expected = new[] { "Unknown state.", "Value: 18", "Error!" }.ToList();

			// Act
			await chain
				.AuditAsync(audit)
				.LinkMapAsync(l0)
				.AuditAsync(audit)
				.LinkMapAsync(l1)
				.AuditAsync(audit);

			// Assert
			Assert.Equal(expected, log);
		}

		[Fact]
		public async Task StartSync_Unsuccessful_Audit_Captures_Exception()
		{
			// Arrange
			var chain = R.Chain;
			static async Task audit<T>(IR<T> _) => throw new Exception();

			// Act
			var result = await chain.AuditAsync(audit);

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.AuditAsyncException>());
		}

		[Fact]
		public async Task StartAsync_AuditAsync_Returns_Original_Object()
		{
			// Arrange
			var chain = R.ChainAsync;
			static async Task audit<T>(IR<T> _) { }

			// Act
			var result = await chain.AuditAsync(audit);

			// Assert
			Assert.StrictEqual(await chain, result);
		}

		[Fact]
		public async Task StartAsync_Successful_AuditAsync_Writes_To_Log()
		{
			// Arrange
			var chain = R.ChainAsync;

			static async Task<IR<int>> l0<T>(IOk<T> r) => r.OkV(18);
			static async Task<IR<T>> l1<T>(IOkV<T> r) => r.Error();

			var log = new List<string>();
			async Task audit<T>(IR<T> r)
			{
				if (r is IError<T> error)
				{
					log.Add("Error!");
				}
				else if (r is IOkV<T> ok)
				{
					log.Add($"Value: {ok.Val}");
				}
				else
				{
					log.Add("Unknown state.");
				}
			}

			var expected = new[] { "Unknown state.", "Value: 18", "Error!" }.ToList();

			// Act
			await chain
				.AuditAsync(audit)
				.LinkMapAsync(l0)
				.AuditAsync(audit)
				.LinkMapAsync(l1)
				.AuditAsync(audit);

			// Assert
			Assert.Equal(expected, log);
		}

		[Fact]
		public async Task StartAsync_Unsuccessful_Audit_Captures_Exception()
		{
			// Arrange
			var chain = R.ChainAsync;
			static async Task audit<T>(IR<T> _) => throw new Exception();

			// Act
			var result = await chain.AuditAsync(audit);

			// Assert
			Assert.Equal(1, result.Messages.Count);
			Assert.True(result.Messages.Contains<Jm.AuditAsyncException>());
		}
	}
}
