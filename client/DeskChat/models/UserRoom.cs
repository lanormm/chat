using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskChat.models;
namespace DeskChat.models
{
    public class UserRoom: Room
    {
        public byte[] SessionKey { get; set; }
        public byte[] SessionIV { get; set; }
        public UserRoom()
        {
            
        }
    }
}
