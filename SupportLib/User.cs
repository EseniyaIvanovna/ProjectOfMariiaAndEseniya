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
        public string _date;
        public string _comments { get; set; }

        public User()
        {
            _date = DateTime.Now.ToString("dd:MM:yyyy");

        }
        public override string ToString()
        {
            return $"Дата: {_date} Класс: {_name} Тема:{_theme} Тест:{_test} ";
        }
    }
   
}
