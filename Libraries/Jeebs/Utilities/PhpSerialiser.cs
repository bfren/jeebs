/*
 * Serializer.cs
 * This is the Serializer class for the PHPSerializationLibrary
 *
 * Copyright 2004 Conversive, Inc.
 * http://sourceforge.net/projects/csphpserial/?source=typ_redirect
 *
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Jeebs.Util
{
	/// <summary>
	/// PHP Serialiser Class
	/// </summary>
	public sealed class PhpSerialiser
	{
		//types:
		// N = null
		// s = string
		// i = int
		// d = double
		// a = array (hashtable)

		/// <summary>
		/// For serialize (to infinte prevent loops)
		/// </summary>
		private Dictionary<Hashtable, bool> seenHashtables;

		/// <summary>
		/// For serialize (to infinte prevent loops) lol
		/// </summary>
		private Dictionary<ArrayList, bool> seenArrayLists;

		/// <summary>
		/// For unserialize
		/// </summary>
		private int pos;

		/// <summary>
		/// This member tells the serializer wether or not to strip carriage returns from strings when serializing
		/// and adding them back in when deserializing
		/// http://www.w3.org/TR/REC-xml/#sec-line-ends
		/// </summary>
		public bool XMLSafe = true;

		/// <summary>
		/// Encoding
		/// </summary>
		public Encoding StringEncoding = new UTF8Encoding();

		/// <summary>
		/// NumberFormatInfo
		/// </summary>
		private readonly System.Globalization.NumberFormatInfo nfi;

		/// <summary>
		/// Empty constructor
		/// </summary>
		public PhpSerialiser()
		{
			nfi = new System.Globalization.NumberFormatInfo
			{
				NumberGroupSeparator = "",
				NumberDecimalSeparator = "."
			};

			seenArrayLists = new Dictionary<ArrayList, bool>();
			seenHashtables = new Dictionary<Hashtable, bool>();
		}

		/// <summary>
		/// Serialise object
		/// </summary>
		/// <param name="obj">Object</param>
		/// <returns>Serialised object</returns>
		public string Serialise(object obj)
		{
			return PerformSerialisation(obj, new StringBuilder()).ToString();
		}

		private StringBuilder PerformSerialisation(object obj, StringBuilder sb)
		{
			if (obj == null)
			{
				return sb.Append("N;");
			}
			else if (obj is string str)
			{
				if (XMLSafe)
				{
					str = str.Replace("\r\n", "\n");//replace \r\n with \n
					str = str.Replace("\r", "\n");//replace \r not followed by \n with a single \n  Should we do this?
				}

				return sb.Append("s:").Append(StringEncoding.GetByteCount(str)).Append(":\"").Append(str).Append("\";");
			}
			else if (obj is bool bln)
			{
				return sb.Append("b:").Append(bln ? "1" : "0").Append(';');
			}
			else if (obj is int || obj is long || obj is double)
			{
				return sb.Append("i:").Append(nfi).Append(';');
			}
			else if (obj is ArrayList al)
			{
				if (seenArrayLists.ContainsKey(al))
				{
					return sb.Append("N;");//cycle detected
				}
				else
				{
					seenArrayLists.Add(al, true);
				}

				ArrayList a = al;
				sb.Append("a:").Append(a.Count).Append(":{");
				for (int i = 0; i < a.Count; i++)
				{
					PerformSerialisation(i, sb);
					PerformSerialisation(a[i], sb);
				}
				sb.Append("}");
				return sb;
			}
			else if (obj is Hashtable ht)
			{
				if (seenHashtables.ContainsKey(ht))
				{
					return sb.Append("N;");//cycle detected
				}
				else
				{
					seenHashtables.Add(ht, true);
				}

				Hashtable a = ht;
				sb.Append("a:").Append(a.Count).Append(":{");
				foreach (DictionaryEntry entry in a)
				{
					PerformSerialisation(entry.Key, sb);
					PerformSerialisation(entry.Value, sb);
				}
				sb.Append("}");
				return sb;
			}
			else
			{
				return sb;
			}
		}

		/// <summary>
		/// Deserialise object
		/// </summary>
		/// <param name="str">Serialised string</param>
		/// <returns>Deserialised object</returns>
		public object Deserialise(string str)
		{
			pos = 0;
			return PerformDeserialisation(str);
		}

		private object PerformDeserialisation(string str)
		{
			if (str == null || str.Length <= pos)
			{
				return new object();
			}

			int start, end, length;
			string stLen;
			switch (str[pos])
			{
				case 'N':
					pos += 2;
					return new object();

				case 'b':
					char chBool = str[pos + 2];
					pos += 4;
					return chBool == '1';

				case 'i':
					string stInt;
					start = str.IndexOf(":", pos) + 1;
					end = str.IndexOf(";", start);
					stInt = str[start..end];
					pos += 3 + stInt.Length;
					object oRet;
					try
					{
						//firt try to parse as int
						oRet = int.Parse(stInt, nfi);
					}
					catch
					{
						//if it failed, maybe it was too large, parse as long
						oRet = long.Parse(stInt, nfi);
					}

					return oRet;

				case 'd':
					string stDouble;
					start = str.IndexOf(":", pos) + 1;
					end = str.IndexOf(";", start);
					stDouble = str[start..end];
					pos += 3 + stDouble.Length;
					return double.Parse(stDouble, nfi);

				case 's':
					start = str.IndexOf(":", pos) + 1;
					end = str.IndexOf(":", start);
					stLen = str[start..end];
					int bytelen = int.Parse(stLen);
					length = bytelen;

					//This is the byte length, not the character length - so we might
					//need to shorten it before usage. This also implies bounds checking
					if ((end + 2 + length) >= str.Length)
					{
						length = str.Length - 2 - end;
					}

					string stRet = str.Substring(end + 2, length);

					while (StringEncoding.GetByteCount(stRet) > bytelen)
					{
						length--;
						stRet = str.Substring(end + 2, length);
					}
					pos += 6 + stLen.Length + length;

					if (XMLSafe)
					{
						stRet = stRet.Replace("\n", "\r\n");
					}

					return stRet;

				case 'a':
					//if keys are ints 0 through N, returns an ArrayList, else returns Hashtable
					start = str.IndexOf(":", pos) + 1;
					end = str.IndexOf(":", start);
					stLen = str[start..end];
					length = int.Parse(stLen);
					Hashtable htRet = new Hashtable(length);
					ArrayList alRet = new ArrayList(length);
					pos += 4 + stLen.Length; //a:Len:{

					for (int i = 0; i < length; i++)
					{
						//read key
						object key = PerformDeserialisation(str);
						//read value
						object val = PerformDeserialisation(str);

						if (alRet != null)
						{
							if (key is int x && x == alRet.Count)
							{
								alRet.Add(val);
							}
							else
							{
								alRet = new ArrayList();
							}
						}

						htRet[key] = val;
					}
					pos++; //skip the }

					if (pos < str.Length && str[pos] == ';')//skipping our old extra array semi-colon bug (er... php's weirdness)
					{
						pos++;
					}

					return alRet ?? (object)htRet;

				default:
					return "";
			}
		}
	}
}
