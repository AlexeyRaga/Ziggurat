using System;
using System.Collections.Generic;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure.DocumentStore
{
	public sealed class FileSystemDocumentStore : IDocumentStore
	{
		private readonly string _rootFolder;
		private readonly ISerializer _serializer;

		private readonly Dictionary<Type, object> _keyFactories
			= new Dictionary<Type, object>();

		public FileSystemDocumentStore(string rootFolder, ISerializer serializer)
		{
			if (serializer == null) throw new ArgumentNullException("serializer");
			if (String.IsNullOrEmpty(rootFolder)) throw new ArgumentNullException("rootFolder");

			_serializer = serializer;
			_rootFolder = rootFolder;

            RegisterKeyFactory<long>(KeyFactories.TwoSubfolders);
            RegisterKeyFactory<string>(KeyFactories.TwoSubfolders);
		}

		public IDocumentReader<TKey, TDocument> GetReader<TKey, TDocument>()
		{
			var keyFactory = GetKeyFactory<TKey>();
			return new FileSystemDocumentReaderWriter<TKey, TDocument>(_rootFolder, keyFactory, _serializer);
		}

		public IDocumentWriter<TKey, TDocument> GetWriter<TKey, TDocument>()
		{
			var keyFactory = GetKeyFactory<TKey>();
			return new FileSystemDocumentReaderWriter<TKey, TDocument>(_rootFolder, keyFactory, _serializer);
		}

		private Func<TKey, string> GetKeyFactory<TKey>()
		{
			object keyFactory;
			if (!_keyFactories.TryGetValue(typeof(TKey), out keyFactory))
				return null;

			return (Func<TKey, string>)keyFactory;
		}

		public void RegisterKeyFactory<TKey>(Func<TKey, string> keyFactory)
		{
            if (keyFactory == null) return;
		    Type keyType = typeof (TKey);
            if (!_keyFactories.ContainsKey(keyType))
            {
                _keyFactories.Add(typeof(TKey), keyFactory);
            }
            else
            {
                _keyFactories[keyType] = keyFactory;
            }
		}
	}
}
