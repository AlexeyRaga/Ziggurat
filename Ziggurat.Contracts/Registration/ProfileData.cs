using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Registration
{
    public sealed class ProfileData
    {
        public Guid ProfileId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
