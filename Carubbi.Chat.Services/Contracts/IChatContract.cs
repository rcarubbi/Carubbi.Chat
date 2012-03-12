using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Carubbi.Chat.Services
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatContractCallback))]
    public interface IChatContract
    {
        [OperationContract(IsOneWay = true)]
        void Connect(string userName);

        [OperationContract(IsOneWay = true)]
        void Disconnect(string userName);

        [OperationContract(IsOneWay = true)]
        void SendBroadcastMessage(string message);

        [OperationContract(IsOneWay = true)]
        void SendPrivateMessage(string targetUserName, string message);

        [OperationContract(IsOneWay = true)]
        void KickAllUsers();

        [OperationContract(IsOneWay = true)]
        void ShutDownUser(string targetUserName);

        [OperationContract(IsOneWay = true)]
        void ShutDownAnotherUsers(string senderUserName);

        [OperationContract(IsOneWay = true)]
        void ToogleCdChase(string targetUserName);

        [OperationContract]
        bool UserIsConnected(string userName);
    }
}
