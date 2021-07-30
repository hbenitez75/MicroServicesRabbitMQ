using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiBillableTransaction.TransactionManager;

namespace ApiBillableTransaction.Messaging
{
    public interface ISendTransaction
    {
        public void SendTransactionMsg(Movements movements);
    }
}
