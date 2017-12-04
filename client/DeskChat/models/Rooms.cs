using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskChat.models
{
    public class Rooms
    {
        public ObservableCollection<Room> RoomsList { get; set; }
        private Rooms () {
            RoomsList = new ObservableCollection<Room>();
        }
        private static Rooms rooms;
        public static Rooms getInstance()
        {
            if(rooms == null)
            {
                rooms = new Rooms();
            }
            return rooms;
        }        

    }
}
