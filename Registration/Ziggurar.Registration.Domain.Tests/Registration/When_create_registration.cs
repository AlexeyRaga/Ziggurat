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
        [TestCleanup]
        public void Cleanup()
        {
            Now.Reset();
        }

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

        [TestMethod]
        public void Should_create_registration()
        {
            var regId = RegistrationIdGenerator.NewRegistrationId();
            var fakeNow = DateTime.UtcNow;

            Now.SetUtcTime(fakeNow);
            var regData = new RegistrationData
            {
                CreatedDate = Now.UtcTime,
                DisplayName = "Alexey Raga",
                Login = "alexeyraga",
                Email = "alexey.raga@somewhere.in",
                Password = "Password123"
            };

            var securityId = RegistrationIdGenerator.NewSecutiryId(regId);
            var profileId = RegistrationIdGenerator.NewProfileId(regId);

            var profile = new ProfileData(profileId, "Alexey Raga", "alexey.raga@somewhere.in");
            var security = new SecurityData(securityId, "alexeyraga", "alexey.raga@somewhere.in", "Password123");

            var index = MockRepository.GenerateMock<ILoginIndexLookupService>();
            index.Stub(x => x.IsLoginTaken(null)).IgnoreArguments().Return(false);

            When = aggregate => aggregate.CreateRegistration(regId, regData, index);
            Then = new IEvent[] {
                new RegistrationStarted(regId, fakeNow, security, profile)
            };
        }
    }
}
