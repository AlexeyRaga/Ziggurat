using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Registration
{
    public static class RegistrationIdGenerator
    {
        public static readonly Guid SecurityNamespace = new Guid("3F074847-4BA6-4875-AC0C-569097AEC6C1");
        public static readonly Guid ProfileNamespace = new Guid("7FD14B5A-3A35-43B3-9BE5-6033CBD6E89B");

        public static Guid NewSecutiryId(string login)
        {
            return GuidGenerator.Create(SecurityNamespace, login);
        }

        public static Guid NewProfileId(string login)
        {
            return GuidGenerator.Create(ProfileNamespace, login);
        }
    }
}
