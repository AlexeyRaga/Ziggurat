using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Definition
{
    public static class DefinitionIdGenerator
    {
        public static readonly Guid ProjectNamespace = new Guid("8F897DBA-17F6-4ADB-8751-D05BCB23F7BE");
        public static readonly Guid FormNamespace = new Guid("941691C2-DB1D-4B0E-AB48-060FD11C7734");
        public static readonly Guid ProjectLayoutNamespace = new Guid("19DE97E2-B5D3-4825-A19C-C8EC0D3E8B45");

        public static Guid NewProjectId(string shortName)
        {
            return GuidGenerator.Create(ProjectNamespace, shortName);
        }

        public static Guid NewFormId()
        {
            return Guid.NewGuid();
        }

        public static Guid NewProjectLayoutId(Guid projectId)
        {
            return GuidGenerator.Create(ProjectLayoutNamespace, projectId.ToString());
        }
    }
}
