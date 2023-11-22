using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportLib
{
    public class User
    {
        public string _name;
        public string _theme;
        public DateTime _date;
        public string _comments;
        
        public User()
        {
            _date = DateTime.Today;
        }
    }
}
