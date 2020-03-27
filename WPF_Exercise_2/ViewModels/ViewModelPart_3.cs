using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Exercise_2
{  

    public class ViewModelPart_3 : ViewModelBase
    {
        private DAO _currentDAO = new DAO();

        public string VmUserName { get; set; }
        public string VmPassword { get; set; }


        protected override string GetError()
        {
            List<UserDetails> userDetailsList = _currentDAO.GetAll<UserDetails>();
            List<string> userNames = new List<string>();
            List<string> passwords = new List<string>();
            foreach(var s in userDetailsList)
            {
                userNames.Add(s.UserName);
                passwords.Add(s.Password);
            }

           //if (VmUserName != "aaa" || VmPassword != "bbb") return "FuckingError";
           if(!userNames.Contains(VmUserName) || !passwords.Contains(VmPassword)) return "FuckingError";

            return String.Empty;
        }
    }
    public class UserDetails
    {
        public UserDetails() { }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
        

}
