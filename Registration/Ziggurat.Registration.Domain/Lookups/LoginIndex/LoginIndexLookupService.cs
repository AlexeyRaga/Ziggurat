using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Registration.Domain.Lookups.LoginIndex
{
    public interface ILoginIndexLookupService
    {
        bool IsLoginTaken(string login);
    }

    public sealed class LoginIndexLookupService : ILoginIndexLookupService
    {
        private readonly IDocumentReader<byte, LoginIndexLookup> _reader;

        public LoginIndexLookupService(IDocumentStore storeFactory)
        {
            if (storeFactory == null) throw new ArgumentNullException("storeFactory");
            _reader = storeFactory.GetReader<byte, LoginIndexLookup>();
        }

        public bool IsLoginTaken(string login)
        {
            var partition = Partition.GetPartition(login);

            LoginIndexLookup index;
            if (!_reader.TryGet(partition, out index)) return false;
            return index.Logins.ContainsKey(login);
        }
    }
}
