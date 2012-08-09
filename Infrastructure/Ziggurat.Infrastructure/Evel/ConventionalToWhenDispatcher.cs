using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ziggurat.Infrastructure
{
    public interface IMessageDispatcher
    {
        void Subscribe(object subscriber);
        void DispatchToOneAndOnlyOne(object message);
        void DispatchToAll(object message);
    }

    public sealed class ConventionalToWhenDispatcher : IMessageDispatcher
    {
        private readonly Dictionary<Type, List<Tuple<object, Action<object>>>> _subscriptions
            = new Dictionary<Type, List<Tuple<object, Action<object>>>>();

        private readonly object _lockRoot = new object();

        public void Subscribe(object subscriber)
        {
            if (subscriber == null) throw new ArgumentNullException("subscriber");

            var subscribedTypes = GetSubscribedTypes(subscriber.GetType());

            lock (_lockRoot)
            {
                foreach (var type in subscribedTypes)
                {
                    AddSubscription(subscriber, type);
                }
            }            
        }

        public void DispatchToOneAndOnlyOne(object message)
        {
            var actions = GetActionsToDispatch(message).ToList();
            if (actions.Count != 1)
                throw new InvalidOperationException(String.Format("One and only one handler is expected for {0}, but {1} found", 
                    message.GetType().Name, actions.Count));

            try
            {
                actions[0](message);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        public void DispatchToAll(object message)
        {
            var actions = GetActionsToDispatch(message);

            try
            {
                foreach (var action in actions)
                {
                    action(message);
                }
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        private IEnumerable<Action<object>> GetActionsToDispatch(object message)
        {
            List<Tuple<object, Action<object>>> subscriptions;
            var messageType = message.GetType();

            if (!_subscriptions.TryGetValue(messageType, out subscriptions))
                return new Action<object>[0];

            return subscriptions
                .Select(x => x.Item2)
                .ToList();
        }
    
        private void AddSubscription(object subscriber, Type subscribedType)
        {
            var subscription = Tuple.Create(subscriber, DelegateAdjuster.CastArgument<object>(subscriber, subscribedType, "When"));

            List<Tuple<object, Action<object>>> registeredSubscriptions;
            if (!_subscriptions.TryGetValue(subscribedType, out registeredSubscriptions))
            {
                registeredSubscriptions = new List<Tuple<object, Action<object>>>();
                _subscriptions.Add(subscribedType, registeredSubscriptions);
            }

            registeredSubscriptions.Add(subscription);
        }

        private static IEnumerable<Type> GetSubscribedTypes(Type subscriberType)
        {
            var subscribedTypes = subscriberType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.Name == "When")
                .Select(x => x.GetParameters())
                .Where(x => x.Length == 1)
                .Select(x => x.First())
                .Where(x => !x.IsOut)
                .Select(x=>x.ParameterType)
                .Distinct();

            return subscribedTypes.ToList();
        }
    }
}
