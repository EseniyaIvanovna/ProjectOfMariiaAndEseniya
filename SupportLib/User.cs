using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportLib
{
    public class User
    {
        public string _name { get; set; }
        public string _theme { get; set; }
        public string _test { get; set; }
        public DateTime _date;
        public string _comments { get; set; }

        public User()
        {
            _date = DateTime.Today;
        }
    }
}
