using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitmapFont
{
    public class Column
    {
        private byte _pixels;

        /// <summary>
        /// Export column as part of the font
        /// </summary>
        /// <returns>Font column</returns>
        public override string ToString()
        {
            return string.Format("0x{0}", Convert.ToString(_pixels, 16).PadLeft(2, '0'));
        }

        /// <summary>
        /// Column of pixels
        /// </summary>
        /// <param name="pixels">Pixels in this column</param>
        public Column(byte pixels)
        {
            _pixels = pixels;
        }
    }
}
