﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Registration.Client
{
    public static class RegistrationClientBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IDocumentStore factory)
        {
            yield return new RegistrationStatus.RegistrationStatusProjection(factory);
            yield return new Login.UserLoginProjection(factory);
        }
    }
}
