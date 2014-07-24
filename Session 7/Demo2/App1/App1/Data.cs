using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    static class Data
    {
        public static decimal Value
        {
            get
            {
                return (_value);
            }
            set
            {
                _value = value;
            }
        }
        static decimal _value;
    }
}
