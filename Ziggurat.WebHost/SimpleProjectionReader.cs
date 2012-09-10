using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Web
{
    public interface IViewModelReader
    {
        TView Load<TKey, TView>(TKey key);
        bool TryGet<TKey, TView>(TKey key, out TView view);
    }

    public static class IViewModelReaderExtensions
    {
        public static TView LoadOrDefault<TKey, TView>(this IViewModelReader reader, TKey key)
        {
            TView view;
            reader.TryGet(key, out view);
            return view;
        }
    }

    // A very simple reader.
    // concrete readers can be cached for performance reasons if it is needed.
    public sealed class SimpleProjectionReader : IViewModelReader
    {
        private readonly IDocumentStore _projectionStore;
        public SimpleProjectionReader(IDocumentStore projectionStore)
        {
            _projectionStore = projectionStore;
        }

        public TView Load<TKey, TView>(TKey key)
        {
            var actualReader = _projectionStore.GetReader<TKey, TView>();
            return actualReader.Load(key);
        }

        public bool TryGet<TKey, TView>(TKey key, out TView view)
        {
            var actualReader = _projectionStore.GetReader<TKey, TView>();
            return actualReader.TryGet(key, out view);
        }
    }
}