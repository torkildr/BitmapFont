using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitmapFont
{
    public class Letter
    {
        private char _letter;

        public override string ToString()
        {
            if (_letter <= 127)
                return "" + _letter;
            else
                return "\\x" + Convert.ToString(_letter, 16).PadLeft(2, '0');
        }

        public Letter(char c)
        {
            _letter = c;
        }
    }
}
