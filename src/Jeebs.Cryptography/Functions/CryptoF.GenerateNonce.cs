// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;
using Sodium;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Generate a 24 byte nonce to use for encryption
	/// </summary>
	public static byte[] GenerateNonce() =>
		SecretBox.GenerateNonce();

	public static partial class M
	{
		/// <summary>Something went wrong while generating a nonce</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class GeneratingNonceExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
