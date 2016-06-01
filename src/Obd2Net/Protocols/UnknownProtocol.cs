using System;
using Obd2Net.InfrastructureContracts;

namespace Obd2Net.Protocols
{
    public class UnknownProtocol : ProtocolBase
    {
        public UnknownProtocol()
            : base(null)
        {
        }

        protected override int TxIdEngine => 0;

        public override string ElmName => "Unknown";

        public override string ElmId => "U";

        public override bool ParseFrame(IFrame frame)
        {
            throw new NotImplementedException();
        }

        public override bool ParseMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}