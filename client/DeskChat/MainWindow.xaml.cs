using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using DeskChat.app;
using DeskChat.home;
using DeskChat.Properties;
using System.Json;
using DeskChat.models;

namespace DeskChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public delegate void UserChanged(object sender, EventArgs e);
    public delegate void ChangeNavigation(Page p);
    
    public partial class MainWindow : Window
    {
        public event TriggerUpdateUserStatus statusChanged;

        protected void userChanged(object sender,EventArgs e)
        {
            statusTextLabel.Visibility = Visibility.Visible;
            comboStatus.Visibility = Visibility.Visible;
            if (listChats == null)
            {
                listChats = new ListChats();
                listChats.navChanged += new ChangeNavigation(changeNavigation);                
            }
            if (sock == null)
            {
                sock = SocketConnection.getInstance();
                sock.modelPopulatedChanged += new ListChats.ModelPopulated(listChats.populate);
                this.statusChanged += new TriggerUpdateUserStatus(sock.updateUserStatus);
                listChats.newRoomEvent += new NewRoom(sock.createGroup);
            }
            
            frame.Navigate(listChats);
            nameTextBox.Content = User.getInstance().Alias;            
        }

        AliasSelector aliasSelector;
        ListChats listChats;

        SocketConnection sock;
        public MainWindow()
        {
            InitializeComponent();
            aliasSelector = new AliasSelector();
            aliasSelector.changedUser += new UserChanged(userChanged);
            frame.Navigate(aliasSelector);
            comboStatus.ItemsSource = Enum.GetValues(typeof(UserStatus)).Cast<UserStatus>().ToList();
        }        
        


        private void minimiza(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void fecha(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void fechar(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("Fechando socket...");
            if(sock != null)
            {
                try
                {
                    sock.close();
                    sock.cancel();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
        }
        

        public void changeNavigation(Page page)
        {
            this.frame.Navigate(page);
        }

        private void changed(object sender, SelectionChangedEventArgs e)
        {
            UserStatus status = (UserStatus)comboStatus.Items[comboStatus.SelectedIndex];
            User.getInstance().Status = status;            
            statusChanged(status);
        }
    }
}
