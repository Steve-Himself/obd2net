using System;
using Obd2Net.InfrastructureContracts;
using Obd2Net.InfrastructureContracts.Protocols;

namespace Obd2Net.Configuration
{
    public class ObdBuilderConfiguration
    {
        internal IOBDConfiguration Config { get; set; }

        internal ObdBuilderConfiguration()
        {
            Config = new DefaultOBDConfiguration();
        }

        //internal IDependencyResolver DependencyResolver { get; set; }
        public ObdBuilderConfiguration WithDefaults()
        {
            // TODO: Add defaults
            return this;
        }

        //public ObdBuilderConfiguration WithDependencyResolver(IDependencyResolver dependencyResolver)
        //{
        //    DependencyResolver = dependencyResolver;
        //    return this;
        //}

        internal IProtocol Protocol { get; set; }

        public ObdBuilderConfiguration WithProtocol<T>() where T: IProtocol, new()
        {
            Protocol = new T();
            return this;
        }

        public ObdBuilderConfiguration WithLogger(ILogger logger)
        {
            Logger = logger;
            return this;
        }

        internal ILogger Logger { get; set; }

        public ObdBuilderConfiguration WithAutoProtocol()
        {
            Protocol = null;
            return this;
        }

        public ObdBuilderConfiguration WithConfig(IOBDConfiguration configuration)
        {
            Config = configuration;
            return this;
        }

        public ObdBuilderConfiguration WithPort(string port)
        {
            Config = new OBDConfiguration(port, Config.Baudrate, Config.Timeout, Config.Fast);
            return this;
        }

        public ObdBuilderConfiguration WithBaudrate(int baudrate)
        {
            Config = new OBDConfiguration(Config.Portname, baudrate, Config.Timeout, Config.Fast);
            return this;
        }

        public ObdBuilderConfiguration WithTimeout(TimeSpan timeout)
        {
            Config = new OBDConfiguration(Config.Portname, Config.Baudrate, timeout, Config.Fast);
            return this;
        }
    }
}