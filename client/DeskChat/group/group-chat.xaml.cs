using DeskChat.app;
using DeskChat.home;
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
using DeskChat.models;
namespace DeskChat.group
{
    /// <summary>
    /// Interaction logic for group_chat.xaml
    /// </summary>
    public delegate void UpdateChatMessage();
    public delegate void UserAlreadyDropped();
    public partial class GroupChat : Page
    {
        SocketConnection sock;
        private GroupRoom item;
        public event SendMessage messageSent;
        public event DropUser userDropped;
        public GroupChat(GroupRoom item)
        {
            InitializeComponent();
            sock = SocketConnection.getInstance();
            this.item = item;
            messageSent += new SendMessage(sock.sendMessage);
            UserChat temp = item.Subscribers.FirstOrDefault(x => x.Alias.Equals("Você"));
            if (temp != null)
            {
                item.Subscribers.Remove(temp);
            }
            item.Subscribers.Add(new UserChat()
            {
                Alias = "Você",
                Id = User.getInstance().Id
            });
            usersConversationView.ItemsSource = item.Subscribers;
            chats.ItemsSource = item.Chat.col;
            this.DataContext = new
            {
                room = item
            };
        }

        public void userAlreadyDropped()
        {
            textBox1.IsReadOnly = true;
            MessageBox.Show("Você foi excluído dessa sala");
        }

        private void enviar(object sender, RoutedEventArgs e)
        {
            messageSent(textBox1.Text, item);
            textBox1.Text = null;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                UserChat user = item.Content as UserChat;
                if(this.item.Leader.Id == User.getInstance().Id)
                {
                    userDropped(this.item.Id, user);
                }                
            }
        }
    }
}
