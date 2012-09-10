using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ziggurat.Infrastructure.DocumentStore
{
	public static class KeyFactories
	{
		private static readonly MD5CryptoServiceProvider CryptoService = new MD5CryptoServiceProvider();

		/// <summary>
		/// Generates a deterministic subfolders structure for storing a viewmodel with a given <paramref name="key"/>
		/// in a format of [number]\[number].
		/// </summary>
		/// <param name="key">A viewmodel key</param>
		/// <returns>A subfolder structure.</returns>
		public static string TwoSubfolders(long key)
		{
			var bytes = BitConverter.GetBytes(key);
			return ToTwoSubfolders(bytes);
		}
		/// <summary>
		/// Generates a deterministic subfolders structure for storing a viewmodel with a given <paramref name="key"/>
		/// in a format of [number]\[number].
		/// </summary>
		/// <param name="key">A viewmodel key</param>
		/// <returns>A subfolder structure.</returns>
		public static string TwoSubfolders(string key)
		{
			if (String.IsNullOrWhiteSpace(key)) throw new ArgumentNullException("key");
			var bytes = Encoding.UTF8.GetBytes(key);
			return ToTwoSubfolders(bytes);
		}

		private static string ToTwoSubfolders(byte[] content)
		{
			var hash = CryptoService.ComputeHash(content);

			var first8 = BitConverter.ToUInt64(hash, 0);
			var last8 = BitConverter.ToUInt64(hash, 8);
			var subFolder1 = ((first8 % int.MaxValue) % 100).ToString();
			var subFolder2 = ((last8 % int.MaxValue) % 100).ToString();

			return Path.Combine(subFolder1, subFolder2);
		}
	}
}
