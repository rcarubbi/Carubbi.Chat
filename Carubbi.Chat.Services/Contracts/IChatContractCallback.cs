using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Carubbi.Chat.Services
{
    [ServiceContract]
    public interface IChatContractCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyConnection(string userName);

        [OperationContract(IsOneWay = true)]
        void NotifyDisconnection(string userName);

        [OperationContract(IsOneWay = true)]
        void NotifyMessage(string message);

        [OperationContract(IsOneWay = true)]
        void ExecuteShutDown();

        [OperationContract(IsOneWay = true)]
        void ExceptionThrown(string exceptionMessage);

        [OperationContract(IsOneWay = true)]
        void ExecuteToogleCdChase();
    }
}
