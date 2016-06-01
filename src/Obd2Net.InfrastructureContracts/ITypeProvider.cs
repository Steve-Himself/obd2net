using System;
using System.Collections.Generic;

namespace Obd2Net.InfrastructureContracts
{
    public interface ITypeProvider
    {
        IEnumerable<Type> ResolverTypes { get; }
        IEnumerable<Type> CommandTypes { get; }
        IEnumerable<Type> ProtocolTypes { get; set; }
    }
}