using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure;
using Ziggurat.Registration.Domain.Lookups.LoginIndex;
using Ziggurat.Registration.Domain.Registration;
using Ziggurat.Registration.Domain.Tests;
using Rhino.Mocks;
using Ziggurat.Contracts;

namespace Ziggurar.Registration.Domain.Tests.Registration
{
    [TestClass]
    public sealed class When_create_registration : AggregateTest<RegistrationAggregate>
    {
        [TestMethod]
        public void Should_fail_if_login_taken()
        {
            var regId = RegistrationIdGenerator.NewRegistrationId();
            var regData = new RegistrationData {
                CreatedDate = Now.UtcTime,
                DisplayName = "Alexey Raga",
                Login = "alexeyraga",
                Email = "alexey.raga@somewhere.in",
                Password = "Password123"
            };

            var index = MockRepository.GenerateMock<ILoginIndexLookupService>();
            index.Stub(x => x.IsLoginTaken("alexeyraga")).Return(true);

            When = aggregate => aggregate.CreateRegistration(regId, regData, index);
            Then = new IEvent[] {
                new RegistrationFailed(regId, "alexeyraga", new List<string> {
                        "Username 'alexeyraga' is already taken"
                    })
            };
        }
    }
}
