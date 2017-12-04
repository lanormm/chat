using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskChat.models
{
    public class Users
    {
        private Users() {
            UserList = new List<UserChat>();
            UserCollection = new ObservableCollection<UserChat>();
        }
        private static Users users;

        public List<UserChat> UserList { get; set; }
        public ObservableCollection<UserChat> UserCollection { get; set; }
        public static Users getInstance()
        {
            if(users == null)
            {
                users = new Users();                
            }
            return users;
        }
    }
}
