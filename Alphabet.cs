using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BitmapFont
{
    public class Alphabet
    {
        private readonly List<Glyph> _glyphs = new List<Glyph>();

        /// <summary>
        /// Check if colors are equal
        /// </summary>
        /// <param name="a">First color</param>
        /// <param name="b">Second color</param>
        /// <returns>Equality of colors</returns>
        private bool ColorEqual(Color a, Color b) {
            if (a.R == b.R && a.B == b.B && a.G == b.G)
                return true;

            return false;
        }

        /// <summary>
        /// The alphabet represented as a string
        /// </summary>
        public string AlphabetString
        {
            get
            {
                string alphabet = "";

                foreach (var glyph in _glyphs)
                {
                    string letter = glyph.Letter;

                    if (letter == "\\")
                        letter = "\\\\";
                    else if (letter == "\"")
                        letter = "\\\"";

                    alphabet += letter;
                }

                // Converts, for C safety: \ -> \\ and " -> \"
                return string.Format("\"{0}\";", alphabet);
            }
        }

        /// <summary>
        /// Width of characters as a string
        /// </summary>
        public string Width
        {
            get
            {
                string width = "";

                foreach (var glyph in _glyphs)
                {
                    width += glyph.Size;
                }

                return string.Format("\"{0}\";", width);
            }
        }

        /// <summary>
        /// Numbers of glyphs in alphabet
        /// </summary>
        public int Count
        {
            get
            {
                return _glyphs.Count;
            }
        }

        /// <summary>
        /// Alphabet exported as a font
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "{\n    " + string.Join("\n    ", _glyphs) + "\n};";
        }

        /// <summary>
        /// Create new alphabet from image
        /// </summary>
        /// <param name="image">Bitmap image</param>
        /// <param name="alphabet">String of all the characters in bitmap image, ordered</param>
        public Alphabet(Bitmap image, string alphabet)
        {
            // last row is marker bit
            var rows = image.Height - 1;
            var columns = new List<Column>();
            var letterCount = 0;

            // Indicates that we're supposed to read a new character
            bool gap = false;

            // All columns
            for (var x = 0; x < image.Width; x++)
            {
                if (ColorEqual(image.GetPixel(x, rows), Color.Red))
                {
                    if (!gap)
                    {
                        // new gap means finished glyph
                        if (letterCount < alphabet.Length)
                            _glyphs.Add(new Glyph(columns, new Letter(alphabet[letterCount])));
                        else
                            _glyphs.Add(new Glyph(columns));

                        columns = new List<Column>();
                        letterCount++;
                    }

                    gap = true;
                }

                if (!ColorEqual(image.GetPixel(x, rows), Color.Red))
                {
                    byte column = 0;
                    gap = false;

                    // All rows in that column
                    for (var y = 0; y < rows; y++)
                    {
                        byte bit = 0;

                        // White is blank, everything else is set
                        if (!ColorEqual(image.GetPixel(x, y), Color.White))
                        {
                            bit = 1;
                        }

                        // Squeeze pixel in
                        column |= (byte) (bit << y);
                    }

                    columns.Add(new Column(column));
                }
            }
        }
    }
}
