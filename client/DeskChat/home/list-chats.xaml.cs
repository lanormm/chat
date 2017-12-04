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
using System.Json;
using DeskChat.user;
using DeskChat.group;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using DeskChat.app;

namespace DeskChat.home
{
    /// <summary>
    /// Interaction logic for list_groups.xaml
    /// </summary>

    public class ItemChat
    {
        public String id { get; set; }
        public String alias { get; set; }
        public String iconUrl { get; set; }
        public bool isGroup { get; set; }
    }
    public partial class ListChats : Page
    {
        public enum ItemType { Group, User };
        public delegate void ModelPopulated();
        public event ChangeNavigation navChanged;
        public List<ChatRoom> chats { get; set; }
        public event NewRoom newRoomEvent;
        public ListChats()
        {
            InitializeComponent();
            chatsListView.ItemsSource = Rooms.getInstance().RoomsList;
            usersListView.ItemsSource = Users.getInstance().UserCollection;
        } 

        public void populate()
        {
            //if(chats != null)
            //{
            //    chats.Clear();
            //}            
            //List<ChatRoom> rooms = ChatRooms.getInstance().RoomsList;
            //List<UserChat> users = Users.getInstance().UserList;
            //chats = new List<ChatRoom>(rooms);
            //foreach(UserChat user in users)
            //{
            //    chats.Add(new ChatRoom()
            //    {
            //        Alias = user.Alias,
            //        Id = user.Id,
            //        IconUrl = user.imageUrl,
            //        isGroupRoom = false,
            //        Chat = new Chat()
            //    });
            //}
            //listView.ItemsSource = chats;
        }

        private void leaveHover(object sender, MouseEventArgs e)
        {
            Mouse.SetCursor(Cursors.Arrow);
        }

        private void hover(object sender, MouseEventArgs e)
        {
            Mouse.SetCursor(Cursors.Hand);
        }
        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                Room content = item.Content as Room;
                if (content is GroupRoom)
                {
                    GroupChat chat = new GroupChat((GroupRoom)content);
                    chat.userDropped += new DropUser(SocketConnection.getInstance().dropUser);
                    SocketConnection.getInstance().dropped += new UserAlreadyDropped(chat.userAlreadyDropped);
                    navChanged(chat);
                }
                else
                {
                    navChanged(new UserChatWi((UserRoom)content));
                }
            }
        }

        private void usersListView_UserSelectedEvent(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {                
                UserChat user = (UserChat)item.Content;
                if (Rooms.getInstance().RoomsList.FirstOrDefault(s => s.Id.Equals(user.Id)) == null)
                {
                    UserRoom userRoom = new UserRoom()
                    {
                        Alias = user.Alias,
                        Id = user.Id,
                        IconUrl = user.imageUrl
                    };
                    Rooms.getInstance().RoomsList.Add(userRoom);
                }
                else
                {
                    MessageBox.Show("Já existe uma conversa na sua lista de chats");
                }               
            }
        }

        private void newGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            GroupRoom room = new GroupRoom()
            {
                Alias = texboxNewGroup.Text,
                Id = Guid.NewGuid().ToString().Substring(0, 6),
                Leader = User.getInstance().UserAdp
            };
            ObservableCollection<UserChat> newUsers = Users.getInstance().UserCollection;
            foreach(UserChat n in newUsers)
            {
                room.Subscribers.Add(n);
            }
            Rooms.getInstance().RoomsList.Add(room);
            newRoomEvent(room);            
        }
    }
}
