using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure;
using Ziggurat.Registration.Domain.Lookups.LoginIndex;
using Ziggurat.Registration.Domain.Registration;
using Ziggurat.Registration.Domain.Tests;

namespace Ziggurar.Registration.Domain.Tests.Registration
{
    [TestClass]
    public sealed class When_attach_security : AggregateTest<RegistrationAggregate>
    {
        [TestMethod]
        public void Should_only_attach_security()
        {
            var index = MockRepository.GenerateMock<ILoginIndexLookupService>();

            var createdEvent = GetCreatedEvent();

            Given = new IEvent[] { createdEvent };
            When = aggregate => aggregate.AttachSecurity(createdEvent.Security.SecurityId, index);
            Then = new IEvent[] {
                new SecurityAttachedToRegistration(createdEvent.RegistrationId, createdEvent.Security.SecurityId)
            };
        }

        [TestMethod]
        public void Should_attach_security_and_complete_registration()
        {
            var index = MockRepository.GenerateMock<ILoginIndexLookupService>();
            index.Stub(x => x.IsLoginTaken("alexeyraga")).Return(false);

            var createdEvent = GetCreatedEvent();

            Given = new IEvent[] { 
                createdEvent,
                new ProfileAttachedToRegistration(createdEvent.RegistrationId, createdEvent.Profile.ProfileId)
            };

            When = aggregate => aggregate.AttachSecurity(createdEvent.Security.SecurityId, index);
            Then = new IEvent[] {
                new SecurityAttachedToRegistration(createdEvent.RegistrationId, createdEvent.Security.SecurityId),
                new RegistrationCompleted(createdEvent.RegistrationId, createdEvent.Security.SecurityId, createdEvent.Profile.ProfileId, createdEvent.Security.Login)
            };
        }

        [TestMethod]
        public void Should_attach_security_and_fail_registration_if_login_taken()
        {
            var index = MockRepository.GenerateMock<ILoginIndexLookupService>();
            index.Stub(x => x.IsLoginTaken("alexeyraga")).Return(true);

            var createdEvent = GetCreatedEvent();

            Given = new IEvent[] { 
                createdEvent,
                new ProfileAttachedToRegistration(createdEvent.RegistrationId, createdEvent.Profile.ProfileId)
            };

            When = aggregate => aggregate.AttachSecurity(createdEvent.Security.SecurityId, index);
            Then = new IEvent[] {
                new SecurityAttachedToRegistration(createdEvent.RegistrationId, createdEvent.Security.SecurityId),
                new RegistrationFailed(createdEvent.RegistrationId, "alexeyraga", new List<string> { "Username 'alexeyraga' is already taken" })
            };
        }

        private RegistrationStarted GetCreatedEvent()
        {
            var regId = RegistrationIdGenerator.NewRegistrationId();

            var securityId = RegistrationIdGenerator.NewSecutiryId(regId);
            var profileId = RegistrationIdGenerator.NewProfileId(regId);

            var profile = new ProfileData(profileId, "Alexey Raga", "alexey.raga@somewhere.in");
            var security = new SecurityData(securityId, "alexeyraga", "alexey.raga@somewhere.in", "Password123");

            return new RegistrationStarted(regId, Now.UtcTime, security, profile);
        }
    }
}
