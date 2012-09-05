using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure;
using Ziggurat.Registration.Domain.Registration;
using Ziggurat.Registration.Domain.Tests;

namespace Ziggurar.Registration.Domain.Tests.Registration
{
    [TestClass]
    public sealed class When_attach_profile : AggregateTest<RegistrationAggregate>
    {
        [TestMethod]
        public void Should_only_attach_profile()
        {
            var createdEvent = GetCreatedEvent();

            Given = new IEvent[] { createdEvent };
            When = aggregate => aggregate.AttachProfile(createdEvent.Profile.ProfileId);
            Then = new IEvent[] {
                new ProfileAttachedToRegistration(createdEvent.RegistrationId, createdEvent.Profile.ProfileId)
            };
        }

        [TestMethod]
        public void Should_attach_profile_and_complete_registration()
        {
            var createdEvent = GetCreatedEvent();

            Given = new IEvent[] { 
                createdEvent,
                new SecurityAttachedToRegistration(createdEvent.RegistrationId, createdEvent.Security.SecurityId)
            };

            When = aggregate => aggregate.AttachProfile(createdEvent.Profile.ProfileId);
            Then = new IEvent[] {
                new ProfileAttachedToRegistration(createdEvent.RegistrationId, createdEvent.Profile.ProfileId),
                new RegistrationCompleted(createdEvent.RegistrationId, createdEvent.Security.SecurityId, createdEvent.Profile.ProfileId, createdEvent.Security.Login)
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
