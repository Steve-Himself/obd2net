using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Obd2Net.InfrastructureContracts.DependencyInjection;

namespace Obd2Net.Infrastructure.DependencyResolution
{
    public class DependencyResolverScope : TrackingScope, IDependencyResolverScope
    {
        private readonly Type[] _componentTypes;
        private readonly IDictionary<Type, object> _scopedInstances;

        public DependencyResolverScope(Type[] componentTypes, IReadOnlyDictionary<Type, object> parentScopedInstances)
        {
            _componentTypes = componentTypes;
            _scopedInstances = parentScopedInstances.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public IDependencyResolverScope CreateChildScope()
        {
            var childScope = new DependencyResolverScope(_componentTypes, new ReadOnlyDictionary<Type, object>(_scopedInstances));
            Track(childScope);
            return childScope;
        }

        public TComponent Resolve<TComponent>()
        {
            var componentType = typeof(TComponent);
            return (TComponent) Resolve(componentType);
        }

        public object Resolve(Type componentType)
        {
            object scopedInstance;
            if (_scopedInstances.TryGetValue(componentType, out scopedInstance)) return scopedInstance;

            var component = CreateInstance(componentType);
            Track(component);

            return component;
        }

        private object CreateInstance(Type componentType)
        {
            try
            {
                var result = Activator.CreateInstance(componentType);
                return result;
            }
            catch (Exception exc)
            {
                var message = "The {0} can only broker messages to handlers that have default constructors (i.e. ones with no parameters).";

                throw new Exception(message, exc);
            }
        }
    }
}