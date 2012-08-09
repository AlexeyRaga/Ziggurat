﻿using System;
using System.Collections.Generic;

namespace Ziggurat.Infrastructure.EventStore
{
    public interface IEventStore : IDisposable
    {
        EventStream Load(Guid aggregateIdentity);

        void Append(Guid aggregateIdentity, int revision, IEnumerable<object> events);
    }
}
