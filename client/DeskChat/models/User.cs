using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace DeskChat.models
{
    public class UserChat
    {
        public String Alias { get; set; }
        public String Id { get; set; }
        public String imageUrl { get; set; }

        public byte[] PUBLIC_KEY;

        private UserStatus _status = UserStatus.DISPONIVEL;

        public UserStatus Status { get { return _status; } set { this._status = value; } }
        public UserChat()
        {

        }

    }
    public class User
    {
        public IObservable<String> UserName { get; }
        public UserChat UserAdp;

        public string Alias { get; set; }
        private string id = null;
        public String Id { get {
                if(id == null)
                {
                    id = Guid.NewGuid().ToString().Substring(0, 6);
                }
                return id;
        } }
        private static User user;
        private User() {
            UserAdp = new UserChat();
            UserAdp.Alias = Alias;
            UserAdp.Id = Id;
            UserAdp.Status = Status;
        }
        public static User getInstance()
        {
            if(user == null)
            {
                user = new User();
            }
            return user;
        }
        private UserStatus _status = UserStatus.DISPONIVEL;
        

        public UserStatus Status { get { return _status; } set { this._status = value; } }
        public void onchanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(sender.ToString());
        }
    }
}
