// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Messages;
using MaybeF;
using Sodium;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Generate a 32 byte key to use for encryption
	/// </summary>
	public static Maybe<byte[]> GenerateKey() =>
		F.Some(
			() => SecretBox.GenerateKey(),
			e => new M.GeneratingKeyExceptionMsg(e)
		);

	public static partial class M
	{
		/// <summary>Something went wrong while generating a key</summary>
		/// <param name="Value">Exception object</param>
		public sealed record class GeneratingKeyExceptionMsg(Exception Value) : ExceptionMsg;
	}
}
