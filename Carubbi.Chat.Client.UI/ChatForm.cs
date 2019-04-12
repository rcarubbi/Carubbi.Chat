using Carubbi.Chat.Client.UI.ChatService;
using Carubbi.Chat.Client.UI.Properties;
using Carubbi.Google.TTS;
using System;
using System.ServiceModel;
using System.Windows.Forms;


namespace Carubbi.Chat.Client.UI
{
    public partial class ChatForm : Form, IChatContractCallback
    {

        #region Membros Privados

        private const string COMANDO_KICK = "kick";
        private const string COMANDO_SHUTDOWN = "shutdown";
        private const string COMANDO_TOOGLE_CD = "tooglecd";

        private void DoExitWin(int flg)
        {
            bool ok;
            TokPrivlLuid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);
            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            ok = ExitWindowsEx(flg, 0);
        }

        private void ToogleCd()
        {
            if (!_isCdOpened)
                mciSendString("set cdaudio door open", null, 0, IntPtr.Zero);
            else
                mciSendString("set cdaudio door closed", null, 0, IntPtr.Zero);

            _isCdOpened = !_isCdOpened;
        }

        private void Falar(string mensagem)
        {
            if (chkFalar.Checked)
            {
                string mensagemLimpa = string.Empty;
                string intro = string.Empty;
                string corpoMensagem = string.Empty;
                if (mensagem.Contains("-"))
                {
                    string[] mensagens = mensagem.Split('-');
                    mensagens[0] = string.Empty;
                    mensagemLimpa = String.Join("-", mensagens);
                    string[] partesMensagemLimpa = mensagemLimpa.Split(':');
                    intro = partesMensagemLimpa[0];
                    partesMensagemLimpa[0] = string.Empty;
                    corpoMensagem = string.Join(":", partesMensagemLimpa);

                }
                GoogleTTS.Play($"{intro} {corpoMensagem}", Language.BrazilianPortuguese);
            }
        }

        private void ExibirAlerta(string titulo, string mensagem, ToolTipIcon icone)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.ShowBalloonTip(5000, titulo, mensagem, icone);
            }
        }

        private bool IsConnected(string userName)
        {
            return Proxy.UserIsConnected(userName);
        }

        private bool ExecutarComamdos(string mensagem, string usuarioSelecionado)
        {
            bool comandoEncontrado = true;
           
            if (mensagem == COMANDO_KICK)
            {
                if (string.IsNullOrEmpty(usuarioSelecionado))
                {
                    Proxy.SendBroadcastMessage(string.Format("{0} - Reiniciando o servidor... favor logar novamente.", DateTime.Now));
                    Proxy.KickAllUsers();
                }
                else
                {
                    Proxy.SendPrivateMessage(usuarioSelecionado, string.Format("{0} - {1} foi kickado por {2}", DateTime.Now, usuarioSelecionado, _userName));
                    Proxy.Disconnect(usuarioSelecionado);
                }
            }
            else if (mensagem == COMANDO_SHUTDOWN)
            {
                if (string.IsNullOrEmpty(usuarioSelecionado))
                {
                    Proxy.SendBroadcastMessage(string.Format("{0| - {1} solicitou o desligamento de todas as máquinas", DateTime.Now, _userName));
                    Proxy.ShutDownAnotherUsers(_userName);
                }
                else
                {
                    Proxy.SendPrivateMessage(usuarioSelecionado, string.Format("{0} - {1} solicitou o desligamento da sua máquina",DateTime.Now, _userName));
                    Proxy.ShutDownUser(usuarioSelecionado);
                }

            }
            else if (mensagem == COMANDO_TOOGLE_CD)
            {
                if (!string.IsNullOrEmpty(usuarioSelecionado))
                {
                    Proxy.SendPrivateMessage(usuarioSelecionado, string.Format("{0} - {1} manipulou drive de CD de {2}",DateTime.Now, _userName, usuarioSelecionado));
                    Proxy.ToogleCdChase(usuarioSelecionado);
                }
            }
            else
                comandoEncontrado = false;

            return comandoEncontrado;


        }

        private string _userName;
        private bool _isConnected;
        private bool _isCdOpened;
        private InstanceContext _instanceContext;
        private ChatContractClient _proxy;
       
        #endregion
        
        public ChatForm()
        {
            InitializeComponent();
            _instanceContext = new InstanceContext(this);

        }

        public ChatContractClient Proxy
        {
            get 
            {
                if (_proxy == null)
                {
                    _proxy = new ChatContractClient(_instanceContext);
                    _proxy.Open();
                }

                if (_proxy.State != CommunicationState.Opened && _proxy.State != CommunicationState.Opening)
                {
                    if (_proxy.State != CommunicationState.Closed &&
                        _proxy.State != CommunicationState.Closing &&
                        _proxy.State != CommunicationState.Faulted)
                    {
                        _proxy.Close();
                    }
                    _proxy = null;
                    _proxy = new ChatContractClient(_instanceContext);
                    _proxy.Open();
                }
                return _proxy;
            }
            set {
                _proxy = value;
            }
        }

        protected void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        protected void btnSend_Click(object sender, System.EventArgs e)
        {
            string mensagem = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(mensagem))
            {
                string usuarioSelecionado = lstUsers.SelectedItem == null ? string.Empty : lstUsers.SelectedItem.ToString();
                if (!ExecutarComamdos(mensagem, usuarioSelecionado))
                {
                    if (string.IsNullOrEmpty(usuarioSelecionado))
                        Proxy.SendBroadcastMessage(string.Format("{0} - {1} fala para todos: {2}", DateTime.Now, _userName, mensagem));
                    else
                        Proxy.SendPrivateMessage(usuarioSelecionado, string.Format("{0} - {1} fala reservadamente para {2}: {3}", DateTime.Now, _userName, usuarioSelecionado, mensagem));
                }
                txtMessage.Text = string.Empty;
                txtMessage.Focus();
            }
        }

     
        protected void picToogleConnect_Click(object sender, EventArgs e)
        {
            if (_isConnected)
            {
                Proxy.Disconnect(_userName);
                btnSend.Enabled = false;
                txtLogin.Enabled = true;
                picToogleConnect.BackgroundImage = Resources.logoff;
                picToogleConnect.BackgroundImageLayout = ImageLayout.Stretch;
                this.tooltip.SetToolTip(picToogleConnect, "Conectar");
                _isConnected = false;
            }
            else
            {
                _userName = txtLogin.Text;
                if (string.IsNullOrEmpty(_userName))
                    MessageBox.Show(this,"Digite um nome de usuário para logar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                { 
                    if (!IsConnected(_userName))
                    {
                        btnSend.Enabled = true;
                        txtLogin.Enabled = false;
                        Proxy.Connect(_userName);
                        picToogleConnect.BackgroundImage = Resources.loggin;
                        picToogleConnect.BackgroundImageLayout = ImageLayout.Stretch;
                        this.tooltip.SetToolTip(picToogleConnect, "Desconectar");
                        _isConnected = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "Já existe outro usuário logado com este nome", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

        }

        protected void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_proxy != null)
            {
                if (!string.IsNullOrEmpty(_userName) && IsConnected(_userName))
                    Proxy.Disconnect(_userName);

                while (_proxy.State == CommunicationState.Opened)
                {
                    try
                    {
                        Proxy.Close();
                    }
                    catch
                    { }
                }

                Proxy = null;
            
            }
        }

        protected void ChatForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        protected void picBroadcast_Click(object sender, EventArgs e)
        {
            lstUsers.SelectedItem = null;
        }

        #region Callback Members

        public void NotifyConnection(string userName)
        {
            lstUsers.Items.Add(userName);
            string mensagem = string.Format("{0} entrou", userName);
            ExibirAlerta("Alerta de entrada",mensagem , ToolTipIcon.Warning);
            if (userName != _userName)
                Falar(mensagem);
        }

        public void NotifyDisconnection(string userName)
        {
            lstUsers.Items.Remove(userName);
            string mensagem = string.Format("{0} saiu", userName);
            ExibirAlerta("Alerta de saída", mensagem, ToolTipIcon.Warning);
            if (_isConnected && txtLogin.Text == userName)
                picToogleConnect_Click(this, EventArgs.Empty);

            if (userName != _userName)
                Falar(mensagem);
        }

        public void NotifyMessage(string message)
        {
            txtChat.Text += string.Concat(message, Environment.NewLine);
            txtChat.SelectionStart = txtChat.Text.Length;
            txtChat.ScrollToCaret();
            ExibirAlerta("Nova mensagem", message, ToolTipIcon.Info);
            Falar(message);
        }

        public void ExecuteShutDown()
        {
            DoExitWin(EWX_SHUTDOWN);
        }

        public void ExceptionThrown(string exceptionMessage)
        {
            MessageBox.Show(this, exceptionMessage, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ExecuteToogleCdChase()
        {
            ToogleCd();
        }
        #endregion

        
    }
}
