using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Registration
{
    [Serializable]
    public sealed class ProfileData
    {
        public Guid ProfileId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public ProfileData() { }
        public ProfileData(Guid id, string displayName, string email)
        {
            ProfileId = id;
            DisplayName = displayName;
            Email = email;
        }
    }
}
