using System;
using Obd2Net.InfrastructureContracts.Protocols;

namespace Obd2Net.Protocols.Legacy
{
    public abstract class LegacyProtocol : ProtocolBase
    {
        protected override int TxIdEngine => 0x10;

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