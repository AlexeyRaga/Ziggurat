using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Infrastructure.Tests.Projections
{
    [TestClass]
    public class InMemoryProjectionReaderWriterTest
    {
        [TestMethod]
        public void Should_be_able_to_add_a_view()
        {
            var readerWriter = new InMemoryProjectionReaderWriter<Guid, DummyView>();
            var view = new DummyView() {
                Id = Guid.NewGuid(),
            };

            var addedView = readerWriter.Add(view.Id, view);
            Assert.AreEqual(view, addedView);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), 
            "InvalidOperationException should be thrown if trying to add a view with existing key")]
        public void Should_not_be_able_to_add_a_view_with_existing_key()
        {
            var readerWriter = new InMemoryProjectionReaderWriter<Guid, DummyView>();
            var key = Guid.NewGuid();

            var view1 = new DummyView() {
                Id = key,
                Anything = Guid.NewGuid().ToString()
            };

            var view2 = new DummyView() {
                Id = key,
                Anything = Guid.NewGuid().ToString()
            };

            readerWriter.Add(key, view1);
            readerWriter.Add(key, view2);
        }

        private class DummyView
        {
            public Guid Id { get; set; }
            public string Anything { get; set; }
        }
    }
}
