using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using Ziggurat.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ziggurat.Definition.Domain.Base;

namespace Ziggurat.Definition.Domain.Tests.Base
{
    /// <summary>
    /// Aggregate Given-When-Then test base class
    /// </summary>
    /// <typeparam name="TTarget">Aggregate type</typeparam>
    [TestClass]
    public abstract class AggregateTest<TTarget>
        where TTarget : IAggregateRoot, new()
    {
        /// <summary> Given a list of events </summary>
        protected IList<IEvent> Given { get; set; }

        /// <summary> When something happens </summary>
        protected Expression<Action<TTarget>> When { get; set; }
        
        /// <summary> Then the result should be a set of events </summary>
        protected IList<IEvent> Then { set { ExecuteAndAssert(value); } }

        /// <summary> Then an exception expected </summary>
        protected Expression<Func<Exception, bool>> ThenException { set { ExecuteAndCheckException(value); } }

        [TestInitialize]
        public void Initialize()
        {
            Given = new IEvent[0];
            When = null;
        }

        /// <summary>
        /// Creates an empty aggregates and applies Given events putting an aggregate into a required state
        /// </summary>
        private TTarget CreateAggregate()
        {
            PrintEvents("Given", Given);
            var aggregate = new TTarget();
            if (Given != null) aggregate.ApplyFromHistory(Given);

            return aggregate;
        }

        private void ExecuteAndAssert(IList<IEvent> expected)
        {
            var target = CreateAggregate();
            var result = ExecuteWhen(target);
            PrintEvents("Expect", expected);

            try
            {
                AssertEquality(expected.ToArray(), result.ToArray());
            }
            catch (AssertFailedException)
            {
                Console.WriteLine();
                PrintEvents("Actual", result);
                Console.WriteLine();
                throw;
            }
        }
        
        private void ExecuteAndCheckException(Expression<Func<Exception, bool>> predicate)
        {
            try
            {
                var target = CreateAggregate();
                var result = ExecuteWhen(target);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Expected Exception: ");
                Console.WriteLine("\t" + predicate);

                var compiledPredicate = predicate.Compile();
                if (compiledPredicate(ex)) return; else throw;
            }

            Assert.Fail("Expected exception: \n\t" + predicate);
        }

        // Executes the specified When action and returns a list of newly produced events
        private IList<IEvent> ExecuteWhen(TTarget aggregate)
        {
            PrintWhen();
            var compiledWhen = When != null ? When.Compile() : null;
            if (compiledWhen != null) compiledWhen(aggregate);

            return aggregate.Changes.ToList();
        }

        // Prints events to the output (to the console)
        private void PrintEvents(string topic, IList<IEvent> events)
        {
            Console.WriteLine(topic + ": ");
            if (!events.Any())
            {
                Console.WriteLine("\tNo events");
                return;
            }

            foreach (var evt in events)
            {
                Console.WriteLine("\t" + EventToString(evt));
            }
        }

        // Prints When action
        private void PrintWhen()
        {
            var whenString = When == null ? "Nothing happens" : When.ToString();
            Console.WriteLine("When: ");
            Console.WriteLine("\t" + whenString);
        }

        // Asserts both collections of events are equal (through binary serialization)
        private static void AssertEquality(IEvent[] expected, IEvent[] actual)
        {
            // in this method we assert full equality between events by serializing
            // and comparing data
            var actualBytes = SerializeEventsToBytes(actual);
            var expectedBytes = SerializeEventsToBytes(expected);
            bool areEqual = actualBytes.SequenceEqual(expectedBytes);
            if (areEqual) return;

            // however if events differ, and this can be seen in human-readable version,
            // then we display human-readable version (derived from ToString())
            CollectionAssert.AreEqual(
                expected.Select(s => s.ToString()).ToArray(),
                actual.Select(s => s.ToString()).ToArray());

            CollectionAssert.AreEqual(expectedBytes, actualBytes,
                "Expected events differ from actual, but differences are not represented in ToString() ");
        }

        private static byte[] SerializeEventsToBytes(IEvent[] actual)
        {
            // this helper class transforms events to their binary representation
            var formatter = new BinaryFormatter();
            using (var mem = new MemoryStream())
            {
                formatter.Serialize(mem, actual);
                return mem.ToArray();
            }
        }

        private static string EventToString(IEvent evt)
        {
            return evt.GetType().Name + " " 
                + Newtonsoft.Json.JsonConvert.SerializeObject(evt, Newtonsoft.Json.Formatting.None);
        }
    }
}
