using System;
using System.Linq;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain
{
    public abstract class ApplicationServiceBase<T>
        where T : IAggregateRoot, new()
    {
        private readonly IEventStore _store;
        private readonly CommonRepository _repository;

        /// <summary>
        /// A repository that can load any type of aggregate (for read-only purposes)
        /// </summary>
        protected IAggregateRepository Repository { get { return _repository; } }

        protected ApplicationServiceBase(IEventStore store)
        {
            if (store == null) throw new ArgumentNullException("store");

            _repository = new CommonRepository(store);
            _store      = store;
        }

        //this.Update(myId, ar => ar.DoSomething(parameters));
        protected void Update(Guid aggregateId, Action<T> updateAction)
        {
            var aggregate = _repository.GetById<T>(aggregateId);
 
            updateAction(aggregate);

            _repository.Save(aggregate);
        }
    }

}
