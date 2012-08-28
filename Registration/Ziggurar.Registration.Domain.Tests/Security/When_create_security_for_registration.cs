using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Contracts.Registration;
using Ziggurat.Registration.Domain.Security;
using Ziggurat.Registration.Domain.Tests;

namespace Ziggurar.Registration.Domain.Tests.Security
{
    [TestClass]
    public sealed class When_create_security_for_registration :AggregateTest<SecurityAggregate>
    {
        [TestMethod]
        public void Should_create_security_with_password()
        {
            var regId = RegistrationIdGenerator.NewRegistrationId();
            var secId = RegistrationIdGenerator.NewSecutiryId(regId);

            var data = new SecurityData(regId, "alexeyraga", "alexey.raga@somewhere.near", "Password123");

            When = aggregate => aggregate.CreateForRegistration(secId, regId, data);
            Then = new IEvent[] {
                new SecurityCreated(secId, "alexeyraga"),
                new SecurityPasswordSet(secId, "alexeyraga", "Password123"),
                new SecurityCreatedForRegistration(secId, regId, "alexeyraga", "alexey.raga@somewhere.near")
            };
        }
    }
}
