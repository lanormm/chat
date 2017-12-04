using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskChat.models
{
    public class Chat
    {
        public List<String> Messages { get;set; }
        public ObservableCollection<String> col { get; set; }
        public Chat() {
            Messages = new List<string>();
            col = new ObservableCollection<string>();
        }
    }
}
