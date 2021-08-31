using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoClient.Models
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string emoij { get; set; }

        public User(string id, string name, string password, string email, string phone, string address, string emoij)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.emoij = emoij;
        }

        public User()
        {
        }
    }
}