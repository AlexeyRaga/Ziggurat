﻿using System;
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

        public static Guid NewRegistrationId()
        {
            return Guid.NewGuid();
        }

        public static Guid NewSecutiryId(Guid parentId)
        {
            return GuidGenerator.Create(SecurityNamespace, parentId.ToString());
        }

        public static Guid NewProfileId(Guid parentId)
        {
            return GuidGenerator.Create(ProfileNamespace, parentId.ToString());
        }
    }
}
