using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskApp
{
    public class Class
    {
        private int _id;
        private string _name;
        private string _phone;
        private string _issue;

        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }

        public string Issue
        {
            get
            {
                return _issue;
            }
            set
            {
                _issue = value;
            }
        }
    }

}