using System;
using System.Collections.Generic;
using System.Linq;
using Obd2Net.Extensions;

namespace Obd2Net.Infrastructure.DependencyResolution
{
    public abstract class TrackingScope
    {
        private readonly List<object> _components = new List<object>();
        private readonly object _mutex = new object();
        private bool _disposed;

        protected virtual void Track(object component)
        {
            lock (_mutex)
            {
                _components.Add(component);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_disposed) return;
            _disposed = true;

            try
            {
                _components.OfType<IDisposable>()
                           .Do(c => c.Dispose())
                           .Done();
            }
            catch (Exception)
            {
            }
        }
    }
}