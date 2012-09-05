using System;
using System.IO;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure.DocumentStore
{
	public sealed class FileSystemDocumentReaderWriter<TKey, TDocument> : IDocumentReader<TKey, TDocument>, IDocumentWriter<TKey, TDocument>
	{
		public string RootFolder { get; private set; }

		private readonly ISerializer _serializer;
		private readonly Func<TKey, string> _subfolderFactory;

		/// <summary>
		/// Creates a document reader and writer which is capable of persisting view models in a file system
		/// </summary>
		/// <param name="rootFolder">A root folder on disk where everything is persisted.</param>
		/// <param name="serializer">A serializer to use</param>
		public FileSystemDocumentReaderWriter(string rootFolder, ISerializer serializer)
			: this(rootFolder, null, serializer)
		{
		}

		/// <summary>
		/// Creates a document reader and writer which is capable of persisting view models in a file system
		/// </summary>
		/// <param name="rootFolder">A root folder on disk where everything is persisted.</param>
		/// <param name="subfolderFactory">Optional. A factory function from a key to subfolders tree. 
		/// For example, one may want to evenly distribute files between N subfolders.</param>
		/// <param name="serializer">A serializer to use</param>
		public FileSystemDocumentReaderWriter(string rootFolder, Func<TKey, string> subfolderFactory, ISerializer serializer)
		{
			if (String.IsNullOrWhiteSpace(rootFolder)) throw new ArgumentNullException("rootFolder");
			if (serializer == null) throw new ArgumentNullException("serializer");

			RootFolder = Path.Combine(rootFolder, typeof(TDocument).Name);
			_serializer = serializer;
			_subfolderFactory = subfolderFactory;
		}

		private string GetFolderForKey(TKey key)
		{
			return _subfolderFactory != null ? _subfolderFactory(key) : String.Empty;
		}

		private static string GetFileNameOnly(TKey key)
		{
			var idFileNamePart = key.ToString();
			return String.Format("{0}-{1}.ivm", idFileNamePart, typeof (TDocument).Name);
		}

		private string GetStoreFolder(TKey key)
		{
			return Path.Combine(RootFolder, GetFolderForKey(key));
		}

		private string GetFullFileName(TKey key)
		{
			var folderPath = GetStoreFolder(key);
			var fileName = GetFileNameOnly(key);

			return Path.Combine(folderPath, fileName);
		}

		public bool TryGet(TKey key, out TDocument document)
		{
			var fileName = GetFullFileName(key);

			try
			{
				if (File.Exists(fileName))
				{
					using (var stream = File.OpenRead(fileName))
					{
						document = _serializer.Deserialize<TDocument>(stream);
						return true;
					}
				}
			} 
			catch(IOException)
			{
				document = default(TDocument);
				return false;
			}
			catch (DocumentSerializationException)
			{
				document = default(TDocument);
				return false;
			}

			document = default(TDocument);
			return false;
		}

        public bool Exists(TKey key)
        {
            var fileName = GetFullFileName(key);
            return File.Exists(fileName);
        }

		public TDocument AddOrUpdate(TKey key, Func<TDocument> addFactory, Func<TDocument, TDocument> updateFactory)
		{
			var folderPath = GetStoreFolder(key);
			var fileName = GetFullFileName(key);

			try
			{
				if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

				// we are locking this file.
				using (var file = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
				{
					TDocument result;
					if (file.Length == 0)
					{
						result = addFactory();
					}
					else
					{
						result = _serializer.Deserialize<TDocument>(file);
						result = updateFactory(result);
					}

					file.Seek(0, SeekOrigin.Begin);
					_serializer.Serialize(result, file);
					file.SetLength(file.Position);

					return result;
				}
			}
			catch (DocumentSerializationException ex)
			{
				throw new DocumentStoreIOException(key, ex.Message, ex);
			}
			catch (IOException ex)
			{
				throw new DocumentStoreIOException(key, ex.Message, ex);
			}
		}

		public bool TryDelete(TKey key)
		{
			var fileName = GetFullFileName(key);
			if (!File.Exists(fileName)) return false;
			
			File.Delete(fileName);
			return true;
		}
	}
}
