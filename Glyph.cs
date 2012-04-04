using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitmapFont
{
    public class Glyph
    {
        private List<Column> _columns;
        private Letter _letter;
        private int _size;

        /// <summary>
        /// Letter representing the glyph
        /// </summary>
        public string Letter
        {
            get
            {
                return _letter.ToString();
            }
        }

        /// <summary>
        /// Width of glyph
        /// </summary>
        public int Size
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// Export font
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string comment = "";

            if (_letter != null)
            {
                var letter = "" + _letter;

                if (letter == " ")
                    letter = "space";
                else if (letter == "\\")
                    letter = "slash";

                comment += " // " + letter;
            }

            var columns = new List<string>(_columns.Count);

            foreach (var column in _columns)
            {
                columns.Add(column.ToString());
            }

            return "{" + Size + "," + string.Join(",", columns.ToArray()) + "}," + comment;
        }

        /// <summary>
        /// Create new glyph
        /// </summary>
        /// <param name="columns">List of columns in glyph</param>
        /// <param name="letter">Letter representing the glyph</param>
        public Glyph(List<Column> columns, Letter letter)
        {
            _columns = columns;
            _size = columns.Count;

            // Pad with 0-bytes (8 width total, 7 columns and 1 size indicator)
            for (var i = columns.Count; i < 7; i++)
            {
                _columns.Add(new Column(0));
            }

            _letter = letter;
        }        

        /// <summary>
        /// Create new glyph without known letter representation
        /// </summary>
        /// <param name="columns">List of columns in glyph</param>
        public Glyph(List<Column> columns) : this(columns, null)
        {
        }
    }
}
