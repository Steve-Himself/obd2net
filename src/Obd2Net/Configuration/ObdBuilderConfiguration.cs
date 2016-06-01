using Obd2Net.InfrastructureContracts.DependencyInjection;
using Obd2Net.InfrastructureContracts.Protocols;
using Obd2Net.Protocols;

namespace Obd2Net.Configuration
{
    public class ObdBuilderConfiguration
    {
        internal ObdBuilderConfiguration()
        {
        }

        internal IDependencyResolver DependencyResolver { get; set; }

        public ObdBuilderConfiguration WithDependencyResolver(IDependencyResolver dependencyResolver)
        {
            DependencyResolver = dependencyResolver;
            return this;
        }

        internal IProtocol Protocol { get; set; }
        public ObdBuilderConfiguration WithProtocol<T>() where T: IProtocol, new()
        {
            Protocol = new T();
            return this;
        }

        public ObdBuilderConfiguration WithProtocol(Protocol protocol)
        {
            
            return this;
        }

        public ObdBuilderConfiguration WithAutoProtocol()
        {
            Protocol = null;
            return this;
        }
    }
}