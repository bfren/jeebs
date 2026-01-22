// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Security.Cryptography;
using System.Text;
using Jeebs.Functions;
using Sodium;
using Sodium.Exceptions;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	public static Result<Lockable<T>> Unlock<T>(this Locked<T> box, byte[] key)
	{

		static Result<Lockable<T>> fail(Exception ex, string? message) =>
			R.Fail(ex).Msg(message).Ctx(nameof(CryptoF), nameof(Unlock));

		return box.EncryptedContents.Match(
			none: () => R.Fail("There are no encrypted contents to unlock.")
				.Ctx(nameof(CryptoF), nameof(Unlock)),
			some: x =>
			{
				try
				{
					// Open encrypted contents
					var secret = SecretBox.Open(x, box.Nonce, key);

					// Deserialise contents and return
					return JsonF.Deserialise<T>(Encoding.UTF8.GetString(secret))
						.Map(x => new Lockable<T>(x));
				}
				catch (KeyOutOfRangeException ex)
				{
					return fail(ex, "Invalid key.");
				}
				catch (NonceOutOfRangeException ex)
				{
					return fail(ex, "Invalid nonce.");
				}
				catch (CryptographicException ex)
				{
					return fail(ex, "Incorrect key or nonce.");
				}
				catch (Exception ex)
				{
					return fail(ex, null);
				}
			}
		);
	}
}
