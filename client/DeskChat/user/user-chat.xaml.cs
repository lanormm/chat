using DeskChat.app;
using DeskChat.home;
using DeskChat.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeskChat.user
{
    /// <summary>
    /// Interaction logic for user_chat.xaml
    /// </summary>
    public partial class UserChatWi : Page
    {
        public UserRoom model { get; set; }
        public event SendMessage messageSent;
        private SocketConnection sock;
        public UserChatWi(UserRoom item)
        {
            InitializeComponent();
            this.model = item;
            sock = SocketConnection.getInstance();
            messageSent += new SendMessage(sock.sendMessage);
            this.DataContext = new
            {
                Model = model
            };
        }
        

        private void btnSendUser_Click(object sender, RoutedEventArgs e)
        {

            messageSent(chatMessageTextBox.Text, model);
            chatMessageTextBox.Text = null;
        }
    }
}
