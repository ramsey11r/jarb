using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class account
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string photo { get; set; }
        public string id_token { get; set; }
        public int session { get; set; }
        public string OldPassword { get; set; }
        public int telephone { get; set; }
        public string datenaissance { get; set; }
        public string localisation { get; set; }
        public string a_propos_de_moi { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedin { get; set; }
        public string twitter { get; set; }

        public string key = "wassimheros";
        public string coverttoencrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            password += key;
            var passwordByte = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordByte);
        }
    }
}
