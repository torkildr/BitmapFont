using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace BitmapFont
{
    class Program
    {
        private static string _headerFile = @"#include <avr/pgmspace.h>

#define FONT_COUNT {0}

#ifndef FONT_H
#define FONT_H

// This is what we have to work with
// Everything above default ascii values goes in raw fomat (extended ascii)
String charLookup = {1}

// Defines all characters in the lookup string
// First byte is width of font, rest is padded with 0-bytes
unsigned char PROGMEM font_variable[{0}][8] = {2}

#endif
";

        /// <summary>
        /// Converts bitmap image into atmega328-usable font
        /// 
        /// Image format is expected to be 9 pixels high, 8 of which are font pixels. Bottom pixel
        /// row is a marker, where contious red (#ff0000) pixels indicating gap.
        /// </summary>
        /// <param name="args">usage: image-file alphabet-as-text header-output</param>
        static void Main(string[] args)
        {
			if (args.Length < 3)
			{
				Console.WriteLine("usage: <image file> <alphabet file> <header output>");
				Environment.Exit(0);
			}
		
            var image = Image.FromFile(args[0]);
            var bitmap = new Bitmap(image);
            var str = File.ReadAllText(args[1], Encoding.GetEncoding(1252));

            var alphabet = new Alphabet(bitmap, str.TrimEnd());

            // Export to header file
            using (var file = File.Open(args[2], FileMode.Create))
            {
                using (var stream = new StreamWriter(file))
                {
                    stream.Write(
                        string.Format(_headerFile,
                                alphabet.Count,
                                alphabet.AlphabetString,
                                alphabet
                        ));
                }
            }
			
			Console.WriteLine(string.Format("Exported font to {0}", args[2]));
        }
    }
}
