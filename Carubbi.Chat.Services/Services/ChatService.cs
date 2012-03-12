using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Carubbi.Chat.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatContract
    {

        private const int MAX_USERS = 50;
        private static Dictionary<string, IChatContractCallback> _clients = new Dictionary<string, IChatContractCallback>();

        IChatContractCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IChatContractCallback>();
            }
        }

        #region IChatContract Members
        public void Connect(string userName)
        {
            if (Callback != null)
            {
                try
                {
                    if (_clients.Keys.Count > MAX_USERS)
                        throw new ApplicationException(string.Format("Este servidor atingiu o limite máximo de {0} usuários conectados. Tente novamente mais tarde.", MAX_USERS));

                    lock (_clients)
                        _clients.Add(userName, Callback);

                    foreach (KeyValuePair<string, IChatContractCallback> client in _clients)
                    {
                        Callback.NotifyConnection(client.Key);
                        if (client.Key != userName)
                        {
                            try
                            {
                                client.Value.NotifyConnection(userName);
                            }
                            catch (Exception ex)
                            {
                                lock (_clients)
                                    _clients.Remove(client.Key);

                                Callback.ExceptionThrown(ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Callback.ExceptionThrown(ex.Message);
                }
            }
        }

        public void Disconnect(string userName)
        {
            if (Callback != null)
            {

                foreach (KeyValuePair<string, IChatContractCallback> client in _clients)
                {
                    try
                    {
                        if (_clients.ContainsKey(userName))
                            _clients[userName].NotifyDisconnection(client.Key);
                        if (client.Key != userName)
                            client.Value.NotifyDisconnection(userName);
                    }
                    catch (Exception ex)
                    {
                        lock (_clients)
                            _clients.Remove(client.Key);
                        Callback.ExceptionThrown(ex.Message);
                    }
                }

                lock (_clients)
                    _clients.Remove(userName);
            }

        }

        public void SendBroadcastMessage(string message)
        {
            if (Callback != null)
            {
                foreach (KeyValuePair<string, IChatContractCallback> client in _clients)
                {
                    try
                    {
                        client.Value.NotifyMessage(message);
                    }
                    catch (Exception ex)
                    {
                        lock (_clients)
                            _clients.Remove(client.Key);
                        Callback.ExceptionThrown(ex.Message);
                    }
                }
            }
        }

        public void SendPrivateMessage(string targetUserName, string message)
        {
            if (Callback != null)
            {
                Callback.NotifyMessage(message);

                try
                {
                    _clients[targetUserName].NotifyMessage(message);
                }
                catch (Exception ex)
                {
                    lock (_clients)
                        _clients.Remove(targetUserName);
                    Callback.ExceptionThrown(ex.Message);
                }
            }
        }

        public void KickAllUsers()
        {
            foreach (KeyValuePair<string, IChatContractCallback> clientToNotify in _clients)
            {
                foreach (KeyValuePair<string, IChatContractCallback> client in _clients)
                {
                    try
                    {
                        clientToNotify.Value.NotifyDisconnection(client.Key);
                    }
                    catch (Exception ex)
                    {
                        Callback.ExceptionThrown(ex.Message);
                    }
                }
            }

            lock (_clients)
                _clients.Clear();
        }

        public void ShutDownUser(string targetUserName)
        {
            if (Callback != null)
            {
                if (_clients.ContainsKey(targetUserName))
                {
                    try
                    {
                        _clients[targetUserName].ExecuteShutDown();
                    }
                    catch (Exception ex)
                    {
                        lock (_clients)
                            _clients.Remove(targetUserName);

                        Callback.ExceptionThrown(ex.Message);
                    }
                }
                Disconnect(targetUserName);
            }
        }

        public void ShutDownAnotherUsers(string senderUserName)
        {
            if (Callback != null)
            {
                foreach (KeyValuePair<string, IChatContractCallback> client in _clients)
                {
                    if (client.Key != senderUserName)
                    {
                        try
                        {
                            client.Value.ExecuteShutDown();
                        }
                        catch (Exception ex)
                        {
                            lock (_clients)
                                _clients.Remove(client.Key);
                            Callback.ExceptionThrown(ex.Message);
                        }
                    }
                }
            }
        }

        public void ToogleCdChase(string targetUserName)
        {
            if (Callback != null)
            {
                try
                {
                    _clients[targetUserName].ExecuteToogleCdChase();
                }
                catch (Exception ex)
                {
                    lock (_clients)
                        _clients.Remove(targetUserName);
                    Callback.ExceptionThrown(ex.Message);
                }
            }
        }

        public bool UserIsConnected(string userName)
        {
            return _clients.Keys.Contains(userName);
        }
        #endregion
    }
}
