using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskChat.models
{
    public class Room
    {
        public Room()
        {
            Chat = new Chat();
        }

        public String Alias { get; set; }
        public String Id { get; set; }
        public String IconUrl { get; set; }

        public Chat Chat { get; set; }
    }
}
