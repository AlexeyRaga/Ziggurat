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
        bool Exists<TKey, TView>(TKey key);
    }

    public static class IViewModelReaderExtensions
    {
        public static TView LoadOrDefault<TKey, TView>(this IViewModelReader reader, TKey key)
        {
            TView view;
            reader.TryGet(key, out view);
            return view;
        }

        public static TView LoadOrDefault<TKey, TView>(this IViewModelReader reader, TKey key, Func<TKey, TView> defaultFactory)
        {
            var view = reader.LoadOrDefault<TKey, TView>(key);
            return (view == null && defaultFactory != null)
                ? defaultFactory(key)
                : view;
        }

        public static TView LoadOrNew<TKey, TView>(this IViewModelReader reader, TKey key)
            where TView: class, new()
        {
            var view = reader.LoadOrDefault<TKey, TView>(key);
            return view ?? new TView();
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

        public bool Exists<TKey, TView>(TKey key)
        {
            var actualReader = _projectionStore.GetReader<TKey, TView>();
            return actualReader.Exists(key);
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