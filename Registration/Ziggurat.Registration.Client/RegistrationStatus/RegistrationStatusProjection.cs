using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Registration;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Registration.Client.RegistrationStatus
{
    public sealed class RegistrationStatusProjection
    {
        private IProjectionWriter<Guid, RegistrationStatusView> _writer;
        public RegistrationStatusProjection(IProjectionStoreFactory factory)
        {
            _writer = factory.GetWriter<Guid, RegistrationStatusView>();
        }

        public void When(RegistrationStarted evt)
        {
            _writer.Add(evt.RegistrationId, new RegistrationStatusView
            {
                Status = RegistrationProcessStatus.InProgress,
                Login = evt.Security.Login,
                DisplayName = evt.Profile.DisplayName,
                Email = evt.Security.Email,
                Errors = new List<string>()
            });
        }

        public void When(RegistrationFailed evt)
        {
            _writer.AddOrUpdate(evt.RegistrationId, view =>
            {
                view.Status = RegistrationProcessStatus.Failed;
                view.Errors = evt.Errors.ToList();
            });
        }

        public void When(RegistrationCompleted evt)
        {
            _writer.AddOrUpdate(evt.RegistrationId, view =>
            {
                view.Errors = new List<string>();
                view.Status = RegistrationProcessStatus.Successful;
            });
        }
    }
}
