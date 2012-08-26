using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Registration
{
    public sealed class AccountData
    {
        public Guid AccountId { get; set; }
        public string DisplayName { get; set; }
    }
}
