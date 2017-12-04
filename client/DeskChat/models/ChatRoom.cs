using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DeskChat.models
{
    public class ChatRoom
    {
        public String Alias { get; set; }
        public String Id { get; set; }
        public String IconUrl { get; set; }
        public Boolean isGroupRoom { get; set; }

        public Chat Chat { get; set; }

        public ChatRoom() { }
        public ChatRoom(String nome,String id,String iconUrl) {
            Alias = nome;
            Id = id;
            IconUrl = iconUrl;
            Chat = new Chat();
        }
    }
}
