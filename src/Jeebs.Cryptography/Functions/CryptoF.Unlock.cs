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
	/// <summary>
	/// Unlock a Locked box using the specified <paramref name="key"/>.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="box">Locked box.</param>
	/// <param name="key">Encryption key.</param>
	/// <returns>Unlocked box.</returns>
	public static Result<Lockable<T>> Unlock<T>(this Locked<T> box, byte[] key) =>
		box.EncryptedContents.Match(
			fNone: () => NoEncryptedContents(),
			fSome: x =>
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
					return InvalidKey(ex);
				}
				catch (NonceOutOfRangeException ex)
				{
					return InvalidNonce(ex);
				}
				catch (CryptographicException ex)
				{
					return IncorrectKeyOrNonce(ex);
				}
				catch (Exception ex)
				{
					return R.Fail(ex).WithContext(nameof(CryptoF), nameof(Unlock));
				}
			}
		);

	[Failure("There are no encrypted contents to unlock.")]
	private static partial Failure NoEncryptedContents();

	[Failure("Invalid key.")]
	private static partial Failure InvalidKey(Exception e);

	[Failure("Invalid nonce.")]
	private static partial Failure InvalidNonce(Exception e);

	[Failure("Incorrect key or nonce.")]
	private static partial Failure IncorrectKeyOrNonce(Exception e);
}
