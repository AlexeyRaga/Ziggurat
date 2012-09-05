using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Registration.Client.RegistrationStatus
{
    [Serializable]
    public sealed class RegistrationStatusView
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public List<string> Errors { get; set; }

        public RegistrationProcessStatus Status { get; set; }
    }

    public enum RegistrationProcessStatus
    {
        InProgress = 0,
        Successful = 1,
        Failed = 2
    }
}
