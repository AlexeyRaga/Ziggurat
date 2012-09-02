using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Client.Setup.ProjectionRebuilder
{
    public sealed class TypeSignatureBuilder
    {
        public static string GetSignatureForType(Type type)
        {
            var descriptor = GetClassDescriptor(type);
            var signature = GenerateSignature(descriptor);

            return signature;
        }

        private static string GenerateSignature(string value)
        {
            using (var md5 = MD5.Create())
            {
                var srcBytes = Encoding.UTF8.GetBytes(value);
                var hash = md5.ComputeHash(srcBytes);

                var resultBuilder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                    resultBuilder.Append(hash[i].ToString("X2"));

                return resultBuilder.ToString();
            }
        }

        private static string GetClassDescriptor(Type typeToInspect)
        {
            var location = typeToInspect.Assembly.Location;
            var module = ModuleDefinition.ReadModule(location);
            var descriptorBuilder = new StringBuilder();


            var typeDefinition = module.GetType(typeToInspect.FullName);
            descriptorBuilder.AppendLine(typeDefinition.Name);
            ProcessMembers(descriptorBuilder, typeDefinition);

            // we include nested types
            foreach (var nested in typeDefinition.NestedTypes)
            {
                ProcessMembers(descriptorBuilder, nested);
            }

            return descriptorBuilder.ToString();
        }

        private static void ProcessMembers(StringBuilder descriptorBuilder, TypeDefinition typeDefinition)
        {
            foreach (var field in typeDefinition.Fields.OrderBy(f => f.ToString()))
            {
                descriptorBuilder.AppendLine("  " + field);
            }

            foreach (var md in typeDefinition.Methods.OrderBy(m => m.ToString()))
            {
                descriptorBuilder.AppendLine("  " + md);

                foreach (var instruction in md.Body.Instructions)
                {
                    // we don't care about offsets
                    instruction.Offset = 0;
                    descriptorBuilder.AppendLine("    " + instruction);
                }
            }
        }
    }
}
