using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Obd2Net.Collections;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.DependencyInjection;

namespace Obd2Net.Infrastructure.DependencyResolution
{
    public class DependencyResolver : TrackingScope, IDependencyResolver
    {
        private readonly ReadOnlyDictionary<Type, object> _emptyScopedInstances = new ReadOnlyDictionary<Type, object>(new Dictionary<Type, object>());
        private readonly ThreadSafeLazy<Type[]> _resolvableTypes;
        private readonly ITypeProvider _typeProvider;

        public DependencyResolver(ITypeProvider typeProvider)
        {
            _typeProvider = typeProvider;
            _resolvableTypes = new ThreadSafeLazy<Type[]>(ScanForResolvableTypes);
        }

        public IDependencyResolverScope CreateChildScope()
        {
            var childScope = new DependencyResolverScope(_resolvableTypes.Value, _emptyScopedInstances);
            Track(childScope);
            return childScope;
        }

        private Type[] ScanForResolvableTypes()
        {
            return new Type[0]
                .Union(_typeProvider.ProtocolTypes)
                .Union(_typeProvider.ResolverTypes)
                .Union(_typeProvider.CommandTypes)
                .ToArray();
        }
    }
}