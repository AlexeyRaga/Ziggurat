using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure
{
    public static class Partition
    {
        public static byte GetPartition(string value)
        {
            using (var md = new MD5CryptoServiceProvider())
            {
                var bytes = Encoding.UTF8.GetBytes(value.ToLowerInvariant());
                return md.ComputeHash(bytes)[0];
            }
        }
    }
}
