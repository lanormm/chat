using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskChat.models;
using System.Collections.ObjectModel;

namespace DeskChat.models
{
    public class GroupRoom : Room
    {
        public GroupRoom()
        {
            Subscribers = new ObservableCollection<UserChat>();
        }

        public byte[] PUBLIC_KEY;
        public byte[] Key, IV;
        
        public UserChat Leader { get; set; }
        public ObservableCollection<UserChat> Subscribers { get; set; }
    }
}
