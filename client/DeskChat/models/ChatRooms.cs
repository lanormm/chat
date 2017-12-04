using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskChat.models
{
    public class ChatRooms
    {
        private ChatRooms() {
            RoomsList = new List<ChatRoom>();
        }
        private static ChatRooms rooms;
        public List<ChatRoom> RoomsList { get; set; }
        public static ChatRooms getInstance()
        {
            if(rooms == null)
            {
                rooms = new ChatRooms();
            }
            return rooms;
        }
    }
}
